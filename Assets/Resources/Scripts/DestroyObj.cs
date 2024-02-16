using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    /* jak dzia³a ten skrypt?
     * skrypt usuwa obiekt je¿eli animacja obiektu siê zakoñczy³a
     * 
     * 
     * jak u¿ywac ten skrypt?
     * wystarczy go podpi¹æ pod prefab obiektu który chcemy usun¹æ
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
