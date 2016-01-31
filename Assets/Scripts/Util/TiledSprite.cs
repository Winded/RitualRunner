using UnityEngine;
using System.Collections;

public class TiledSprite : MonoBehaviour
{
    public bool TileX = true;
    public bool TileY = true;
    public float ScaleX = 1f;
    public float ScaleY = 1f;

    void Awake()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var spriteWidth = renderer.sprite.bounds.size.x * ScaleX;
        var spriteHeight = renderer.sprite.bounds.size.y * ScaleY;
        var maxWidth = renderer.bounds.size.x;
        var maxHeight = renderer.bounds.size.y;
        var startX = transform.position.x - (maxWidth / 2);
        var startY = transform.position.y - (maxHeight / 2);

        var spritePrefab = new GameObject();
        var pRenderer = spritePrefab.AddComponent<SpriteRenderer>();
        spritePrefab.transform.position = transform.position;
        spritePrefab.transform.localScale = new Vector3(ScaleX, ScaleY, 1);
        pRenderer.sprite = renderer.sprite;
        pRenderer.sortingOrder = renderer.sortingOrder;

        int i = 0;
        for(float width = spriteWidth; width <= maxWidth; width += spriteWidth)
            for(float height = spriteHeight; height <= maxHeight; height += spriteHeight)
            {
                var x = startX + width - (spriteWidth / 2);
                var y = startY + height - (spriteHeight / 2);
                var sprite = Instantiate(spritePrefab) as GameObject;
                sprite.name = "Sprite" + i;
                sprite.transform.parent = transform;
                sprite.transform.position = new Vector3(x, y, 0);
                i++;
            }

        Destroy(spritePrefab);

        renderer.enabled = false;
    }

}