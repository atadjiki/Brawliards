using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBallController : MonoBehaviour
{
    public float speed = 10f;
    public float attack_force = 20f;

    public void DoSphereMotion(Rigidbody rigidBoy, float moveHorizontal, float moveVertical)
    {
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidBoy.AddForce(movement * speed);
    }

    public void DoSphereMotion(Rigidbody rigidbody, Vector3 vector)
    {
        rigidbody.AddForce(vector * speed);
    }

    public void DoForwardMotion(Rigidbody rigidBody, bool allow)
    {
        if (!allow) { return; }

        rigidBody.AddForce(rigidBody.velocity * attack_force);
    }


}

