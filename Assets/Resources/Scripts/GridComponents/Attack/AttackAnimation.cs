using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackAnimation : MonoBehaviour
{

    private Animator animator;

    private const string AttackAnimation_Easy = "AttackAnimation_Easy";
    private const string AttackAnimation_Medium = "AttackAnimation_Medium";
    private const string AttackAnimation_Hard = "AttackAnimation_Hard";

    private string GetAttackAnimationType()
    {
        Debug.Log(PersistentFiles.PlayerAttackData.AttackType);
        switch (PersistentFiles.PlayerAttackData.AttackType)
        {
            case Turn.EASY:
                return AttackAnimation_Easy;
            case Turn.MEDIUM:
                return AttackAnimation_Medium;
            case Turn.HARD:
                return AttackAnimation_Hard;
        }

        Debug.LogError("ASSERTION FAILED: This code should not be executed.");
        return AttackAnimation_Easy; // <- Default to this
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        Debug.Log(GetAttackAnimationType());
        animator.Play(GetAttackAnimationType());
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }


}
