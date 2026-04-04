// EquipmentData.cs
using System;
using System.Collections.Generic;

[Serializable]
public class EquipmentData
{
    public int maxSlots = 2;
    public List<string> equippedElementCardIds = new List<string>();
    public List<string> equippedOtherCardIds = new List<string>();
}