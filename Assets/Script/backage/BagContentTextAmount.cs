using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagContentTextAmount : MonoBehaviour
{
    public TMP_Text[] propText;
    public TMP_Text[] talismanText;
    public TMP_Text[] targetCardText;
    public TMP_Text[] numberCardText;
    public TMP_Text[] elementCardText;

    void Start()
    {
        AmountText();
    }

    private void Update()
    {
        AmountText();
    }

    public void AmountText()
    {
        for (int i = 0; i < propText.Length; i++)
        {
            propText[i].text = PersistentObject.instance.prop[i].ToString();
        }

        for (int i = 0; i < talismanText.Length; i++)
        {
            talismanText[i].text = PersistentObject.instance.talisman[i].ToString();
        }

        for (int i = 0; i < targetCardText.Length; i++)
        {
            if (i == 0)
            {
                targetCardText[0].text = "¡Þ";
            }
            else if (i != 0)
            {
                targetCardText[i].text = PersistentObject.instance.targetCard[i].ToString();
            }
        }

        for (int i = 0; i < numberCardText.Length; i++)
        {
            if (i == 0)
            {
                numberCardText[0].text = "¡Þ";
            }
            else if (i == 3)
            {
                numberCardText[3].text = "¡Þ";
            }
            else if (i != 0 && i != 3)
            {
                numberCardText[i].text = PersistentObject.instance.numberCard[i].ToString();
            }
        }

        for (int i = 0; i < elementCardText.Length; i++)
        {
            elementCardText[i].text = PersistentObject.instance.elementCard[i].ToString();
        }
    }
}
