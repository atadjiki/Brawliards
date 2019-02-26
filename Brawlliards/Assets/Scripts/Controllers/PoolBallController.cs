using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBallController : MonoBehaviour
{
    private static float speed = 10f;
    private static float maxSpeed = 50f;
    private static float attack_force = 10f;
    private static float attack_max_charge = 50f;
    private int attack_charge;
    private int kills;
    public PoolBallController lastCollidedWith;
    private Rigidbody rb;

    private void Start()
    {
        kills = 0;
        lastCollidedWith = null;
        attack_charge = 0;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

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


        rigidBody.AddForce(rigidBody.velocity * Mathf.Clamp(attack_force * attack_charge, 0, 500));

        StartCoroutine(EffectsManager.instance.DoSmoke(this.gameObject, 1f));

        attack_charge = 0;
    }

    public void DoForwardMotion(Rigidbody rigidBody, Vector3 vector, bool allow)
    {
        if (!allow) { return; }

        rigidBody.AddForce(vector * Mathf.Clamp(attack_force * attack_charge, 0, 500));

        StartCoroutine(EffectsManager.instance.DoSmoke(this.gameObject, 1f)); ;

        attack_charge = 0;
    }

    public int incrementKills()
    {
        kills++;
        return kills;
    }

    public int getKills()
    {
        return kills;
    }

    public void ResetKills()
    {
        kills = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PoolBallController>() != null)
        {
            lastCollidedWith = other.gameObject.GetComponent<PoolBallController>();
        }

    }



    public void chargeAttack(bool allow)
    {
        if (!allow) { return;  }
        if (attack_charge <= attack_max_charge)
        {
            attack_charge++;

        }

    }



}

