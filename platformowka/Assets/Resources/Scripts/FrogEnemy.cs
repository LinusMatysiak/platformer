using System.Collections;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float jumpForce = 5f;
    public float jumpWaitTime = 1.5f;

    private Rigidbody2D rb;
    private int patrolDestination = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        Instantiate(Resources.Load("DeathAnimation") as GameObject, transform.position, Quaternion.identity);
    }
}