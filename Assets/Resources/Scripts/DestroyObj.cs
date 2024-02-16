using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    /* jak dzia�a ten skrypt?
     * skrypt usuwa obiekt je�eli animacja obiektu si� zako�czy�a
     * 
     * 
     * jak u�ywac ten skrypt?
     * wystarczy go podpi�� pod prefab obiektu kt�ry chcemy usun��
     */
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
