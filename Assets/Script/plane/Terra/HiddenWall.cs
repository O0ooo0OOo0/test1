using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class SimpleHiddenWall : MonoBehaviour
{
    [SerializeField] private Tilemap wallTilemap;  // 要隐藏的墙
    [SerializeField] private float fadeSpeed = 2f; // 渐隐速度

    private bool isFading = false;
    private TilemapRenderer wallRenderer;
    private Material material;

    private void Start()
    {
        if (wallTilemap == null)
            wallTilemap = GetComponent<Tilemap>();

        wallRenderer = wallTilemap.GetComponent<TilemapRenderer>();

        material = Instantiate(wallRenderer.material);  // 创建材质实例
        wallRenderer.material = material;  // 应用材质
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 只要玩家碰到触发器就开始渐隐
        if (other.CompareTag("Player") && !isFading)
        {
            isFading = true;
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        Color color = material.color;

        while (color.a > 0)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            material.color = color;
            yield return null;
        }
    }
}