using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        float animationDuration = clipInfo[0].clip.length;

        Invoke("SelfDestroy", animationDuration);
    }
    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
