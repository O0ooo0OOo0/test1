// CardData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card System/Card")]
public class CardData : ScriptableObject
{
    public string cardId;        // "DoubleJump", "Dash", "Ice", "Fire"
    public string cardName;
    public Sprite icon;
    public CardType cardType;
}

public enum CardType
{
    Element,  // 占用槽位
    Other     // 独立能力
}