using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider2Dchange : MonoBehaviour
{
    private PolygonCollider2D poly;
    private new SpriteRenderer renderer;
    private Sprite lastSprite;

    void Awake()
    {
        if(!TryGetComponent(out poly))
        {
            poly = gameObject.AddComponent<PolygonCollider2D>();
        }
        renderer = GetComponent<SpriteRenderer>();
        lastSprite = renderer.sprite;
    }

    void Update()
    {
        if(renderer.sprite != lastSprite)
        {
            PolygonCollider2D lastPoly = gameObject.AddComponent<PolygonCollider2D>();
            poly.points = lastPoly.points;
            Destroy(lastPoly);
            lastSprite = renderer.sprite;
        }
    }
}
