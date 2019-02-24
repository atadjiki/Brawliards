using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    [SerializeField]
    private static float death_seconds = 1.5f;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("On trigger enter");
        if(other.gameObject.GetComponent<PoolBallController>() != null)
        {
            IEnumerator coroutine = KillBall(other.gameObject);
            StartCoroutine(coroutine);

        }
    }
    IEnumerator KillBall(GameObject ball)
    {
        yield return new WaitForSeconds(death_seconds);

        if (player == ball)
        {
            Debug.Log("You died!");
            GameManager.instance.PlayerDead();
        }
        else
        {
            Debug.Log("Ball pocketed! " + ball.name);
            GameManager.instance.incrementKills();
        }

        Destroy(ball);
    }
}