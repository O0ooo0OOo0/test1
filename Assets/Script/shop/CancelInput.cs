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
            cancelInput.onClick.AddListener(CancelAllInputs);
        }
    }

    public void CancelAllInputs()
    {
        inputContent.text = "";

        // 遍历所有物品按钮，恢复初始状态
        foreach (var material in inputMaterials)
        {
            if (material != null)
            {
                material.amount += material.inputAmount;
                material.inputAmount = 0;
                material.materialContent.text = material.materialName + "×" + material.amount;
                material.ChangeAmount();
            }
        }

        InputMaterial.allMaterials.Clear();
    }
}