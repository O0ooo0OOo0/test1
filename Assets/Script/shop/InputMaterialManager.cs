using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputMaterialManager : MonoBehaviour
{
    [System.Serializable]
    public struct InputMaterials
    {
        public Button material;
        public string nameMaterial;
        public int amount;
        public int inputAmount;
        public TMP_Text contentMaterial;
    }

    public List<InputMaterials> inputMaterials;
    public TMP_Text inputContent;
    public static Dictionary<string, int> allMaterials = new Dictionary<string, int>();

    public CoinsAmount coinsAmount;

    void Start()
    {
        InitializeData();

        foreach (var material in inputMaterials)
        {
            if (material.material != null)
            {
                material.material.onClick.AddListener(() => InputMt(material.amount, material.nameMaterial));
            }
        }
    }

    public void InitializeData()
    {
        for (int i = 0; i < inputMaterials.Count; i++)
        {
            InputMaterials material = inputMaterials[i];

            if (material.nameMaterial == "±´±Ò")
            {
                material.amount = PersistentObject.instance.material[0];
            }
            else if (material.nameMaterial == "ÖñÆ¬")
            {
                material.amount = PersistentObject.instance.material[1];
            }
            else if (material.nameMaterial == "ËéÍß")
            {
                material.amount = PersistentObject.instance.material[2];
            }
            else if (material.nameMaterial == "½ðÆá")
            {
                material.amount = PersistentObject.instance.material[3];
            }
            else if (material.nameMaterial == "Í­±Ò")
            {
                material.amount = PersistentObject.instance.coins;
            }

            material.inputAmount = 0;
            material.contentMaterial.text = material.nameMaterial + "¡Á" + material.amount;
            inputMaterials[i] = material;
        }
    }

    public void InputMt(int amountM, string nameM)
    {
        if (amountM > 0)
        {
            ChangeData(nameM);

            if (allMaterials.ContainsKey(nameM))
            {
                allMaterials[nameM]++;
            }
            else
            {
                allMaterials.Add(nameM, 1);
            }

            UpdateInputContent();
        }
    }

    private void UpdateInputContent()
    {
        string displayContent = "";
        foreach (var item in allMaterials)
        {
            displayContent += item.Key + "¡Á" + item.Value + "\n";
        }
        displayContent = displayContent.TrimEnd('\n'); // È¥µô¶àÓàµÄ»»ÐÐ·û
        inputContent.text = displayContent;
    }

    public void ChangeData(string name)
    {
        for (int i = 0; i < inputMaterials.Count; i++)
        {
            InputMaterials material = inputMaterials[i];

            if (material.nameMaterial == name)
            {
                material.inputAmount++;
                material.amount--;

                if (material.nameMaterial == "±´±Ò")
                {
                    PersistentObject.instance.material[0] = material.amount;
                }
                else if (material.nameMaterial == "ÖñÆ¬")
                {
                    PersistentObject.instance.material[1] = material.amount;
                }
                else if (material.nameMaterial == "ËéÍß")
                {
                    PersistentObject.instance.material[2] = material.amount;
                }
                else if (material.nameMaterial == "½ðÆá")
                {
                    PersistentObject.instance.material[3] = material.amount;
                }
                else if (material.nameMaterial == "Í­±Ò")
                {
                    PersistentObject.instance.coins = material.amount;
                    coinsAmount.AmountCoins();
                }

                material.contentMaterial.text = material.nameMaterial + "¡Á" + material.amount;
                inputMaterials[i] = material;
            }
            else
            {
                return;
            }
        }
    }
}
