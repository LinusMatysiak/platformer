using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    /* Jak dzia³a ten skrypt?
     * Skrypt pozwala graczowi sie poruszaæ, interaktowac z przedmiotami
     * obraca gracza gdy ten zacznie chodziæ w innym kierunku
     * 
     * 
     * Jak u¿ywaæ ten skrypt?
     * Skrypt nale¿y podpi¹æ pod gracza, i ustawiæ mu wszystkie predkoœci oraz SetHealth.
     */
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float knockback;
    bool isJumping;
    bool knockbackeffect;
    //
    Vector2 movementDirection;
    Rigidbody2D rb;
    Animator animator;
    bool isFacingRight = true;
    //
    public float health;
    public static float currentHealth;
    public static float maxHealth;

    void Start()
    {
        AssignHealth();
        if (PlayerPrefs.GetInt("Initialized") != 1)
        {
            SetValues();
        }
        //Checkifdead();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void AssignHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = health;
            maxHealth = health;
        }
    }
    void Update()
    {
        Debug.Log(currentHealth);
        if (knockbackeffect == false) {
            movementDirection.x = Input.GetAxisRaw("Horizontal");
        }
        if (movementDirection.x < 0 && isFacingRight || movementDirection.x > 0 && !isFacingRight) {
            Flip();
        }
        animator.SetFloat("Speed", Mathf.Abs(movementDirection.x));
        if (isJumping == false && Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void FixedUpdate()
    {
        rb.position += speed * movementDirection * Time.deltaTime;
    }
    public void Jump()
    {
        PlayerPrefs.SetInt("TimesJumped", PlayerPrefs.GetInt("TimesJumped") + 1);
        isJumping = true;
        animator.SetBool("IsJumping", true);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }
    void Checkifdead()
    {
        if (currentHealth <= 0)
        {
            AssignHealth();
            SceneManager.LoadScene(sceneBuildIndex: 0);
            SetValues();
        }
    }
    void Knockback()
    {
        rb.velocity = new Vector2(0, 0);
        knockbackeffect = true;
        movementDirection.x = 0;
        if (isFacingRight) {
            rb.velocity = new Vector2(knockback * -1, knockback / 2f);
        } else {
            rb.velocity = new Vector2(knockback, knockback / 2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                isJumping = false;
                animator.SetBool("IsJumping", false);
                rb.velocity = new Vector2(0, 0);
            break;
            case "SilverCoin":
                PlayerPrefs.SetInt("CoinsCollected", PlayerPrefs.GetInt("CoinsCollected") + 1);
                Destroy(collision.gameObject);
            break;
            case "GoldCoin":
                PlayerPrefs.SetInt("CoinsCollected", PlayerPrefs.GetInt("CoinsCollected") + 3);
                Destroy(collision.gameObject);
            break;
            case "HealthUP":
                PlayerPrefs.SetInt("TimesHealed", PlayerPrefs.GetInt("TimesHealed") + 1);
                currentHealth += 1;
                Destroy(collision.gameObject);
            break;
            case "Star":
                PlayerPrefs.SetInt("StarsCollected", PlayerPrefs.GetInt("StarsCollected") + 1);
                Destroy(collision.gameObject);
            break;
            case "NextLevel":
                SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
            break;
            case "Passage":
                                                                                   // Kolor Alfa  R                  G            B
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(10f / 255f, 100f / 255f, 100f / 255f, 100f / 255f);
            break;
            case "Sign":
                collision.transform.GetChild(0).gameObject.SetActive(true);
            break;
            case "Ladder":
                if (animator.GetBool("IsJumping") == true)
                {
                    animator.SetBool("IsJumping", false);
                }
            break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ladder":
                isJumping = false;
                animator.SetBool("OnLadder", false);
                rb.gravityScale = 1;
                movementDirection.y = 0;
            break;
            case "Passage":
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
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
                isJumping = true;
                animator.SetBool("OnLadder", true);
                movementDirection.y = Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(0, 0);
                rb.gravityScale = 0;
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                knockbackeffect = false;
                isJumping = false;
                animator.SetBool("IsJumping", false);
            break;
            case "Enemy":
                currentHealth -= 1;
                Knockback();
                Checkifdead();
            break;
        }
    }
    public static void SetValues()
    {
        PlayerPrefs.SetInt("Initialized", 1);
        PlayerPrefs.SetInt("GameCompleted", 0);
        PlayerPrefs.SetInt("EnemiesKilled", 0);
        PlayerPrefs.SetInt("TimesJumped", 0);
        PlayerPrefs.SetInt("StarsCollected", 0);
        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.SetInt("TimesHealed", 0);
    }
    private void OnApplicationQuit()
    {
        SetValues();
    }
}