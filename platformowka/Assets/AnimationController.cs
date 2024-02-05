using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
    }
}
