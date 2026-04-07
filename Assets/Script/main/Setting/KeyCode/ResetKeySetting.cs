using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetKeySetting : MonoBehaviour
{
    public Button reset;  // 重置button

    // 调用其他脚本
    public KeyChangeSetting keyChangeSetting;

    void Start()
    {
        if (reset != null)
        {
            reset.onClick.AddListener(ResetKeysToDefaults);
        }
    }

    private void ResetKeysToDefaults()
    {
        GameKeyManager.gkm.run = KeyCode.LeftShift;
        GameKeyManager.gkm.jump = KeyCode.UpArrow;
        GameKeyManager.gkm.dash = KeyCode.Space;
        GameKeyManager.gkm.interaction = KeyCode.Z;
        GameKeyManager.gkm.grab = KeyCode.X;
        GameKeyManager.gkm.map = KeyCode.Tab;
        GameKeyManager.gkm.item = KeyCode.Q;
        GameKeyManager.gkm.cards = KeyCode.F;
        GameKeyManager.gkm.book = KeyCode.E;
        GameKeyManager.gkm.forging = KeyCode.LeftAlt;

        PlayerPrefs.SetString("runKey", GameKeyManager.gkm.run.ToString());
        PlayerPrefs.SetString("jumpKey", GameKeyManager.gkm.jump.ToString());
        PlayerPrefs.SetString("dashKey", GameKeyManager.gkm.dash.ToString());
        PlayerPrefs.SetString("interactionKey", GameKeyManager.gkm.interaction.ToString());
        PlayerPrefs.SetString("grabKey", GameKeyManager.gkm.grab.ToString());
        PlayerPrefs.SetString("mapKey", GameKeyManager.gkm.map.ToString());
        PlayerPrefs.SetString("itemKey", GameKeyManager.gkm.item.ToString());
        PlayerPrefs.SetString("cardsKey", GameKeyManager.gkm.cards.ToString());
        PlayerPrefs.SetString("bookKey", GameKeyManager.gkm.book.ToString());
        PlayerPrefs.SetString("forgingKey", GameKeyManager.gkm.forging.ToString());

        UpdateUIWithDefaultKeys();
    }

    private void UpdateUIWithDefaultKeys()
    {
        if (keyChangeSetting != null)
        {
            keyChangeSetting.RefreshKeyBindings(); 
            keyChangeSetting.ClearAllChoseTables();
        }
    }
}
