using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthImageUpdater : MonoBehaviour
{

    private static HealthImageUpdater instance;
    public static HealthImageUpdater INSTANCE => instance;

    private Image image;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateImage()
    {
        int health = PlayerScript.INSTANCE.Health;
        image.sprite = typeof(GameSpritres).GetField($"Heart_{health}").GetValue(null) as Sprite;
    }

}
