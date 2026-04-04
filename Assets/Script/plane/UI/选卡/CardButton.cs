// CardButton.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Button button;

    private CardData cardData;
    private bool isEquipped;
    private CardSelectionPanel panel;

    public void Init(CardData card, bool equipped, CardSelectionPanel ownerPanel)
    {
        cardData = card;
        isEquipped = equipped;
        panel = ownerPanel;

        iconImage.sprite = card.icon;
        nameText.text = card.cardName;
        statusText.text = equipped ? "✓ 已装备" : "";
        statusText.color = equipped ? Color.green : Color.clear;

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        panel.OnCardClicked(cardData, isEquipped);
    }
}