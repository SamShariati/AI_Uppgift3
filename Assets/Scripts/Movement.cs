using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    int rotateSpeed = 1;
    public void Move(float FB, float LR)
    {
        LR = Mathf.Clamp(LR, -1, 1);
        FB = Mathf.Clamp(FB, -1, 1);


        transform.Rotate(0, LR * rotateSpeed, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        
    }
}
