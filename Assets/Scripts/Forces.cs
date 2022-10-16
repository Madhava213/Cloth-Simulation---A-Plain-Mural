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

    void Forces(GameObject[] spheres)
    {
        int sphereLen = spheres.Length();
        Vector3 velBuffer = new Vector3[sphereLen];
        for (int i = 0; i < sphereLen / 2; i++)
        {
            for (int j = 0; j < sphereLen / 2; j++)
            {
                velBuffer[i + j] = spheres[i + j].GetComponent<Rigidbody>().velocity;
            }
        }

    }
}
