using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthImageUpdater : MonoBehaviour
{

    private static HealthImageUpdater instance;
    public static HealthImageUpdater INSTANCE
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<HealthImageUpdater>();
                instance.CustomAwake();
                instance.CustomEnable();
            }
            return instance;
        }
    }

    private Image image;

    private void Awake()
    {
        CustomAwake();
    }

    private void OnEnable()
    {
        CustomEnable();
    }

    private void CustomAwake()
    {
        instance = this;
    }

    private void CustomEnable()
    {
        image = GetComponent<Image>();
    }

    public void UpdateImage()
    {
        int health = PlayerScript.INSTANCE.gameObject.GetComponent<HealthComponent>().Health;
        Debug.Log("H:" + health);
        string spriteName = $"Heart_{health}";
        Sprite pickedSprite = typeof(GameSprites).GetField(spriteName).GetValue(null) as Sprite;

        image.sprite = pickedSprite;

    }
}
