using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class GridSprites
{

    public static Sprite Ground_Black;
    public static Sprite Ground_Flowers;
    public static Sprite Ground_WhiteStripes;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void LoadSprites()
    {
        Ground_Black = Resources.Load<Sprite>("Ground/Ground_Black");
        Ground_Flowers = Resources.Load<Sprite>("Ground/Ground_Flowers");
        Ground_WhiteStripes = Resources.Load<Sprite>("Ground/Ground_WhiteStripes");
        Verify();
    }

    private static void Verify()
    {

        FieldInfo[] fields = typeof(GridSprites).GetFields();


        foreach(FieldInfo field in fields)
        {
            Sprite sprite = field.GetValue(null) as Sprite;
            if(sprite == null)
            {
                Debug.LogError($"GridSprites.{sprite.name} field is missing a value. (Sprite is missing)");
            }
        }

    }


}
