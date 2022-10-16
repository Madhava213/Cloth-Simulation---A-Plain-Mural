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

   /* void VelUpdate(GameObject[] spheres)
    {
        int sphereLen = spheres.Length();
        Vector3 velBuffer[] = new Vector3[sphereLen];
        for (int i = 0; i < sphereLen / 2; i++)
        {
            for (int j = 0; j < sphereLen / 2; j++) //fill velocity buffer
            {
                velBuffer[i*(sphereLen/2) + j] = spheres[i*(sphereLen/2) + j].GetComponent<Rigidbody>().velocity;
            }
        }

        //update velocities before positions.
        float forceBuffer[sphereLen];
        //horizontal
        for(int i = 0; i < ; i++)
        {
            for(int j = 0; j < ; j++)
            {
                //vector between point and its horizontal neighbor. math to get said horizontal neighbors might be wrong though.
                Vec3 e = spheres[i*(sphereLen/2)+j+1]-spheres[i*(sphereLen/2)+j]; 
                double l = Math.sqrt(e.dot(e)); //get length
                e = e / l; //normalize
                float v1 = e.dot(spheres[i * (sphereLen / 2) + j].GetComponent<Rigidbody>().velocity);
                float v2 = e.dot(spheres[i * (sphereLen / 2) + j + 1].GetComponent<Rigidbody>().velocity);
                float ks = 1; //spring constant, can tweak
                float kd = .1; //dampening, can tweak
                float f = -ks * (/*rest position*//*-l) - kd * (v1 - v2);
                forceBuffer[i * (sphereLen / 2) + j] = f; //times e times dt
                forceBuffer[i * (sphereLen / 2) + j + 1] = -f; //times e times dt
            }
        }

        //vertical
        for (int i = 0; i < ; i++)
        {
            for (int j = 0; j < ; j++)
            {
                //vector between point and its horizontal neighbor. math to get said horizontal neighbors might be wrong though.
                Vec3 e = spheres[i  + j * (sphereLen / 2) + 1] - spheres[i + j * (sphereLen / 2)];
                double l = Math.sqrt(e.dot(e)); //get length
                e = e / l; //normalize
                float v1 = e.dot(spheres[i + j * (sphereLen / 2)].GetComponent<Rigidbody>().velocity);
                float v2 = e.dot(spheres[i + j * (sphereLen / 2) + 1].GetComponent<Rigidbody>().velocity);
                float ks = 1; //spring constant, can tweak
                float kd = .1; //dampening, can tweak
                float f = -ks * (/*rest position*//*-l) - kd * (v1 - v2);
                forceBuffer[i + j * (sphereLen / 2)] = f; //times e times dt
                forceBuffer[i + j * (sphereLen / 2) + 1] = -f; //times e times dt
            }
        }

        //update velocity buffer with forces

        //fix top row in place in buffer

        //set velocity to new velocity

    } //velUpdate*/
}
