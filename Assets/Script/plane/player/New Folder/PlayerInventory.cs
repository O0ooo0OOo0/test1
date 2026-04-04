using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    private int cherryCount = 0;
    [SerializeField] private TextMeshProUGUI cherryText;

    public void AddCherry(int amount)
    {
        cherryCount += amount;
        if (cherryText != null)
        {
            cherryText.text = "集卡： " + cherryCount;
        }
        Debug.Log($"获得 {amount} 个樱桃，总计: {cherryCount}");
    }
}