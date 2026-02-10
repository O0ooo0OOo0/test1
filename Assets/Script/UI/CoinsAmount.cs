using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsAmount : MonoBehaviour
{
    public TMP_Text coinsText;

    void Start()
    {
        AmountCoins();
    }

    public void AmountCoins()
    {
        coinsText.text = PersistentObject.instance.coins.ToString();
    }
}
