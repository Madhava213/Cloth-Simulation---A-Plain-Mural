using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LineController line;
    public GameObject Sphere;
    private GameObject[] spheres;
    public int numSpheres = 49;
    public float sphereDistanceOffset = 1.8f;
    public Vector3 spawnPos = new Vector3(0.0f,0.0f,0.0f);


    // Start is called before the first frame update
    void Start()
    {
        int spheresLength = ((numSpheres/2) * (numSpheres/2));
        spheres = new GameObject[spheresLength];
        for (int i = 0; i < numSpheres/2; i++)
        {
            for (int j = 0; j < numSpheres/2; j++)
            {
                Vector3 position = new Vector3(spawnPos.x + i/sphereDistanceOffset, spawnPos.y + j/sphereDistanceOffset, spawnPos.z);
                GameObject newSphere = Instantiate(Sphere, position, Quaternion.identity);
                newSphere.name = "Sphere " + ((i*numSpheres/2)+j);
                spheres[((i*numSpheres/2)+j)] = newSphere;
                Debug.Log((i * numSpheres / 2) + j);
                Debug.Log(((numSpheres/2) * (numSpheres/2)));
            }
        }

        for (int i = 0; i < numSpheres/2; i++)
        {
            for (int j = 0; j < numSpheres / 2; j++)
            {
                if (i + 1 < spheresLength)
                {
                    line.addPoints(new GameObject[2] { spheres[i], spheres[i + 1] });
                    LineController newLine = Instantiate(line, spheres[i].transform.position, Quaternion.identity);
                    newLine.name = spheres[i].name + " to " + spheres[i + 1].name;
                }
                // if(i+(numSpheres/2) < numSpheres){
                //     line.addPoints(new GameObject[2] { spheres[i], spheres[i+(numSpheres/2)]});
                //     LineController newLine = Instantiate(line, spheres[i].transform.position, Quaternion.identity);
                //     newLine.name = spheres[i].name + " to " + spheres[i+(numSpheres/2)].name;
                // }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
