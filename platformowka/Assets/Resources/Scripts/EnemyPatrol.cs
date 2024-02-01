using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 3;
    
    private int patrolDestination = 0;
    private bool isFacingRight = true;

    // Gizmos
    private Vector3[] Points;
    private void Start()
    {
        if (patrolPoints.Length <= 1)
        {
            Debug.Log(transform.parent.name + " This Object Doent have atleast 2 patrolPoints");
        }
    }
    private void Update() {
        if (patrolPoints.Length >= 2) {
            MoveTowardsPatrolPoint();
            CheckDirection();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),collision.collider);
                    break;
        }
    }
    private void MoveTowardsPatrolPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolDestination].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < 0.2f)
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
        }
    }

    private void CheckDirection()
    {
        if (transform.position.x < patrolPoints[patrolDestination].position.x && isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x > patrolPoints[patrolDestination].position.x && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;

        Instantiate(Resources.Load("DeathAnimation") as GameObject, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        if (patrolPoints.Length >= 2)
        {
            DrawPatrolPathGizmos();
        }
    }

    private void DrawPatrolPathGizmos()
    {
        if (patrolPoints != null)
        {
            InitializePointsArray();

            for (int i = 0; i < patrolPoints.Length; i++)
            {
                Points[i] = new Vector3(patrolPoints[i].position.x, patrolPoints[i].position.y, patrolPoints[i].position.z);
                int nextIndex = (i + 1) % patrolPoints.Length;
                Gizmos.DrawLine(Points[i], Points[nextIndex]);
            }
        }
    }

    private void InitializePointsArray()
    {
        if (Points == null || Points.Length != patrolPoints.Length)
        {
            Points = new Vector3[patrolPoints.Length];
        }
    }
}