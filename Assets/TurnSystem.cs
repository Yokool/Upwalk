using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{

    private static TurnSystem instance;
    public static TurnSystem INSTANCE => instance;

    private void Awake()
    {
        instance = this;
    }

}
