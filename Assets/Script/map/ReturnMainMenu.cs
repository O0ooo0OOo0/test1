using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnMenu();
        }
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("main");
    }
}
