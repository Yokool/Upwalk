using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameAnimation
{

    public static GameObject attackAnimationPrefab;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void LoadAnimations()
    {
        attackAnimationPrefab = Resources.Load<GameObject>("Prefabs/Animations/AttackAnimation/AttackAnimation");
    }

}
