using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateManager : MonoBehaviour
{
    private Rigidbody2D rb;
    public string mainSceneName = "main";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;   // 注册场景加载事件
        CheckSceneAndUpdateBodyType();   // 当前场景立即检测一次
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;   // 取消注册，防止内存泄漏
    }

    // 场景加载完成时调用
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckSceneAndUpdateBodyType();
    }

    // 检查场景并更新刚体类型
    private void CheckSceneAndUpdateBodyType()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        bool isMainScene;

        if (currentScene == mainSceneName)
        {
            isMainScene = true;
        }
        else
        {
            isMainScene = false;
        }  

        if (!isMainScene)
        {
            // 不是主场景，设置为Dynamic（可物理运动）
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.simulated = true;  // 确保模拟开启
        }
        else
        {
            // 是主场景，设置为Static（静止）
            rb.bodyType = RigidbodyType2D.Static;
            //rb.velocity = Vector2.zero;  // 清除速度
            //rb.angularVelocity = 0f;     // 清除角速度
        }
    }
}
