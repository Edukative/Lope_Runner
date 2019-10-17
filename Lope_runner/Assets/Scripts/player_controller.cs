﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    private Rigidbody playerRB;

    public float jumpForce; // the force the player jumps
    public float gravityModifier; // to modify the gravity, to earth one to a lunar one!

    public bool IsOnGround = true; //is on the ground
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); //Get the Rigidbody component
        playerRB.AddForce(Vector3.up * 1000); //Adding a physics force up

        Physics.gravity *= gravityModifier; //Modify the default Unity gravity to your gravity!
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)  // if you press space and is touching the ground
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Apply a impulse force, to make it up
            IsOnGround = false; // no longer touches the ground
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsOnGround = true;
    }
}