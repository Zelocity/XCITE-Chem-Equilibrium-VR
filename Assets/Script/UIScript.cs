using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{


    [Header("N2O")]
    public TextMeshProUGUI NO2_Counter;
    public static int numNO2;

    [Header("N2O4")]
    public TextMeshProUGUI N2O4_Counter;
    public static int numN2O4;

    [Header("Particle Generation")]
    public GameObject particleGen;
    public GameObject spawn;
    public TextMeshProUGUI conc_str;
    public Slider conc_slider;
    private static int conc_num = 1;

    [Header("Lid")]
    public GameObject lid;
    private static float lid_start_pos;
    private static float lid_current_pos;
    private static bool up_lid_pressure;
    private static bool down_lid_pressure;

    [Header("Temperature")]
    public Slider temp_slider;
    private static bool temp_point_up;
    //public static  TextMeshProUGUI temp_str;


    private void Start()
    {
        switch (tag)
        {
            case "Untagged":
                break;

            case "N02 Counter":
                N02Count();
                break;

            case "N204 Counter":
                N2O4_Counter = GetComponent<TextMeshProUGUI>();
                break;

            case "Pressure":
                lid_start_pos = lid.transform.localPosition.y;
                break;

            case "Temperature":
                Temperature_Change(0.15f);
                break;
        }

    }

    private void FixedUpdate()
    {
    
        //if (PlaceObject.GetComponent<PlacePrefab>().Get_Placed())
        //{
            //if (start)
            //{

            //    Start();
            //    start = false;
            //}
            switch (tag)
            {
                case "Untagged":
                    break;

                case "N02 Counter":
                    N02Count();
                    break;

                case "N204 Counter":
                    N204Count();
                    break;

                case "Magnitude Num":
                    MagnitudeNum();
                    break;

                case "Temperature":
                    if (temp_point_up)
                    {
                        Temperature_Change(temp_slider.value);
                    }
                    break;

                case "Pressure":
                    //Lid_Movemwen
                    if (up_lid_pressure)
                    {
                        Lid_Movement();
                    }
                    else if (down_lid_pressure)
                    {
                        Lid_Movement();
                    }
                    break;
            }
        //}

    }


    public void CreateButton()
    {
        //create NO2 object with specified quantity at random location. IGNORE THIRD PARAMETER HERE, 4th indicates if particle is splitting.
        particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), conc_num, new Vector3(0,0,0), false);
        numNO2 += conc_num;
    }

    public void DestroyButton()
    {
        if (numNO2 != 0)
        {
            int numConc = conc_num;
            if (numConc > numNO2)
            {
                numConc = numNO2;
            }

            for (int i = 0; i < numConc; i++)
            {
                particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("NO2" ,-1);
            }

        }
        //Debug.Log(numNO2);
    }

    public void Clear_Particles()
    {
        //Debug.Log("Clearing Particles");
        List<GameObject> NO2_List = ParticleGeneration.moleculeList;
        List<GameObject> N2O4_List = ParticleGeneration.N2O4List;

        while (NO2_List.Count != 0 || N2O4_List.Count != 0)
        {
            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("N2O4", 0);
            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("NO2", 0);
        }
    }

    public void Lid_Movement()
    {
        lid_current_pos = lid.transform.localPosition.y;
        float lid_level_diff = lid_start_pos - lid_current_pos;

        if (up_lid_pressure == true && lid_start_pos > lid_current_pos)
        {
            //Debug.Log("go up");
            lid.transform.Translate(Vector3.right * Time.deltaTime *.2f);
        }
        else if (down_lid_pressure == true && lid_level_diff < 412)
        {
            //Debug.Log("go down");
            lid.transform.Translate(Vector3.left * Time.deltaTime * .2f);
        }

        //inputs percentage in decimal to set spawn height since spawn length is 10 and lid distance is 412.
        //there's probably a better way to do this.

        particleGen.GetComponent<ParticleGeneration>().Spawn_Height(1 - lid_level_diff / 412f);
    }

    public void Temperature_Change(float value)
    {
        List<GameObject> NO2_List = ParticleGeneration.moleculeList;
        List<GameObject> N2O4_List = ParticleGeneration.N2O4List;

        int i = 0;
        while (i < NO2_List.Count) {
            NO2_List[i].GetComponent<ParticlePhysics>().Modify_Average_Speed(value);
            i++;
        }

        int j = 0;
        while (j < N2O4_List.Count)
        {
            N2O4_List[j].GetComponent<ParticlePhysics>().Modify_Average_Speed(value);
            j++;
        }
    }


    public void N02Count()
    {
        numNO2 = ParticleGeneration.moleculeList.Count;
        NO2_Counter.text = numNO2.ToString();
        //NO2_Counter.text = "NO<sub>2</sub> = " + numNO2.ToString();
    }

    public void N204Count()
    {
        numN2O4 = ParticleGeneration.N2O4List.Count;
        string str = numN2O4.ToString();
        //string str = "N<sub>2</sub>O<sub>4</sub> = " + numN2O4.ToString();
        N2O4_Counter.text = str;
    }

    public void Pressure_Up_Button(bool up) { up_lid_pressure = up; }

    public void Pressure_Down_Button(bool down) { down_lid_pressure = down; }

    public void Temp_Slider(bool up) { temp_point_up = up; }

    public void MagnitudeNum() { conc_num = (int)conc_slider.value; conc_str.text = conc_num.ToString(); }

    public void Set_Lid(GameObject newLid) { lid = newLid; }

    public void Dismiss_Welcome()
    {
        GameObject.Find("AR Session Origin").GetComponent<PlacePrefab>().enabled = true;

    }

}
