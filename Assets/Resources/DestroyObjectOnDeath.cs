using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnDeath : MonoBehaviour, IOnDeath
{

    public void OnDeath()
    {
        Destroy(gameObject);
    }

}
