using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;  // 引入 TextMeshPro 命名空间

public class Collect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int cherry = 0;
    private Animator anim;
    [SerializeField] private TextMeshProUGUI cherryText;  // 改为 TextMeshProUGUI 类型

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集樱桃
        if(collision.gameObject.CompareTag("cherry"))//tag是cherry
        {
            Destroy(collision.gameObject);
            cherry++;
            cherryText.text = "集卡：" + cherry;

        }

        //收集二段跳卡
        if (collision.gameObject.CompareTag("Card_DoubleJump"))
        {
            Destroy(collision.gameObject);
            playerjump playerjump = GetComponent<playerjump>();
            playerjump.HasDoubleJump = true;
            Debug.Log("获得二段跳能力");

        }

        // 战斗
        //if (collision.gameObject.CompareTag("npc"))//如果碰到敌人标签的物体
        //{
            // 切换到战斗场景
         //   SceneManager.LoadScene(2);
        //}


    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
