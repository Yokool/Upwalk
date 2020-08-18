using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{

    private static CoinCounter instance;
    public static CoinCounter INSTANCE => instance;

    [SerializeField]
    private GameObject CoinCounter_100000;

    [SerializeField]
    private GameObject CoinCounter_10000;

    [SerializeField]
    private GameObject CoinCounter_1000;

    [SerializeField]
    private GameObject CoinCounter_100;

    [SerializeField]
    private GameObject CoinCounter_10;

    [SerializeField]
    private GameObject CoinCounter_1;


    private List<Image> counters;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        counters = new List<Image>();
        counters.Add(CoinCounter_1.GetComponent<Image>());
        counters.Add(CoinCounter_10.GetComponent<Image>());
        counters.Add(CoinCounter_100.GetComponent<Image>());
        counters.Add(CoinCounter_1000.GetComponent<Image>());
        counters.Add(CoinCounter_10000.GetComponent<Image>());
        counters.Add(CoinCounter_100000.GetComponent<Image>());
    }

    public void CoinCounterUpdate()
    {
        int coinAmount = GameLifetimeManager.INSTANCE.GetCoins();

        AttachValuesToCounter(coinAmount, counters);

    }

    public static void AttachValuesToCounter(int value, List<Image> counters)
    {

        for(int i = counters.Count - 1; i >= 0; --i)
        {

            Image counter = counters[i];

            int zeroes = (int)System.Math.Pow(10.0, (double)i);

            int counterValue = 0;

            if(i != 0)
            {
                counterValue = value / zeroes;
                value %= zeroes;
            }
            else
            {
                counterValue = value;
            }

            counter.sprite = typeof(GameSprites).GetField($"NumberFont_{counterValue}").GetValue(null) as Sprite;

        }

    }

}