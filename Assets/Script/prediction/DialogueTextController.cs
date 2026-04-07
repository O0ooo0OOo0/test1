using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTextController : MonoBehaviour
{
    public TMP_Text text; 
    public float maxWidth = 1085.01f;

    private void Update()
    {
        if (text != null)
        {
            string textT = text.text;
            if (string.IsNullOrEmpty(textT))
            {
                return;
            }

            int lineCount = textT.Split('\n').Length;

            for (int i = 0; i < lineCount; i++)
            {
                string currentLine = textT.Split('\n')[i];
                if (text.GetPreferredValues(currentLine).x > maxWidth)
                {
                    int wrapIndex = FindWrapIndex(currentLine, maxWidth);
                    if (wrapIndex > 0)
                    {
                        textT = textT.Insert(textT.IndexOf(currentLine) + wrapIndex, "\n");
                    }
                }
            }
            text.text = textT;
        }
    }

    private int FindWrapIndex(string line, float maxWidth)
    {
        int wrapIndex = -1;

        for (int i = 0; i < line.Length; i++)
        {
            string substring = line.Substring(0, i + 1);
            if (text.GetPreferredValues(substring).x > maxWidth)
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
