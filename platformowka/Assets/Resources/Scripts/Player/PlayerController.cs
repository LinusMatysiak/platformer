using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    bool isJumping;
    bool isCrouching;
    //
    Vector2 movementDirection;
    Rigidbody2D rb;
    Animator animator;
    //
    public static int points;
    public static int health = 3;
    public static int stars;

    void Start() {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //movement input
        movementDirection.x = Input.GetAxis("Horizontal");
        //animation
        animator.SetFloat("Speed", Mathf.Abs(movementDirection.x));
        //jumping
        if (isJumping == false && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //crouching
        if (Input.GetKey(KeyCode.C))
        {
            Crouch();
        } else {
            isCrouching = false;
        }
    }
    private void FixedUpdate() {
        rb.position += speed * movementDirection * Time.deltaTime;
    }
    void Jump()
    {
        Statistics.stats[1] += 1;
        isJumping = true;
        animator.SetBool("IsJumping", true);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }
    void Crouch()
    {
        isCrouching = true;
        animator.SetBool("IsCrouching", true);
    }
    void Die()
    {
        if (health <= 0)
        {
            health = 3;
            points = 0;
            stars = 0;
            Statistics.ResetStatistics();
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.gameObject.tag) {
            case "Ground":
                isJumping = false;
                animator.SetBool("IsJumping",false);
                rb.velocity = new Vector2(0,0);
                break;
            case "SilverCoin":
                Statistics.stats[3] += 1;
                points += 1;
                Destroy(collision.gameObject);
                break;
            case "GoldCoin":
                Statistics.stats[3] += 5;
                points += 5;
                Destroy(collision.gameObject);
                break;
            case "HealthUP":
                Statistics.stats[4] += 1;
                health += 1;
                Destroy(collision.gameObject);
                break;
            case "Star":
                Statistics.stats[2] += 1;
                stars += 1;
                Destroy(collision.gameObject);
                Debug.Log("stars: " + stars);
                break;
            case "WeakPoint":
                Statistics.stats[0] += 1;
                Jump();
                Destroy(collision.transform.parent?.parent?.gameObject);
                break;
            case "NextLevel":
                SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "Passage":
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(10f/255f, 100f/255f, 100f/255f, 100f/255f);
                break;
            case "Sign":
                collision.transform.GetChild(0).gameObject.SetActive(true);
                collision.GetComponent<Sign>().SignBehaviour();
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) {
            case "Ladder":
                animator.SetBool("OnLadder", true);
                rb.velocity = new Vector2(0, 0);
                break;
            case "Passage":
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
                break;
            case "Sign":
                collision.transform.GetChild(0).gameObject.SetActive(false);
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ladder":
                animator.SetBool("OnLadder", true);
                movementDirection.y = Input.GetAxis("Vertical");
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        switch (collision.gameObject.tag) {
            case "Ground":
                isJumping = false;
                animator.SetBool("IsJumping",false);
                rb.velocity = new Vector2(0,0);
                break;
            case "Obstacle":
                rb.velocity = new Vector2(Random.Range(-10,10),Random.Range(2,10));
                health -= 1;
                Die();
                break;
            case "Enemy":
                health -= 1;
                Die();
                break;
        }
    }
}