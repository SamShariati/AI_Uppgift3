using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //int rotateSpeed = 1;
    //public void Move(float FB, float LR)
    //{
    //    LR = Mathf.Clamp(LR, -1, 1);
    //    FB = Mathf.Clamp(FB, -1, 1);


    //    transform.Rotate(0, LR * rotateSpeed, 0);

    //    Vector3 forward = transform.TransformDirection(Vector3.forward);

    //}
    public CharacterController controller;
    private bool hasController = false;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;
    public float speed = 10.0F;
    public float rotateSpeed = 10.0F;
    public float FB = 0;
    public float LR = 0;

    //private ObjectTracker objectTracker;
    private Agent agent;

    void Awake()
    {
        //objectTracker = FindObjectOfType<ObjectTracker>();
        agent = GetComponent<Agent>();
        controller = GetComponent<CharacterController>();
    }
    public void Move(float FB, float LR)
    {
        LR = Mathf.Clamp(LR, -1, 1);
        FB = Mathf.Clamp(FB, 0, 1);

        //flytta agenten
        //if (!agent.isDead)
        //{
        //    transform.Rotate(0, LR * rotateSpeed, 0);

        //    Vector3 forward = transform.TransformDirection(Vector3.forward);
        //    controller.SimpleMove(forward * speed * FB * -1);
        //}

        transform.Rotate(0, LR * rotateSpeed, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed * FB * -1);


        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        else
        {
            // Gravity
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
