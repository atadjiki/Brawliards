using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public GameObject youDied;
    public Text killCount;
    public Text ballCount;
    public Text message;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Init();
    }

    public void Init()
    {
        if(youDied == null)
        {
            youDied = GameObject.Find("Death");
        }
        if(killCount == null)
        {
            killCount = GameObject.Find("Kills_Value").GetComponent<Text>();
        }
        if(ballCount == null)
        {
            ballCount = GameObject.Find("Balls_Value").GetComponent<Text>();
        }
        if(message == null)
        {
            message = GameObject.Find("Message").GetComponent<Text>();
        }
        youDied.SetActive(false);

    }

    public void UpdateCounts(int kills, int balls)
    {
        killCount.text = kills.ToString();
        ballCount.text = balls.ToString();
    }

    public void playerDead()
    {
        UIManager.instance.youDied.gameObject.SetActive(true);
    }

    public void SetKillCount(int kills)
    {
        killCount.text = kills.ToString();
    }

    public void SetBallCount(int balls)
    {
        ballCount.text = balls.ToString();
    }

    public void SetMessage(string _string)
    {
        message.text = _string;
    }

}
