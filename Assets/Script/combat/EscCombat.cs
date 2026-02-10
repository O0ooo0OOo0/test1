using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscCombat : MonoBehaviour
{
    public GameObject escPanel;
    public Button continueButton;
    public Button endButton;

    public GameObject black;
    public float fadeInSpeed = 1f;

    void Start()
    {
        escPanel.SetActive(false);
        black.SetActive(false);

        if (continueButton != null )
        {
            continueButton.onClick.AddListener(ContinueCombat);
        }
        if (endButton != null)
        {
            endButton.onClick.AddListener(EndCombat);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenEscPanel();
        }
    }

    public void OpenEscPanel()
    {
        escPanel.SetActive(true);
    }

    public void ContinueCombat()
    {
        escPanel.SetActive(false);
    }

    public void EndCombat()
    {
        escPanel.SetActive(false);
        StartCoroutine(ExitScene());
    }

    public IEnumerator ExitScene()
    {
        black.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeInSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 1f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        SceneManager.LoadScene("map");
    }
}
