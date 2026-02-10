using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public GameObject fade;
    public float fadeInSpeed = 1f;

    public IEnumerator EnterScene()
    {
        fade.SetActive(true);
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            fade.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 0f;
        fade.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        fade.SetActive(false);
    }

    public IEnumerator ExitScene()
    {
        fade.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeInSpeed;
            fade.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 1f;
        fade.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
    }
}
