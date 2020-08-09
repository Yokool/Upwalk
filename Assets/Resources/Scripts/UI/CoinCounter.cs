using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{

    private static CoinCounter instance;
    public static CoinCounter INSTANCE => instance;

    [SerializeField]
    private GameObject CoinCounter_10000;
    private Image Renderer_10000;

    [SerializeField]
    private GameObject CoinCounter_1000;
    private Image Renderer_1000;

    [SerializeField]
    private GameObject CoinCounter_100;
    private Image Renderer_100;

    [SerializeField]
    private GameObject CoinCounter_10;
    private Image Renderer_10;

    [SerializeField]
    private GameObject CoinCounter_1;
    private Image Renderer_1;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        Renderer_10000 = CoinCounter_10000.GetComponent<Image>();
        Renderer_1000 = CoinCounter_1000.GetComponent<Image>();
        Renderer_100 = CoinCounter_100.GetComponent<Image>();
        Renderer_10 = CoinCounter_10.GetComponent<Image>();
        Renderer_1 = CoinCounter_1.GetComponent<Image>();
    }

    public void CoinCounterUpdate()
    {
        int coinAmount = GameLifetimeManager.INSTANCE.GetCoins();

        int _10000 = coinAmount / 10000;
        coinAmount %= 10000;
        int _1000 = coinAmount / 1000;
        coinAmount %= 1000;
        int _100 = coinAmount / 100;
        coinAmount %= 100;
        int _10 = coinAmount / 10;
        coinAmount %= 10;
        int _1 = coinAmount;

        // An array would be faster, but lets stay consistent with the rest of the system
        Renderer_10000.sprite = typeof(GameSprites).GetField($"NumberFont_{_10000}").GetValue(null) as Sprite;
        Renderer_1000.sprite = typeof(GameSprites).GetField($"NumberFont_{_1000}").GetValue(null) as Sprite;
        Renderer_100.sprite = typeof(GameSprites).GetField($"NumberFont_{_100}").GetValue(null) as Sprite;
        Renderer_10.sprite = typeof(GameSprites).GetField($"NumberFont_{_10}").GetValue(null) as Sprite;
        Renderer_1.sprite = typeof(GameSprites).GetField($"NumberFont_{_1}").GetValue(null) as Sprite;

    }

}