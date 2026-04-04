using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseShader : MonoBehaviour
{
    [Header("目标材质")]
    [SerializeField] private Material targetMaterial;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;

    private bool isActive = true;

    void Start()
    {
        // 关键：获取材质实例（不是共享材质）
        var renderer = GetComponent<Renderer>();

    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isActive = !isActive;
            targetMaterial.SetFloat("_Active", isActive ? 1f : 0f);
            Debug.Log($"Shader Effect: {(isActive ? "ON" : "OFF")}");
        }
    }
}

