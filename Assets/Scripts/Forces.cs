using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forces : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void VelUpdate(GameObject[] spheres)
    {
        int sphereLen = spheres.Length;
        Vector3[] velBuffer = new Vector3[sphereLen];
        for (int i = 0; i < sphereLen / 2; i++)
        {
            for (int j = 0; j < sphereLen / 2; j++) //fill velocity buffer
            {
                velBuffer[i*(sphereLen/2) + j] = spheres[i*(sphereLen/2) + j].GetComponent<Rigidbody>().velocity;
            }
        }

        //update velocities before positions.
        //horizontal
        for(int i = 0; i < ; i++)
        {
            for(int j = 0; j < ; j++)
            {
                //vector between point and its horizontal neighbor. math to get said horizontal neighbors might be wrong though.
                Vector3 e = spheres[i*(sphereLen/2)+j+1]-spheres[i*(sphereLen/2)+j]; 
                float l = e.magnitude;
                e.Normalize();
                float v1 = Vector3.Dot(e,spheres[i * (sphereLen / 2) + j].GetComponent<Rigidbody>().velocity);
                float v2 = Vector3.Dot(e,spheres[i * (sphereLen / 2) + j + 1].GetComponent<Rigidbody>().velocity);
                float ks = 1; //spring constant, can tweak
                float kd = 0.1f; //dampening, can tweak
                Vector3 f = -ks * (spheres[i * (sphereLen / 2) + j].transform.position-l) - kd * (v1 - v2);
                velBuffer[i * (sphereLen / 2) + j] = f; //times e times dt
                velBuffer[i * (sphereLen / 2) + j + 1] = -f; //times e times dt
            }
        }

        //vertical
        for (int i = 0; i < ; i++)
        {
            for (int j = 0; j < ; j++)
            {
                //vector between point and its horizontal neighbor. math to get said horizontal neighbors might be wrong though.
                Vector3 e = spheres[i  + j * (sphereLen / 2) + 1] - spheres[i + j * (sphereLen / 2)];
                float l = e.magnitude;
                e.Normalize();
                float v1 = Vector3.Dot(e,spheres[i + j * (sphereLen / 2)].GetComponent<Rigidbody>().velocity);
                float v2 = Vector3.Dot(e,spheres[i + j * (sphereLen / 2) + 1].GetComponent<Rigidbody>().velocity);
                float ks = 1; //spring constant, can tweak
                float kd = 0.1f; //dampening, can tweak
                Vector3 f = -ks * (spheres[i + j * (sphereLen / 2)].transform.position - l) - kd * (v1 - v2);
                velBuffer[i + j * (sphereLen / 2)] = f; //times e times dt
                velBuffer[i + j * (sphereLen / 2) + 1] = -f; //times e times dt
            }
        }

        //update velocity buffer with forces

        //fix top row in place in buffer

        //set velocity to new velocity

    } //velUpdate
}
