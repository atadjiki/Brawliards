using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        Debug.Log("Loading game");
        SceneManager.LoadScene("Game");
    }

    public void SettingsButton()
    {
       // ToggleSettingsPanel();
    }

    public void ExitButton()
    {
        Application.Quit(0); 
    }
}
