using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string nextScene;

    public void StartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }

}