using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class GameSpritres
{

    public static Sprite Ground_Black;
    public static Sprite Ground_Flowers;
    public static Sprite Ground_WhiteStripes;

    // DO NOT CHANGE FIELD NAMES
    // DEPENDENCY -- WALL SPRITE ADJUSTER
    // The Suffix represents if there is
    // another wall next to the sprite
    // The naming convention follows the:
    // Never Eat Sea Weed rule
    // NESW
    public static Sprite Wall;
    public static Sprite Wall_N;
    public static Sprite Wall_S;
    public static Sprite Wall_E;
    public static Sprite Wall_W;
    public static Sprite Wall_EW;
    public static Sprite Wall_NS;
    public static Sprite Wall_NESW;
    public static Sprite Wall_ESW;
    public static Sprite Wall_NE;
    public static Sprite Wall_NES;
    public static Sprite Wall_NEW;
    public static Sprite Wall_NSW;
    public static Sprite Wall_SW;
    public static Sprite Wall_NW;
    public static Sprite Wall_ES;

    public static Sprite Health_3;
    public static Sprite Health_2;
    public static Sprite Health_1;
    public static Sprite Health_0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void LoadSprites()
    {
        Ground_Black = Resources.Load<Sprite>("Ground/Ground_Black");
        Ground_Flowers = Resources.Load<Sprite>("Ground/Ground_Flowers");
        Ground_WhiteStripes = Resources.Load<Sprite>("Ground/Ground_WhiteStripes");

        Wall = Resources.Load<Sprite>("Wall/Wall");
        Wall_N = Resources.Load<Sprite>("Wall/Wall_N");
        Wall_E = Resources.Load<Sprite>("Wall/Wall_E");
        Wall_S = Resources.Load<Sprite>("Wall/Wall_S");
        Wall_W = Resources.Load<Sprite>("Wall/Wall_W");
        Wall_EW = Resources.Load<Sprite>("Wall/Wall_EW");
        Wall_NS = Resources.Load<Sprite>("Wall/Wall_NS");
        Wall_NESW = Resources.Load<Sprite>("Wall/Wall_NESW");
        Wall_ESW = Resources.Load<Sprite>("Wall/Wall_ESW");
        Wall_NE = Resources.Load<Sprite>("Wall/Wall_NE");
        Wall_NES = Resources.Load<Sprite>("Wall/Wall_NES");
        Wall_NEW = Resources.Load<Sprite>("Wall/Wall_NEW");
        Wall_NSW = Resources.Load<Sprite>("Wall/Wall_NSW");
        Wall_SW = Resources.Load<Sprite>("Wall/Wall_SW");
        Wall_NW = Resources.Load<Sprite>("Wall/Wall_NW");
        Wall_ES = Resources.Load<Sprite>("Wall/Wall_ES");

        Health_3 = Resources.Load<Sprite>("Hearts/Heart_3");
        Health_2 = Resources.Load<Sprite>("Hearts/Heart_2");
        Health_1 = Resources.Load<Sprite>("Hearts/Heart_1");
        Health_0 = Resources.Load<Sprite>("Hearts/Heart_0");

        Verify();
    }

    /// <summary>
    /// A logger method that verifies that all fields inside this class have a Sprite value.
    /// </summary>
    private static void Verify()
    {

        FieldInfo[] fields = typeof(GameSpritres).GetFields();


        foreach(FieldInfo field in fields)
        {
            Sprite sprite = field.GetValue(null) as Sprite;
            if(sprite == null)
            {
                Debug.LogError($"GridSprites.{field.Name} field is missing a value. (Sprite is missing)");
            }
        }

    }


}
