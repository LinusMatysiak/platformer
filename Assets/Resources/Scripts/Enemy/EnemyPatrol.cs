using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    /* Jak dzia�a ten skrypt?
     * 
     * Skrypt patrzy czy przeciwnik dotar� do wyznaczonego punktu,
     * je�eli warunek zostanie spe�niony to przechodzi do nast�pnego
     * je�eli nadamy mu WaitTimer to b�dzie czeka� na kazdym przystanku
     * 
     * 
     * 
     * Jak u�ywa� ten skrypt?
     * skrypt nale�y podpi�� pod prefab przeciwnika i
     * przypi�� mu punkty mi�dzy kt�rymi b�dzie si� porusza�
     * 
     * 
     */
    public Transform[] patrolPoints;
    public float moveSpeed = 3;
    public float waitTimer;
    private float waitTime;
    private int patrolDestination = 0;
    private bool isFacingRight = true;
    private bool isWaiting;
    private Rigidbody2D rb;
    private Vector2[] Points;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (patrolPoints.Length <= 1)
        {
            Debug.Log(transform.parent.name + " This Object Doesnt have atleast 2 patrolPoints");
        }
    }
    private void Update() {
        if (patrolPoints.Length >= 2 && isWaiting == false)
        {
            MoveTowardsPatrolPoint();
            CheckDirection();
        }
        else if (isWaiting == true)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= waitTimer)
            {
                waitTime = 0;
                isWaiting = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
                break;
        }
    }
    private void MoveTowardsPatrolPoint()
    {
        rb.position = Vector2.MoveTowards(rb.position, patrolPoints[patrolDestination].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(rb.position, patrolPoints[patrolDestination].position) < 0.2f)
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
            isWaiting = true;
        }
    }

    private void CheckDirection()
    {
        if (rb.position.x < patrolPoints[patrolDestination].position.x && isFacingRight ||
            rb.position.x > patrolPoints[patrolDestination].position.x && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;

        Instantiate(Resources.Load("Prefabs/DeathAnimation") as GameObject, transform.position, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        if (patrolPoints.Length >= 2)
        {

            if (Points == null || Points.Length != patrolPoints.Length)
            {
                Points = new Vector2[patrolPoints.Length];
            }

            for (int i = 0; i < patrolPoints.Length; i++)
            {
                Points[i] = new Vector2(patrolPoints[i].position.x, patrolPoints[i].position.y);
                int nextIndex = (i + 1) % patrolPoints.Length;
                Gizmos.DrawLine(Points[i], Points[nextIndex]);
            }
        }
    }
}