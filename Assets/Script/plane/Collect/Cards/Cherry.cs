using UnityEngine;

public class Cherry : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 碰到玩家时
        if (other.CompareTag("Player"))
        {
            // 让玩家增加樱桃数
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddCherry(value);
            }

            // 销毁自己
            Destroy(gameObject);
        }
    }
}