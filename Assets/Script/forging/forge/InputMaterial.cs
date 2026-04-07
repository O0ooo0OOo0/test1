using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameFramework.Samples.Localization;

public class InputMaterial : MonoBehaviour
{
    public Button materials;   // 投入材料对应按键
    public int materialType;   // 材料类别
    public string materialNameCN, materialNameEN;   // 材料名称
    public string materialName;

    public int amount;   // 材料当前数量
    public int inputAmount;   // 材料投入数量

    public TMP_Text materialContent;   // 材料剩余数量文本显示
    public TMP_Text inputContent;   // 材料投入数量文本显示

    public static Dictionary<string, int> allMaterials = new Dictionary<string, int>();    // 投入的全部材料字典

    void Start()
    {
        MatName(LanguageType.lt.lanType);
        CurrentMat();

        inputAmount = 0;

        if (materials != null)   // 点击按键实现材料投入
        {
            materials.onClick.AddListener(InputMat);
        }
    }

    // 判断当前使用的语言类型（确定使用的材料名称）
    public void MatName(int lanT)
    {
        if (lanT == 0)
        {
            materialName = materialNameCN;
        }
        else if (lanT == 1)
        {
            materialName = materialNameEN;
        }
    }

    // 获取当前材料信息（数量+文本）
    public void CurrentMat()
    {
        // 材料数量
        if (materialType == 0)
        {
            amount = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].coins;
        }
        else if (materialType != 0)
        {
            amount = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].materials[materialType - 1].materialAmount;
        }

        // 显示文本
        materialContent.text = materialName + "×" + amount;
    }

    // 材料投入
    public void InputMat()
    {
        if (amount > 0)
        {
            // 数据处理
            inputAmount++;
            amount--;

            // 更新当前材料信息
            UpdateMatA();
            CurrentMat();

            // 更新投入材料信息
            UpdateMatType();
            UpdateInputMatContent();
        }
    }

    // 更新材料数量
    public void UpdateMatA()
    {
        var arc = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex];

        if (materialType == 0)
        {
            arc.coins = amount;
        }
        else if (materialType != 0)
        {
            var material = arc.materials[materialType - 1];
            material.materialAmount = amount;
            arc.materials[materialType - 1] = material;
        }

        ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex] = arc;
    }

    // 更新当前材料投入
    public void UpdateMatType()
    {
        if (allMaterials.ContainsKey(materialName))   // 如果材料已在字典中
        {
            allMaterials[materialName]++;
        }
        else   // 如果材料不在字典中
        {
            allMaterials.Add(materialName, 1);
        }
    }

    // 更新已投入的材料文本信息
    private void UpdateInputMatContent()
    {
        string displayContent = "";   // 初始化已投入材料信息
        foreach (var item in allMaterials)
        {
            displayContent += item.Key + "×" + item.Value + "\n";
        }
        displayContent = displayContent.TrimEnd('\n');    // 去掉多余的换行符
        inputContent.text = displayContent;   // 显示投入材料信息
    }
}
