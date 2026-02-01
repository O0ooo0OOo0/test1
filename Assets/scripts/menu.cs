using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public void GameStart()
    {
        SceneManager.LoadScene(0);
    }

public void GameExit()
    {
        Application.Quit();
    }
}
