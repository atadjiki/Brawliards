using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float speed = 10f;
    public float attack_force = 20f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        GetInput();
    }

    void GetInput()
    {

        DoSphereMotion(rb, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        DoForwardMotion(rb, Input.GetKeyDown(KeyCode.LeftShift));

        
    }

    void DoSphereMotion(Rigidbody rigidBoy, float moveHorizontal, float moveVertical)
    {
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidBoy.AddForce(movement * speed);
    }

    void DoForwardMotion(Rigidbody rigidBody, bool allow)
    {
        if (!allow) { return; }

        rigidBody.AddForce(rigidBody.velocity * attack_force);
    }


}

