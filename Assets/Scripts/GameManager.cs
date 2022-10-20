using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LineController line;
    public GameObject Sphere;
    public GameObject Holder;
    private GameObject[,] spheres;
    private GameObject holder;

    public int numSpheres = 5;
    private int spheresLength;
    private float maxDistance;
    public float maxDistanceMultiplier = 1.0f;
    public float sphereDistanceOffset = 1.8f;
    public float restLength = 1/1.8f;
    public Vector3 spawnPos = new Vector3(0.0f,0.0f,0.0f);
    public float ks = 1.0f; //spring constant, can tweak
    public float kd = 0.1f; //dampening, can tweak
    public float airDensity = 1.0f; //density of the air, constant
    public float airDrag = 1.0f; //drag coefficient, constant
    public float speedFactor = 12f; // Speed Factor for velocity and Rendering
    public Vector3 gravity = new Vector3(0,-0.1f,0); //Gravity


    // Start is called before the first frame update
    void Start()
    {
        spheresLength = numSpheres * numSpheres;
        maxDistance = 1 / sphereDistanceOffset * maxDistanceMultiplier;
        spheres = new GameObject[numSpheres,numSpheres];

        // Instantiate Holder
        Vector3 holderPos = new Vector3(spawnPos.x + (numSpheres/(2 * sphereDistanceOffset)), spawnPos.y + 0.05f , spawnPos.z + ((numSpheres-1)/sphereDistanceOffset));
        holder = Instantiate(Holder, holderPos, Quaternion.Euler(0,0,90));
        holder.transform.localScale *= numSpheres/1.5f;

        GenerateSpheres();

        GenerateLines();
    }

    // Update is called once per frame
    void Update()
    {
        VelUpdate();
        AirDrag();
    }

    void GenerateSpheres(){
        
        // Instantiate Spheres
        for (int i = 0; i < numSpheres; i++)
        {
            for (int j = 0; j < numSpheres; j++)
            {
                Vector3 position = new Vector3(spawnPos.x + i/sphereDistanceOffset, spawnPos.y , spawnPos.z + j/sphereDistanceOffset);
                GameObject newSphere = Instantiate(Sphere, position, Quaternion.identity);
                newSphere.name = "Sphere " + ((i*numSpheres)+j);
                spheres[i,j] = newSphere;
                newSphere.transform.SetParent(holder.transform);
                if(j == numSpheres-1){
                    newSphere.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
                    newSphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
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
                        newLine.transform.SetParent(holder.transform);
                    }

                    // Horizontal Lines
                    if((((i+1)*numSpheres)+j) < spheresLength){
                        line.addPoints(new GameObject[2] { spheres[i,j], spheres[i+1,j]});
                        LineController newLine = Instantiate(line, spheres[i,j].transform.position, Quaternion.identity);
                        newLine.name = spheres[i,j].name + " to " + spheres[i+1,j].name;
                        newLine.transform.SetParent(holder.transform);
                    }
                }
            }
    }
    void VelUpdate()
    {
        float dtFactor = Time.deltaTime * speedFactor;
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
                Vector3 e = spheres[i,j+1].transform.position-spheres[i,j].transform.position; 
                float l = e.magnitude;
                e.Normalize();
                float v1 = Vector3.Dot(e,spheres[i,j].GetComponent<Rigidbody>().velocity);
                float v2 = Vector3.Dot(e,spheres[i,j + 1].GetComponent<Rigidbody>().velocity);
                float f = -ks * ((restLength) - l) - kd * (v1 - v2);
                velBuffer[i,j] += e * f * dtFactor; //times e times dt
                velBuffer[i,j + 1] -= e * f * dtFactor; //times e times dt
            }
        }

        // Horizontal
        for (int i = 0; i < numSpheres - 1; i++)
        {
            for (int j = 0; j < numSpheres; j++)
            {
                {
                    Vector3 e = spheres[i+1,j].transform.position - spheres[i,j].transform.position;
                    float l = e.magnitude;
                    e.Normalize();
                    float v1 = Vector3.Dot(e,spheres[i,j].GetComponent<Rigidbody>().velocity);
                    float v2 = Vector3.Dot(e,spheres[i+1,j].GetComponent<Rigidbody>().velocity);
                    float f = -ks * ((restLength) - l) - kd * (v1 - v2);
                    velBuffer[i,j] += e * f * dtFactor; //times e times dt
                    velBuffer[i + 1,j] -= e * f * dtFactor; //times e times dt
                }
            }
        }


        //Add Gravity, Fix top row in place in buffer & set velocity to new velocity
        for (int i = 0; i < numSpheres; i++) //fill velocity buffer
        {
            for (int j = 0; j < numSpheres-1; j++) //fill velocity buffer
            {
                // Add Gravity
                velBuffer[i, j] += gravity * dtFactor;
                // Update Velocities
                spheres[i, j].GetComponent<Rigidbody>().velocity = velBuffer[i, j];

            }
        }
    } //velUpdate


    void AirDrag()
    {
        for(int i = 0; i < numSpheres - 1; i++){ //num triangles in shape. could also do num squares and just repeat the code with the right side up ones.
            for (int j = 0; j < numSpheres - 1; j++)
            {
                Vector3 velocity = (spheres[i, j].GetComponent<Rigidbody>().velocity + spheres[i + 1, j].GetComponent<Rigidbody>().velocity + spheres[i, j + 1].GetComponent<Rigidbody>().velocity) / 3; //minus relative velocity of the air, which I'm not sure how to get.
                Vector3 nstar = Vector3.Cross((spheres[i + 1, j].transform.position - spheres[i, j].transform.position), (spheres[i, j + 1].transform.position - spheres[i, j].transform.position));
                Vector3 vtan = ((velocity.magnitude * Vector3.Dot(velocity, nstar)) / (2 * nstar.magnitude)) * nstar; //|v|^2 * a * n
                Vector3 f1 = vtan * airDensity * airDrag * -0.5f; //divide by 3 to get force applied at each particle, and apply it.

                //code's repeated here just so we do have it for right side up too.
                velocity = (spheres[i, j + 1].GetComponent<Rigidbody>().velocity + spheres[i + 1, j].GetComponent<Rigidbody>().velocity + spheres[i + 1, j + 1].GetComponent<Rigidbody>().velocity) / 3; //minus velocity of air
                nstar = Vector3.Cross((spheres[i + 1, j].transform.position - spheres[i, j + 1].transform.position), (spheres[i + 1, j + 1].transform.position - spheres[i, j + 1].transform.position));
                vtan = ((velocity.magnitude * Vector3.Dot(velocity, nstar)) / (2 * nstar.magnitude)) * nstar; //|v|^2 * a * n
                Vector3 f2 = vtan * airDensity * airDrag * -0.5f; //divide by 3 to get force applied at each particle, and apply it.

                spheres[i, j].GetComponent<Rigidbody>().AddForce(f1);

                if(i+1 != numSpheres-1){
                    spheres[i+1, j].GetComponent<Rigidbody>().AddForce(f1 + f2);
                }
                if(j+1 != numSpheres-1){
                    spheres[i, j+1].GetComponent<Rigidbody>().AddForce(f1 + f2);
                }
                if(i+1 != numSpheres-1 && j+1 != numSpheres-1){
                    spheres[i, j+1].GetComponent<Rigidbody>().AddForce(f1 + f2);
                }
            }
        }
    } 

    // void ClothElasticity(){
    //     // Vertical
    //     for (int i = 0; i <  numSpheres; i++)
    //     {
    //         for (int j = 0; j < numSpheres - 1; j++){

    //             GameObject currSphere = spheres[i,j];
    //             GameObject nextSphere = spheres[i,j+1];

    //             float dist = (currSphere.transform.position - nextSphere.transform.position).magnitude;
    //             float error = Mathf.Abs(dist - maxDistance);
    //             Vector3 changeDir = Vector3.zero;

    //             if (dist > maxDistance)
    //             {
    //                 changeDir = (currSphere.transform.position - nextSphere.transform.position).normalized;
    //             } else if (dist < maxDistance)
    //             {
    //                 changeDir = (nextSphere.transform.position - currSphere.transform.position).normalized;
    //                 error = 0;
    //             }

    //             Vector3 changeAmount = changeDir * error;
    //             if (((i*numSpheres)+ j + 2) % numSpheres != 0){
    //                 currSphere.transform.position -= changeAmount;
    //                 spheres[i,j] = currSphere;
    //                 nextSphere.transform.position += changeAmount;
    //                 spheres[i,j + 1] = nextSphere;
    //             }
    //             else{
    //                 currSphere.transform.position -= changeAmount;
    //                 spheres[i,j] = currSphere;
    //             }
    //         }
    //     }

    //     // Horizontal
    //     for (int i = 0; i <  numSpheres-1; i++)
    //     {
    //         for (int j = 0; j < numSpheres - 1; j++){

    //             GameObject currSphere = spheres[i,j];
    //             GameObject nextSphere = spheres[i+1,j];

    //             float dist = (currSphere.transform.position - nextSphere.transform.position).magnitude;
    //             float error = Mathf.Abs(dist - maxDistance);
    //             Vector3 changeDir = Vector3.zero;

    //             if (dist > maxDistance)
    //             {
    //                 changeDir = (currSphere.transform.position - nextSphere.transform.position).normalized;
    //             } else if (dist < maxDistance)
    //             {
    //                 changeDir = (nextSphere.transform.position - currSphere.transform.position).normalized;
    //                 error = 0;
    //             }
    //             Vector3 changeAmount = changeDir * error;
    //             currSphere.transform.position -= changeAmount;
    //             spheres[i,j] = currSphere;
    //             nextSphere.transform.position += changeAmount;
    //             spheres[i+1,j] = nextSphere;
    //         }
    //     }
    // }
}
