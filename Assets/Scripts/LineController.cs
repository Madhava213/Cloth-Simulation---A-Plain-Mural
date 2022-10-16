using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    public int numCapVertices = 0;
    
    public GameObject[] points;

    public void addPoints(GameObject[] pts){
        lr.positionCount = pts.Length;
        this.points = pts;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].transform.position);
            lr.numCapVertices = numCapVertices;
        }
    }
}
