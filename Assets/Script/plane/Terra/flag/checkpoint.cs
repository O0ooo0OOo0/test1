using UnityEngine;

public class checkpoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //检查点
        if (collision.gameObject.CompareTag("Player"))
        {
            Life pd = collision.GetComponent<Life>();//获取 位置
            pd.pos = transform.position;

        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
