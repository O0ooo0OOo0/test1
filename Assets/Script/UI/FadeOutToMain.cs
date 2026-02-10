using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutToMain : MonoBehaviour
{
    // 退出场景UI
    public GameObject fadeOut;
    public float fadeOutSpeed = 1f;

    // 调用全局静态变量下的function物体
    private PlayerManager player;
    private Transform function;

    void Start()
    {
        player = PlayerManager.instance;
        function = player.transform.Find("function");   // player路径下的命名为function的子物体
    }

    private void Update()
    {
        if (function.GetComponent<escManager>().isFadeOut == true)
        {
            function.GetComponent<escManager>().isFadeOut = false;
            StartCoroutine(ExitSceneToMain());
        }
    }

    IEnumerator ExitSceneToMain()
    {
        fadeOut.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeOutSpeed;
            fadeOut.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 1f;
        fadeOut.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        SceneManager.LoadScene("main");
    }
}
