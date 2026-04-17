using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionMananger : MonoBehaviour
{
    public Vector2 combat_2_1_Position = new Vector2(0, 0);   // 出生点

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    // 场景加载完成时调用
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "combat_2_1")
        {
            float z = transform.position.z;
            transform.position = new Vector3(combat_2_1_Position.x, combat_2_1_Position.y, z);
        }
    }
}
