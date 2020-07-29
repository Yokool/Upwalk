using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class TurnImageManager : MonoBehaviour
{

    private static TurnImageManager instance;
    public static TurnImageManager INSTANCE => instance;

    private Image image;

    private void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
    }

    public void UpdateTurnSprite()
    {
        Turn currentTurn = TurnSystem.INSTANCE.CurrentTurn;
        
        switch (currentTurn)
        {
            case Turn.EASY:
                image.sprite = GameSprites.Turn_Easy;
                break;
            case Turn.MEDIUM:
                image.sprite = GameSprites.Turn_Medium;
                break;
            case Turn.HARD:
                image.sprite = GameSprites.Turn_Hard;
                break;
            case Turn.FULL:
                image.sprite = GameSprites.Turn_Full;
                break;
        }


    }
}
