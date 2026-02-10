using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PredictionChange : MonoBehaviour
{
    public Button prediction;
    public Button close_prediction;
    public GameObject predictionPanel;
    public GameObject black;
    public float fadeInSpeed = 1f;
    public float add;
    public GameObject yuyan;
    private Animator yuyan_ani;

    void Start()
    {
        black.SetActive(false);
        predictionPanel.SetActive(false);
        yuyan_ani = yuyan.GetComponent<Animator>();

        if (prediction != null)
        {
            prediction.onClick.AddListener(OpenPrePanel);
        }
        if (close_prediction != null)
        {
            close_prediction.onClick.AddListener(ClosePrePanel);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            yuyan_ani.SetBool("isDialog", true);
        }
    }

    public void OpenPrePanel()
    {
        StartCoroutine(EnterPanel());
    }

    public void ClosePrePanel()
    {
        yuyan_ani.SetBool("isDialog", false);
        StartCoroutine(ExitPanel());
    }

    IEnumerator EnterPanel()
    {
        black.SetActive(true);
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * (fadeInSpeed + add);
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        predictionPanel.SetActive(true);

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 0f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        black.SetActive(false);
    }

    IEnumerator ExitPanel()
    {
        black.SetActive(true);
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * (fadeInSpeed + add);
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        predictionPanel.SetActive(false);

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 0f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        black.SetActive(false);
    }
}
