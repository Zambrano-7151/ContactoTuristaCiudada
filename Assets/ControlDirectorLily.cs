using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDirectorLily : MonoBehaviour
{
    Camera cam;
    
    void Start()
    {
        cam = Camera.main;

        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cam.transform, Vector3.down);
       var euler= transform.localEulerAngles;
        euler.z = 0;        
        euler.y = 0;
        euler.x *= -1;
        euler.z *=-1;
        transform.localEulerAngles = euler;
    }
}
