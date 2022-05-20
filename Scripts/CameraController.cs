using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BallController target;
    private float offset;

    
    void Awake()
    {
        offset = transform.position.y - target.transform.position.y;
    }

    void Update()
    {
        Vector3 currPos = transform.position;
        currPos.y = target.transform.position.y + offset;    
        transform.position = currPos;
    }
}
