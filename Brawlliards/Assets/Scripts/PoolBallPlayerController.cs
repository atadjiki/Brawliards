using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBallPlayerController : MonoBehaviour
{

    PoolBallController poolBallController;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
       if(poolBallController == null)
        {
            if(this.gameObject.GetComponent<PoolBallController>() == null)
            {
                poolBallController = this.gameObject.AddComponent<PoolBallController>();
            }
            else
            {
                poolBallController = this.gameObject.GetComponent<PoolBallController>();
            }

        }

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
    }

    void GetInput()
    {

        poolBallController.DoSphereMotion(rb, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        poolBallController.DoForwardMotion(rb, Input.GetKeyDown(KeyCode.LeftShift));


    }
}
