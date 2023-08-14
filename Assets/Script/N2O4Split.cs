using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class N2O4Split : MonoBehaviour
{
    [Header("Particle")]
    public GameObject particleGen;

    [Header ("Time")]
    float timer = 0f;
    [SerializeField] public int time_to_split = 0;

    [Header("Particle List")]
    private int listSize = 0;
    private static int thisIndex;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        listSize = particleGen.GetComponent<ParticleGeneration>().GetN2O4List().Count;
        thisIndex = ParticleGeneration.N2O4List.IndexOf(gameObject);
        
        //Debug.LogWarning("Number of N2O4List: " + particleGen.GetComponent<ParticleGeneration>().GetN2O4List().Count);
        if (listSize > 0 && gameObject.name == "N2O4(Clone)")
        {
            timer += Time.deltaTime;
            if (timer >= time_to_split)
            {
                //Debug.Log("5 SECONDS PASSED. About to delete " + thisIndex + " This Object: " + gameObject.name + " N2O4 Count: " + listSize);
                particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("N2O4", thisIndex);

                //Debug.Log("X: " + transform.localPosition.x + " Y: " + transform.localPosition.y + " Z: " + transform.localPosition.z);

                particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), 2, transform.position, true);
                //particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), 2, help, true);
                timer = 0f;
            }
        } else
        {
            timer = 0;
        }
    }
}