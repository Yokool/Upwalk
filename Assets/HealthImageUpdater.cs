using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthImageUpdater : MonoBehaviour
{

    private static HealthImageUpdater instance;
    public static HealthImageUpdater INSTANCE => instance;

    private Image image;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void UpdateImage()
    {
        int health = PlayerScript.INSTANCE.Health;

        string spriteName = $"Heart_{health}";
        image.sprite = typeof(GameSprites).GetField(spriteName).GetValue(null) as Sprite;
    }

}
