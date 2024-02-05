using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    // Referencja do komponentu Animator przypisanego do GameObjectu
    Animator animator;

    void Start()
    {
        // Pobranie komponentu Animator przypisanego do tego samego GameObjectu
        animator = GetComponent<Animator>();

        // Pobranie informacji na temat aktualnie odtwarzanego klipu animacji
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        // Wyci�gni�cie d�ugo�ci klipu animacji
        float animationDuration = clipInfo[0].clip.length;

        // Wywo�anie metody "SelfDestroy" po zadanym czasie trwania animacji
        Invoke("SelfDestroy", animationDuration);
    }
    void SelfDestroy()
    {
        // Zniszczenie GameObjectu, do kt�rego przyczepiony jest ten skrypt
        Destroy(gameObject);
    }
}
