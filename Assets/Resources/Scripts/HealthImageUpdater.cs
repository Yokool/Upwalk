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
        int health = PlayerScript.INSTANCE.gameObject.GetComponent<HealthComponent>().Health;

        string spriteName = $"Heart_{health}";
        Sprite pickedSprite = typeof(GameSprites).GetField(spriteName).GetValue(null) as Sprite;

        image.sprite = pickedSprite;

    }
}
