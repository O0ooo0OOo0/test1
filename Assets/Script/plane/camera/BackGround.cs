using UnityEngine;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public class Layer
    {
        public Transform bg;        // 背景物体
        public float ratio = 1f;    // 移动比例
        public bool reverse = false; // 反向移动
    }

    public Transform target;        // 跟随目标
    public List<Layer> layers = new List<Layer>();

    private Vector2 lastPos;

    void Start()
    {
        lastPos = target.position;
    }

    void Update()
    {
        // 计算移动距离
        Vector2 delta = (Vector2)target.position - lastPos;

        // 更新每个背景层
        foreach (Layer layer in layers)
        {
            if (layer.bg == null) continue;

            float speed = layer.reverse ? -layer.ratio : layer.ratio;
            Vector3 move = new Vector3(delta.x * speed, delta.y * speed, 0);
            layer.bg.position += move;
        }

        // 更新相机跟随
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        lastPos = target.position;
    }
}