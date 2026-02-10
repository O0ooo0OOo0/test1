using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class LevelGroup
{
    public Button stageButton;
    public string levelName;
}

public class EnterStage : MonoBehaviour
{
    public List<LevelGroup> levelGroups;
    public GameObject black;
    public float fadeSpeed = 1f;

    void Start()
    {
        black.SetActive(false);

        foreach (var group in levelGroups)
        {
            if (group.stageButton != null)
            {
                group.stageButton.onClick.AddListener(() => CombatLevel(group.levelName));
            }
        }
    }

    public void CombatLevel(string levelName)
    {
        StartCoroutine(ExitScene(levelName));
    }

    IEnumerator ExitScene(string levelName)
    {
        black.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 1f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        SceneManager.LoadScene(levelName);
    }
}