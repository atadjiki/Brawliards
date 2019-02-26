using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera playerCam;
    public Cinemachine.CinemachineVirtualCamera gameCam;

    public static CameraManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void Init()
    {
        if (playerCam == null)
        {
            playerCam = GameObject.Find("CM_Main").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        }
        if(gameCam == null)
        {
            gameCam = GameObject.Find("CM_General").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        }
    }

    public void SwitchToPlayer() {

        gameCam.enabled = false;
        playerCam.enabled = true;
    }

    public void SwitchToGame()
    {
        gameCam.enabled = true;
        playerCam.enabled = false;
    }
}
