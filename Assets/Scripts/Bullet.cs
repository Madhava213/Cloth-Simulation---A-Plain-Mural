using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -20.0f){
            Destroy(this.gameObject);
        }
    }
}
