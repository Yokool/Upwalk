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

        SpriteChance[] possibleSprites = spriteList.GetSprites();

        int totalWeight = 0;


        foreach (SpriteChance spriteChance in possibleSprites)
        {
            totalWeight += spriteChance.weight;
        }

        int generatedNumber = Random.Range(0, totalWeight);

        Sprite pickedSprite = null;

        List<Sprite> list = new List<Sprite>();

        for (int i = 0; i < possibleSprites.Length; ++i)
        {
            SpriteChance spriteChance = possibleSprites[i];

            Sprite sprite = spriteChance.sprite;

            for (int j = 1; j <= spriteChance.weight; ++j)
            {
                list.Add(sprite);
            }

        }


        pickedSprite = list[generatedNumber];

        spriteRenderer.sprite = pickedSprite;


    }



}
