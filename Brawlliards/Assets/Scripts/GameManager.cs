using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public Text killCount;
    public Text ballCount;
    public GameObject youDied;
    private bool playerAlive;
    private bool paused;
    private PoolBallController player;

    private HashSet<PoolBallController> poolBallControllers;
    private float sinceLastSpawn;
    private float spawnTime = 25f;
    private float ST_min = 5;
    private float ST_max = 30;
    public int maxBotsAllowed = 11;


    // Start is called before the first frame update
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        player = GameObject.Find("Player").GetComponent<PoolBallController>();
        poolBallControllers = new HashSet<PoolBallController>();
        InitGame();

    }

    void InitGame()
    {
        playerAlive = true;
        paused = false;
        Time.timeScale = 1;

        killCount = GameObject.Find("Kills_Value").GetComponent<Text>();
        killCount.text = player.getKills().ToString();
        youDied = GameObject.Find("Death");
        youDied.SetActive(false);
        sinceLastSpawn = Time.time;
        spawnTime = Random.Range(ST_min, ST_max);

    }

    public void PlayerDead()
    {
        playerAlive = false;
        Time.timeScale = 0;
        youDied.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

        killCount.text = player.getKills().ToString();
        ballCount.text = poolBallControllers.Count.ToString();

        CheckForSpawns();
    }

    public void SpawnBot()
    {
        if(poolBallControllers.Count >= maxBotsAllowed) { return; }
        GameObject bot = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bot"));
        bot.transform.position = GameObject.Find("SpawnPoint").transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        bot.GetComponent<Renderer>().material.color = Random.ColorHSV();
        Debug.Log("Spawned bot " + bot.name);
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
