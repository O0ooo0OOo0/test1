using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTextController : MonoBehaviour
{
    public TMP_Text textMeshPro; 
    public float maxWidth = 1085.01f; 

    private void Update()
    {
        if (textMeshPro != null)
        {
            string text = textMeshPro.text;
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            int lineCount = text.Split('\n').Length;

            for (int i = 0; i < lineCount; i++)
            {
                string currentLine = text.Split('\n')[i];
                if (textMeshPro.GetPreferredValues(currentLine).x > maxWidth)
                {
                    int wrapIndex = FindWrapIndex(currentLine, maxWidth);
                    if (wrapIndex > 0)
                    {
                        text = text.Insert(text.IndexOf(currentLine) + wrapIndex, "\n");
                    }
                }
            }
            textMeshPro.text = text;
        }
    }

    private int FindWrapIndex(string line, float maxWidth)
    {
        int wrapIndex = -1;

        for (int i = 0; i < line.Length; i++)
        {
            string substring = line.Substring(0, i + 1);
            if (textMeshPro.GetPreferredValues(substring).x > maxWidth)
            {
                wrapIndex = i - 1;
                if (wrapIndex > 0)
                {
                    break;
                }
            }
        }
        return wrapIndex;
    }
}
