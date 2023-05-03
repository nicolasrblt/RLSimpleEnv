using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 10.0f; // forward movement speed
    public float turnSpeed = 50.0f; // turning speed
    public Rigidbody carRigidbody; // rigidbody of the car
    public float moveInput; // input for forward movement
    public float turnInput; // input for turning

/*    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // get input for forward movement and turning
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // move the car forward or backward
        Vector3 movement = transform.forward * moveInput * speed;
        carRigidbody.AddForce(movement);

        // turn the car left or right
        Quaternion turn = Quaternion.Euler(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0);
        carRigidbody.MoveRotation(carRigidbody.rotation * turn);

        // stop the car if space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            carRigidbody.velocity = Vector3.zero;
            carRigidbody.angularVelocity = Vector3.zero;
        }

    }*/
}
