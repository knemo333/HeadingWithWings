using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverQuitButton : MonoBehaviour
{
    public void OnClickQuitButton()
    {
        SceneManager.LoadScene("LobbyScene");
        Time.timeScale = 1;
    }
}