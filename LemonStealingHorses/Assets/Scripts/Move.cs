﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private Transform t = null;
    private Rigidbody rb = null;
    private Animator animator;
    public int speed;
    public float TurnSpeed;
   
    // Use this for initialization
    void Start ()
    {
        t = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        animator.SetBool("Running", false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Quaternion rot = t.rotation;
        
        if (Input.GetKey(KeyCode.D))
        {
            rot *= Quaternion.AngleAxis(TurnSpeed * Time.deltaTime, Vector3.up);
        }   
        if (Input.GetKey(KeyCode.A))
        {
            rot *= Quaternion.AngleAxis(TurnSpeed * Time.deltaTime * -1f, Vector3.up);
        }
        //t.position += direction *Time.deltaTime* speed;
        t.localRotation = rot;
    }

    private void FixedUpdate()
    {
        animator.SetBool("Running", false);
        if (Input.GetKey(KeyCode.W))
        {
           // rb.AddForce(t.forward * speed, ForceMode.Acceleration);
            rb.MovePosition(rb.position + t.right * Time.deltaTime * speed);
            animator.SetBool("Running", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //rb.AddForce(-1.0f * t.forward * speed, ForceMode.Acceleration);
            rb.MovePosition(rb.position + -1.0f * t.right * Time.deltaTime*speed);
            animator.SetBool("Running", true);
        }

    }
}
