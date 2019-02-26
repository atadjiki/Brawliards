using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    [SerializeField]
    private static float death_seconds = 1f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
    
        if(other.gameObject.GetComponent<PoolBallController>() != null)
        {
            IEnumerator coroutine = KillBall(other.gameObject);
            StartCoroutine(coroutine);

        }
    }
    IEnumerator KillBall(GameObject ball)
    {
      
        Vector3 ballpos = ball.transform.position;
        ball.SetActive(false);

        StartCoroutine(EffectsManager.instance.DoExplosion(ball, death_seconds));
        


        if (player == ball)
        {
            Debug.Log("You died!");
            GameManager.instance.PlayerDead();
        }
        else
        {
           
            if(ball.GetComponent<PoolBallController>() != null && ball.GetComponent<PoolBallController>().lastCollidedWith != null)
            {
                ball.GetComponent<PoolBallController>().lastCollidedWith.incrementKills();
                Debug.Log(ball.GetComponent<PoolBallController>().lastCollidedWith.name + " has " +
                ball.GetComponent<PoolBallController>().lastCollidedWith.getKills().ToString() + " kills!");
                GameManager.instance.DeRegisterPoolBall(ball.GetComponent<PoolBallController>());
            }
            else if(ball.GetComponent<PoolBallController>() != null && ball.GetComponent<PoolBallController>().lastCollidedWith == null)
            {
                Debug.Log(ball.name + " commited suicide!");
                UIManager.instance.SetMessage(ball.name + " commited suicide!");
                GameManager.instance.DeRegisterPoolBall(ball.GetComponent<PoolBallController>());
            }

            yield return new WaitForSeconds(1);
            Destroy(ball);
        }
      
    }
}