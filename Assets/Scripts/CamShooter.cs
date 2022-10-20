using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShooter : MonoBehaviour
{
    public GameObject bullet;
    public float shootPower = 1f;
    public float moveOffset = 0.01f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            GameObject shell = Instantiate(bullet, transform.position, transform.rotation);
            Vector3 shootDir = transform.forward;
            shootDir.y += 0.1f;
            shell.GetComponent<Rigidbody>().AddForce(shootDir * shootPower);
        }

        // Change Position
        Vector3 newPos = transform.position;
        if(Input.GetKey("a")){
            newPos.x -= moveOffset;
        }
        if(Input.GetKey("d")){
            newPos.x += moveOffset;
        }
        if(Input.GetKey("w")){
            newPos.z += moveOffset;
        }
        if(Input.GetKey("s")){
            newPos.z -= moveOffset;
        }
        if(Input.GetKey("q")){
            newPos.y += moveOffset;
        }
        if(Input.GetKey("e")){
            newPos.y -= moveOffset;
        }
        transform.position = newPos;
    }
}
