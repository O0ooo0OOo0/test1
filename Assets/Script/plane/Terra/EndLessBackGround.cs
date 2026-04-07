using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class EndLessBackGround : MonoBehaviour
{
    [Header("endless")]
    public GameObject camera;

    public float mapWidth;
    public int mapNum;

    private float TotalWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("camera");//查找标签

        //mapWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;//获取图像宽度
        mapWidth = GetComponent<Renderer>().bounds.size.x;//获取图像宽度
        TotalWidth = mapWidth * mapNum;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempPos = transform.position;
        if (camera.transform.position.x > transform.position.x + TotalWidth)
        {
            tempPos.x += TotalWidth;//向右平移
            transform.position = tempPos;//更新
        }
        else if (camera.transform.position.x < transform.position.x + TotalWidth)
        {
            tempPos.x += TotalWidth;//向左平移
            transform.position = tempPos;//更新
        }
    }
}
