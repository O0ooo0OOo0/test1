using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputMaterial : MonoBehaviour
{
    public Button materials;
    public int materialType;
    public string materialName;
    public int amount;
    public int inputAmount;
    public TMP_Text materialContent;
    public TMP_Text inputContent;
    public static Dictionary<string, int> allMaterials = new Dictionary<string, int>();   

    public CoinsAmount coinsAmount;

    void Start()
    {
        DefineAmount();
        inputAmount = 0;
        materialContent.text = materialName + "×" + amount;

        if (materials != null)
        {
            materials.onClick.AddListener(InputMt);
        }
    }

    public void DefineAmount()
    {
        if (materialType == 0)
        {
            amount = PersistentObject.instance.coins;
        }
        else if (materialType != 0)
        {
            amount = PersistentObject.instance.material[materialType - 1];
        }
    }

    public void InputMt()
    {
        if (amount > 0)
        {
            inputAmount++;
            amount--;
            ChangeAmount();

            materialContent.text = materialName + "×" + amount;
            if (allMaterials.ContainsKey(materialName))
            {
                allMaterials[materialName]++;
            }
            else
            {
                allMaterials.Add(materialName, 1);
            }

            UpdateInputContent();
        }
    }

    private void UpdateInputContent()
    {
        string displayContent = "";
        foreach (var item in allMaterials)
        {
            displayContent += item.Key + "×" + item.Value + "\n";
        }
        displayContent = displayContent.TrimEnd('\n'); // 去掉多余的换行符
        inputContent.text = displayContent;
    }

    public void ChangeAmount()
    {
        if (materialType == 0)
        {
            PersistentObject.instance.coins = amount;
            coinsAmount.AmountCoins();
        }
        else if (materialType != 0)
        {
            PersistentObject.instance.material[materialType - 1] = amount;
        }
    }
}
