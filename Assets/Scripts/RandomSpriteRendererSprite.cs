using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteRendererSprite : MonoBehaviour
{

    private ISpriteListProvider spriteList;

    private SpriteRenderer spriteRenderer;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteList = GetComponent<ISpriteListProvider>();
        SetRandomSprite();

    }

    public void SetRandomSprite()
    {
        spriteRenderer.sprite = RandomWeightUtility.Pick(spriteList.GetSprites());
    }



}
