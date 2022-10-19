using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LineController line;
    public GameObject Sphere;
    private GameObject[,] spheres;
    public int numSpheres = 5;
    private int spheresLength;
    private float maxDistance;
    public float maxDistanceMultiplier = 1.0f;
    public float sphereDistanceOffset = 1.8f;
    public Vector3 spawnPos = new Vector3(0.0f,0.0f,0.0f);
    public float ks = 1; //spring constant, can tweak
    public float kd = 0.1f; //dampening, can tweak
    public Vector3 gravity = new Vector3(0,-0.1f,0); //Gravity


    // Start is called before the first frame update
    void Start()
    {
        spheresLength = numSpheres * numSpheres;
        maxDistance = 1 / sphereDistanceOffset * maxDistanceMultiplier;
        spheres = new GameObject[numSpheres,numSpheres];

        GenerateSpheres();

        GenerateLines();
    }

    // Update is called once per frame
    void Update()
    {
        VelUpdate();
        ClothElasticity();
    }

    void GenerateSpheres(){
        for (int i = 0; i < numSpheres; i++)
        {
            for (int j = 0; j < numSpheres; j++)
            {
                Vector3 position = new Vector3(spawnPos.x + i/sphereDistanceOffset, spawnPos.y, spawnPos.z + j/sphereDistanceOffset);
                GameObject newSphere = Instantiate(Sphere, position, Quaternion.identity);
                newSphere.name = "Sphere " + ((i*numSpheres)+j);
                spheres[i,j] = newSphere;
            }
        }
    }

    void GenerateLines(){
        for (int i = 0; i < numSpheres; i++)
            {
                for (int j = 0; j < numSpheres; j++)
                {
                    // Vertical Lines
                    if (((i*numSpheres)+ j + 1) % numSpheres != 0)
                    {
                        line.addPoints(new GameObject[2] { spheres[i,j], spheres[i,j+1] });
                        LineController newLine = Instantiate(line, spheres[i,j].transform.position, Quaternion.identity);
                        newLine.name = spheres[i,j].name + " to " + spheres[i,j+1].name;
                    }

                    // Horizontal Lines
                    if((((i+1)*numSpheres)+j) < spheresLength){
                        line.addPoints(new GameObject[2] { spheres[i,j], spheres[i+1,j]});
                        LineController newLine = Instantiate(line, spheres[i,j].transform.position, Quaternion.identity);
                        newLine.name = spheres[i,j].name + " to " + spheres[i+1,j].name;
                    }
                }
            }
    }
    void VelUpdate()
    {
        Vector3[,] velBuffer = new Vector3[numSpheres,numSpheres];
        for (int i = 0; i < numSpheres; i++) //fill velocity buffer
        {
            for (int j = 0; j < numSpheres; j++)
            {
                velBuffer[i,j] = spheres[i,j].GetComponent<Rigidbody>().velocity;
            }
        }
        //update velocities before positions.
        // Vertical
        for(int i = 0; i < numSpheres; i++)
        {
            for(int j = 0; j < numSpheres - 1; j++)
            {
                //vector between point and its horizontal neighbor. math to get said horizontal neighbors might be wrong though.
                    Vector3 e = spheres[i,j+1].transform.position-spheres[i,j].transform.position; 
                    float l = e.magnitude;
                    e.Normalize();
                    float v1 = Vector3.Dot(e,spheres[i,j].GetComponent<Rigidbody>().velocity);
                    float v2 = Vector3.Dot(e,spheres[i,j + 1].GetComponent<Rigidbody>().velocity);
                    float f = -ks * ((1/sphereDistanceOffset)-l) - kd * (v1 - v2);
                    velBuffer[i,j] += e * f * Time.deltaTime; //times e times dt
                    velBuffer[i,j + 1] -= -e * f * Time.deltaTime; //times e times dt
            }
        }

        // Horizontal
        for (int i = 0; i < numSpheres - 1; i++)
        {
            for (int j = 0; j < numSpheres; j++)
            {
                //vector between point and its horizontal neighbor. math to get said horizontal neighbors might be wrong though.
                {
                    Vector3 e = spheres[i+1,j].transform.position - spheres[i,j].transform.position;
                    float l = e.magnitude;
                    e.Normalize();
                    float v1 = Vector3.Dot(e,spheres[i,j].GetComponent<Rigidbody>().velocity);
                    float v2 = Vector3.Dot(e,spheres[i+1,j].GetComponent<Rigidbody>().velocity);
                    float f = -ks * ((1/sphereDistanceOffset) - l) - kd * (v1 - v2);
                    velBuffer[i,j] += e * f * Time.deltaTime; //times e times dt
                    velBuffer[i + 1,j] -= -e * f * Time.deltaTime; //times e times dt
                }
            }
        }


        //Add Gravity, Fix top row in place in buffer & set velocity to new velocity
        for (int i = 0; i < numSpheres; i++) //fill velocity buffer
        {
            for (int j = 0; j < numSpheres-1; j++) //fill velocity buffer
            {
                // Add Gravity
                velBuffer[i, j] += gravity * Time.deltaTime;

                // Update Velocities
                spheres[i, j].GetComponent<Rigidbody>().velocity = velBuffer[i, j];
            }
        }
    } //velUpdate

    void ClothElasticity(){
        // Vertical
        for (int i = 0; i <  numSpheres; i++)
        {
            for (int j = 0; j < numSpheres - 1; j++){

                GameObject currSphere = spheres[i,j];
                GameObject nextSphere = spheres[i,j+1];

                float dist = (currSphere.transform.position - nextSphere.transform.position).magnitude;
                float error = Mathf.Abs(dist - maxDistance);
                Vector3 changeDir = Vector3.zero;

                if (dist > maxDistance)
                {
                    changeDir = (currSphere.transform.position - nextSphere.transform.position).normalized;
                } else if (dist < maxDistance)
                {
                    changeDir = (nextSphere.transform.position - currSphere.transform.position).normalized;
                }

                Vector3 changeAmount = changeDir * error;
                if (((i*numSpheres)+ j + 2) % numSpheres != 0){
                    currSphere.transform.position -= changeAmount * 0.5f;
                    spheres[i,j] = currSphere;
                    nextSphere.transform.position += changeAmount * 0.5f;
                    spheres[i,j + 1] = nextSphere;
                }
                else{
                    currSphere.transform.position -= changeAmount * 0.5f;
                    spheres[i,j] = currSphere;
                }
            }
        }

        // Horizontal
        for (int i = 0; i <  numSpheres-1; i++)
        {
            for (int j = 0; j < numSpheres - 1; j++){

                GameObject currSphere = spheres[i,j];
                GameObject nextSphere = spheres[i+1,j];

                float dist = (currSphere.transform.position - nextSphere.transform.position).magnitude;
                float error = Mathf.Abs(dist - maxDistance);
                Vector3 changeDir = Vector3.zero;

                if (dist > maxDistance)
                {
                    changeDir = (currSphere.transform.position - nextSphere.transform.position).normalized;
                } else if (dist < maxDistance)
                {
                    changeDir = (nextSphere.transform.position - currSphere.transform.position).normalized;
                }
                Vector3 changeAmount = changeDir * error;
                currSphere.transform.position -= changeAmount * 0.5f;
                spheres[i,j] = currSphere;
                nextSphere.transform.position += changeAmount * 0.5f;
                spheres[i+1,j] = nextSphere;
            }
        }
    }
}
