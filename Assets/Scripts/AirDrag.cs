using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDrag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*  void AirDrag()
      {

          float density = ; //density of the air, constant
          float drag = ; //drag coefficient, constant
          for(){ //num triangles in shape. could also do num squares and just repeat the code with the right side up ones.
              Vec3 velocity = (spheres[i,j].GetComponent<Rigidbody>().velocity+spheres[i+1,j].GetComponent<Rigidbody>().velocity+spheres[i,j+1].GetComponent<Rigidbody>().velocity)/3 ; //minus relative velocity of the air, which I'm not sure how to get.
              Vec3 nstar = Cross((circles[i+1,j].transform.position-circles[i,j].transform.position),(circles[i,j+1].transform.position-circles[i,j].transform.position));
              vtan = ((velocity.magnitude * Dot(velocity, nstar))/(2*nstar.magnitude)) * nstar; //|v|^2 * a * n
              Vec3 f1 = -0.5 * density * drag * vtan; //divide by 3 to get force applied at each particle, and apply it.
    //code's repeated here just so we do have it for right side up too.
              velocity = (spheres[i,j+1].GetComponent<Rigidbody>().velocity+spheres[i+1,j].GetComponent<Rigidbody>().velocity+spheres[i+1,j+1].GetComponent<Rigidbody>().velocity)/3 ; //minus velocity of air
              nstar = Cross((circles[i+1,j].transform.position-circles[i,j+1].transform.position),(circles[i+1,j+1].transform.position-circles[i,j+1].transform.position));
              vtan = ((velocity.magnitude * Dot(velocity, nstar))/(2*nstar.magnitude)) * nstar; //|v|^2 * a * n
              Vec3 f2 = -0.5 * density * drag * vtan; //divide by 3 to get force applied at each particle, and apply it.
          }
      } */
}
