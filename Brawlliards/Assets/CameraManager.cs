using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera playerCam;
    public Cinemachine.CinemachineVirtualCamera gameCam;

    public static CameraManager instance = null;

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

        InitCameras();

    }

    void InitCameras()
    {
        if(playerCam == null)
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
