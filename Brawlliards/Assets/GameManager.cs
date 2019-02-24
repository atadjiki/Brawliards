using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public Text killCount;
    public GameObject youDied;
    private float kills;
    private bool playerAlive;
    private bool paused;


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
           

        InitGame();

    }

    void InitGame()
    {
        kills = 0;
        playerAlive = true;
        paused = false;
        Time.timeScale = 1;

        killCount = GameObject.Find("Kills_Value").GetComponent<Text>();
        killCount.text = kills.ToString();
        youDied = GameObject.Find("Death");
        youDied.SetActive(false);

    }

    public float incrementKills()
    {
        kills++;
        killCount.text = kills.ToString();
        return kills;
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
    }


}
