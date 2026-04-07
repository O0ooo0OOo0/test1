using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CancelInput : MonoBehaviour
{
    public Button cancelInput;
    public TMP_Text inputContent; 
    public List<InputMaterial> inputMaterials;

    private void Start()
    {
        if (cancelInput != null)
        {
            cancelInput.onClick.AddListener(RefreshInputs);
        }
    }

    public void RefreshInputs()
    {
        inputContent.text = "";

        // 遍历所有物品按钮，恢复初始状态
        foreach (var material in inputMaterials)
        {
            if (material != null)
            {
                material.amount += material.inputAmount;
                material.inputAmount = 0;
                material.UpdateMatA();
                material.CurrentMat();
            }
        }

        InputMaterial.allMaterials.Clear();   // 清除已投入材料字典中的数据
    }
}