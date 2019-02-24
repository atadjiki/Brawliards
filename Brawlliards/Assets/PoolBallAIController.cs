using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBallAIController : MonoBehaviour
{

    private GameObject target;
    private float maxMotion = 60f;
    private float currentMotion = 0;
    private float maxAttack = 120f;
    private float currentAttack = 0;

    public PoolBallController poolBallController;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if (poolBallController == null)
        {
            if (this.gameObject.GetComponent<PoolBallController>() == null)
            {
                poolBallController = this.gameObject.AddComponent<PoolBallController>();
            }
            else
            {
                poolBallController = this.gameObject.GetComponent<PoolBallController>();
            }

        }

        rb = GetComponent<Rigidbody>();

        FindClosestBall();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentMotion >= maxMotion)
        {
            FindClosestBall();
            currentMotion = 0;
        }
        else
        {
            currentMotion++;
        }

        poolBallController.DoSphereMotion(rb, GetDirection());

        if(currentAttack >= maxAttack)
        {
            poolBallController.DoForwardMotion(rb, true);
            currentAttack = 0;
        }
        else
        {
            currentAttack++;
        }

    }

    Vector3 GetDirection()
    {
        if(target != null)
        {
            Debug.DrawLine(this.transform.position, target.transform.position);
            return target.transform.position - this.transform.position;
        }
        else
        {
            return Vector3.forward;
        }

    }

    void FindClosestBall()
    { 

        Collider[] collisions = Physics.OverlapSphere(this.transform.position, Mathf.Infinity);
       
        foreach(Collider col in collisions)
        {
            if (col.gameObject.GetComponent<PoolBallController>() != null && col.gameObject != this.gameObject)
            {
            
                if (target == null)
                {
                    target = col.gameObject;
                    Debug.Log("New Target! " + col.name);
                    return;
                }

            }
        }
    }
}
