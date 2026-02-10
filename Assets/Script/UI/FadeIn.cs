using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    // ½øÈë³¡¾°UI
    public GameObject fadeIn;
    public float fadeInSpeed = 1f;

    void Start()
    {
        fadeIn.SetActive(true);
        StartCoroutine(EnterScene());
    }

    IEnumerator EnterScene()
    {
        fadeIn.SetActive(true);
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            fadeIn.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 0f;
        fadeIn.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        fadeIn.SetActive(false);
    }
}
