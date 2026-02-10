using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMapScene : MonoBehaviour
{
    public SwitchScene switchScene;

    void Start()
    {
        switchScene.fade.SetActive(false);
        StartCoroutine(switchScene.EnterScene());
    }
}
