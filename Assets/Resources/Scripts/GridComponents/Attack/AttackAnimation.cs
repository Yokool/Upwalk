using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackAnimation : MonoBehaviour
{

    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.Play("");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }


}
