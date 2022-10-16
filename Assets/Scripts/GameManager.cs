using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LineController line;
    public GameObject Sphere;
    private GameObject[] spheres;
    public int numSpheres = 49;
    private int spheresLength;
    public float sphereDistanceOffset = 1.8f;
    public Vector3 spawnPos = new Vector3(0.0f,0.0f,0.0f);


    // Start is called before the first frame update
    void Start()
    {
        spheresLength = ((numSpheres / 2) * (numSpheres / 2));
        spheres = new GameObject[spheresLength];
        for (int i = 0; i < numSpheres/2; i++)
        {
            for (int j = 0; j < numSpheres/2; j++)
            {
                Vector3 position = new Vector3(spawnPos.x + i/sphereDistanceOffset, spawnPos.y, spawnPos.z + j/sphereDistanceOffset);
                GameObject newSphere = Instantiate(Sphere, position, Quaternion.identity);
                newSphere.name = "Sphere " + ((i*numSpheres/2)+j);
                spheres[((i*numSpheres/2)+j)] = newSphere;
            }
        }

        for (int i = 0; i < numSpheres/2; i++)
        {
            for (int j = 0; j < numSpheres / 2; j++)
            {
                // Vertical Lines
                if ((i * numSpheres / 2) + j + 1 < spheresLength)
                {
                    line.addPoints(new GameObject[2] { spheres[(i * numSpheres / 2) + j], spheres[(i * numSpheres / 2) + j + 1] });
                    LineController newLine = Instantiate(line, spheres[(i * numSpheres / 2) + j].transform.position, Quaternion.identity);
                    newLine.name = spheres[(i * numSpheres / 2) + j].name + " to " + spheres[(i * numSpheres / 2) + j + 1].name;
                }

                // Horizontal Lines
                if(((i+1) * numSpheres / 2) + j < spheresLength){
                    line.addPoints(new GameObject[2] { spheres[((i) * numSpheres / 2) + j], spheres[((i+1) * numSpheres / 2) + j]});
                    LineController newLine = Instantiate(line, spheres[((i) * numSpheres / 2) + j].transform.position, Quaternion.identity);
                    newLine.name = spheres[((i) * numSpheres / 2) + j].name + " to " + spheres[((i+1) * numSpheres / 2) + j].name;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void VelUpdate()
    {
        Vector3[] velBuffer = new Vector3[spheresLength];
        for (int i = 0; i < spheresLength / 2; i++)
        {
            for (int j = 0; j < spheresLength / 2; j++) //fill velocity buffer
            {
                velBuffer[i*(spheresLength/2) + j] = spheres[i*(spheresLength/2) + j].GetComponent<Rigidbody>().velocity;
            }
        }

        //update velocities before positions.
        //horizontal
        for(int i = 0; i < ; i++)
        {
            for(int j = 0; j < ; j++)
            {
                //vector between point and its horizontal neighbor. math to get said horizontal neighbors might be wrong though.
                Vector3 e = spheres[i*(spheresLength/2)+j+1].transform.position-spheres[i*(spheresLength/2)+j].transform.position; 
                float l = e.magnitude;
                e.Normalize();
                float v1 = Vector3.Dot(e,spheres[i * (spheresLength / 2) + j].GetComponent<Rigidbody>().velocity);
                float v2 = Vector3.Dot(e,spheres[i * (spheresLength / 2) + j + 1].GetComponent<Rigidbody>().velocity);
                float ks = 1; //spring constant, can tweak
                float kd = 0.1f; //dampening, can tweak
                Vector3 f = -ks * (spheres[i * (spheresLength / 2) + j].transform.position-l) - kd * (v1 - v2);
                velBuffer[i * (spheresLength / 2) + j] = f; //times e times dt
                velBuffer[i * (spheresLength / 2) + j + 1] = -f; //times e times dt
            }
        }

        //vertical
        for (int i = 0; i < ; i++)
        {
            for (int j = 0; j < ; j++)
            {
                //vector between point and its horizontal neighbor. math to get said horizontal neighbors might be wrong though.
                Vector3 e = spheres[i  + j * (spheresLength / 2) + 1].transform.position - spheres[i + j * (spheresLength / 2)].transform.position;
                float l = e.magnitude;
                e.Normalize();
                float v1 = Vector3.Dot(e,spheres[i + j * (spheresLength / 2)].GetComponent<Rigidbody>().velocity);
                float v2 = Vector3.Dot(e,spheres[i + j * (spheresLength / 2) + 1].GetComponent<Rigidbody>().velocity);
                float ks = 1; //spring constant, can tweak
                float kd = 0.1f; //dampening, can tweak
                Vector3 f = -ks * (spheres[i + j * (spheresLength / 2)].transform.position - l) - kd * (v1 - v2);
                velBuffer[i + j * (spheresLength / 2)] = f; //times e times dt
                velBuffer[i + j * (spheresLength / 2) + 1] = -f; //times e times dt
            }
        }

        //update velocity buffer with forces

        //fix top row in place in buffer

        //set velocity to new velocity

    } //velUpdate
}
