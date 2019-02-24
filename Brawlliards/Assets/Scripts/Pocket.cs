using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    [SerializeField]
    private static float death_seconds = 1f;

    private GameObject player;

    private void Awake()
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

        GameObject vfx = Instantiate<GameObject>(Resources.Load<GameObject>("VFX/BigExplosionEffect"));
        vfx.transform.position = ballpos;
        vfx.transform.localScale = new Vector3(3, 3, 3);

        vfx.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(death_seconds);
       // vfx.GetComponent<ParticleSystem>().Stop();
        


        if (player == ball)
        {
            Debug.Log("You died!");
            GameManager.instance.PlayerDead();
        }
        else
        {
            Debug.Log("Ball pocketed! " + ball.name);
            if(ball.GetComponent<PoolBallController>() != null && ball.GetComponent<PoolBallController>().lastCollidedWith != null)
            {
                ball.GetComponent<PoolBallController>().lastCollidedWith.incrementKills();
                Debug.Log(ball.GetComponent<PoolBallController>().lastCollidedWith.name + " has " +
                ball.GetComponent<PoolBallController>().lastCollidedWith.getKills().ToString() + "kills!");
            }
            else if(ball.GetComponent<PoolBallController>() != null && ball.GetComponent<PoolBallController>().lastCollidedWith == null)
            {
                Debug.Log(ball.name + " commited suicide!");
            }
        }

        Destroy(vfx);
        Destroy(ball);
    }
}