using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBallAIController : MonoBehaviour
{

    private GameObject target;

    private float maxMotion = 60f;
    private float currentMotion = 0;

    private float maxAttack = 600f;
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
            poolBallController.DoForwardMotion(rb, GetDirection(), true);
            currentAttack = 0;
        }
        else
        {
            currentAttack++;
            poolBallController.chargeAttack(true);
        }

    }

    Vector3 GetDirection()
    {
        if(target != null)
        {
            float randomX = Random.Range(-10, 10);
            float randomZ = Random.Range(-10, 10);
            Vector3 targetVector = target.transform.position + new Vector3(randomX, 0, randomZ);

            return targetVector - this.transform.position;
        }
        else
        {
            return Vector3.zero;
        }

    }

    void FindClosestBall()
    { 

        Collider[] collisions = Physics.OverlapSphere(this.transform.position, Mathf.Infinity);

        List<Collider> poolCollisions = new List<Collider>();
        foreach(Collider candidate in collisions){

            if(candidate.GetComponent<PoolBallController>() != null)
            {
                if(candidate.gameObject != this.gameObject)
                {
                    poolCollisions.Add(candidate);
                }

            }

        }

        Collider col = poolCollisions[Random.Range(0, poolCollisions.Count - 1)];
        
        if (col.gameObject != this.gameObject)
            {
            
                if (target == null)
                {
                    target = col.gameObject;
                   // Debug.Log("New Target! " + col.name);
                    return;
                }

            }
        
    }
}
