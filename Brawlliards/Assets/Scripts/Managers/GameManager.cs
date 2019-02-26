using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private bool playerAlive;
    private bool paused;
    private PoolBallController player;

    private HashSet<PoolBallController> poolBallControllers;
    private float sinceLastSpawn;
    private float spawnTime = 25f;
    private float ST_min = 5;
    private float ST_max = 30;
    public int maxBotsAllowed = 11;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        player = GameObject.Find("Player").GetComponent<PoolBallController>();

        Init();
    }

    public void Init()
    {

        poolBallControllers = new HashSet<PoolBallController>();
        player.gameObject.SetActive(true);
        player.gameObject.transform.position = GameObject.Find("SpawnPoint").transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        playerAlive = true;
        player.gameObject.GetComponent<PoolBallController>().ResetKills();
        CameraManager.instance.SwitchToPlayer();
        paused = false;
        Time.timeScale = 1;


        UIManager.instance.UpdateCounts(player.getKills(), poolBallControllers.Count);
        UIManager.instance.youDied.SetActive(false);
        sinceLastSpawn = Time.time;
        spawnTime = Random.Range(ST_min, ST_max);

    }

    public void PlayerDead()
    {
        playerAlive = false;
        CameraManager.instance.SwitchToGame();
        player.gameObject.SetActive(false);
        UIManager.instance.playerDead();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Init();
        }

        if (Input.GetKeyDown(KeyCode.P) && playerAlive)
        {
            if(paused == false)
            {
                paused = true;
                Time.timeScale = 0;
            }else if(paused == true)
            {
                paused = false;
                Time.timeScale = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBot();

        }

        UIManager.instance.UpdateCounts(player.getKills(), poolBallControllers.Count);


        CheckForSpawns();
    }

    public void SpawnBot()
    {
        if(poolBallControllers.Count >= maxBotsAllowed) { return; }
        GameObject bot = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bot"));
        bot.transform.position = GameObject.Find("SpawnPoint").transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        bot.GetComponent<Renderer>().material.color = Random.ColorHSV();
        Debug.Log("Spawned bot " + bot.name);
        UIManager.instance.SetMessage("Spawned bot " + bot.name);
    }

    public void RegisterPoolBall(PoolBallController ball)
    {
      //  Debug.Log("Registering ball " + ball.name);
        poolBallControllers.Add(ball);
    }

    public int DeRegisterPoolBall(PoolBallController ball)
    {
        poolBallControllers.Remove(ball);
        Debug.Log("Ball killed! " + ball.name);
        UIManager.instance.SetMessage("Ball killed! " + ball.name);
        return poolBallControllers.Count;
    }

    public void CheckForSpawns()
    {
        if(poolBallControllers.Count < 1)
        {
            SpawnBot();
            sinceLastSpawn = Time.time;
            spawnTime = Random.Range(10, 25);
            Debug.Log("Next bot spawns in " + spawnTime + " seconds");
            return;
        }

        if(Time.time - sinceLastSpawn >= spawnTime)
        {

            SpawnBot();
            sinceLastSpawn = Time.time;
            spawnTime = Random.Range(10, 25);
            Debug.Log("Next bot spawns in " + spawnTime + " seconds");
            return;
        }

    }


}
