using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ParticleNet : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NO2" || other.gameObject.tag == "N2O4")
        {
            GameObject particleGen = GameObject.Find("ParticleGeneration"); 
            GameObject spawner = particleGen.GetComponent<ParticleGeneration>().Get_Spawner();
            float spawnHeight = particleGen.GetComponent<ParticleGeneration>().Get_Spawn_Height();

            float spawn_x = spawner.transform.position.x;
            float spawn_y = spawner.transform.position.y;
            float spawn_z = spawner.transform.position.z;


           
            
           

            float newPos_X = Random.Range(spawn_x - .168f, spawn_x + 0.168f);
            float newPos_Y = Random.Range(spawn_y + 5f, spawn_y + (0.19f + (10f * spawnHeight)));
            float newPos_Z = Random.Range(spawn_z - .1f, spawn_z + .1f);


            //Debug.LogWarning("Particle Leakage! Sending back to container. - " + other.gameObject.name);

            Vector3 position = new Vector3(newPos_X, newPos_Y, newPos_Z);
            other.gameObject.transform.position = position;
        }
    }

}
