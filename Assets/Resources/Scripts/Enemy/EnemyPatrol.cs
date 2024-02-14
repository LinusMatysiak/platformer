using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Tabela zawieraj¹ca punkty miêdzy którymi porusza siê wróg
    public Transform[] patrolPoints;

    // Predkoœæ poruszania siê
    public float moveSpeed = 3;

    // Ustanawia czas oczekiwania na ka¿dym punkcie patrolowym
    public float waitTimer;

    // Zmienna œledz¹ca aktualny czas na przystanku
    private float waitTime;

    // Zmienna œledz¹ca nastêpny punkt do którego siê uda
    private int patrolDestination = 0;

    // Zmienna od której zale¿y w któr¹ stronê jest skierowany sprite obiektu
    private bool isFacingRight = true;

    // Gdy True powoduje, ¿e obiekt siê nie rusza do momentu gdy waitTime == waitTimer
    private bool isWaiting;

    // odwo³anie do obiektu obs³uguj¹cego fizykê
    private Rigidbody2D rb;

    // zmienna u¿ywana przy rysowaniu linii miêdzy punktami
    private Vector2[] Points;
    private void Start() {
        // sprawdza czy obiekt ma mniej ni¿ 2 punkty po których mo¿e siê poruszaæ
        // je¿eli ma mniej to wypisze ostrze¿enie i wska¿e który to obiekt
        rb = GetComponent<Rigidbody2D>();
        if (patrolPoints.Length <= 1)
        {
            Debug.Log(transform.parent.name + " This Object Doesnt have atleast 2 patrolPoints");
        }
    }
    private void Update() {
        // Sprawdzenie, czy istnieje co najmniej 2 punkty patrolowe i czy wróg nie czeka
        if (patrolPoints.Length >= 2 && isWaiting == false)
        {
            MoveTowardsPatrolPoint(); // odpowiada za logikê poruszania siê
            CheckDirection(); // odpowiada za obrócenie sprite'a obiektu
        }
        else if (isWaiting == true)
        {
            // dodaje czas w waitTIme
            waitTime += Time.deltaTime;
            // je¿eli obiekt przeczeka wskazany czas, odblokuje siê mo¿liwoœæ ponownego poruszania
            if (waitTime >= waitTimer)
            {
                waitTime = 0;
                isWaiting = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) // sprawdza po tagu z czym obiekt wchodzi w kolizjê
        {
            case "Enemy":
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider); // ignoruje kolizjê z innym obiektem o tagu "enemy"
                break;
        }
    }
    private void MoveTowardsPatrolPoint()
    {
        // poruszanie siê obiektu w pozycjê patrolPoints[patrolDestination]
        rb.position = Vector2.MoveTowards(rb.position, patrolPoints[patrolDestination].position, moveSpeed * Time.deltaTime);

        // je¿eli obiekt zbli¿y siê do punktu patrolowego
        // zmienia jego kierunek na nastêpny patrolDestination
        // i daje instrukcjê by przy nastêpnej próbie poruszania siê poczeka³
        if (Vector2.Distance(rb.position, patrolPoints[patrolDestination].position) < 0.2f)
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
            isWaiting = true;
        }
    }

    private void CheckDirection()
    {
        // sprawdza czy pozycja obiektu zgadza siê z pozycj¹ docelow¹ i w któr¹ stronê jest aktualnie skierowany obiekt
        if (rb.position.x < patrolPoints[patrolDestination].position.x && isFacingRight ||
            rb.position.x > patrolPoints[patrolDestination].position.x && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // odwraca warunek pomocniczy
        isFacingRight = !isFacingRight;
        // pobiera aktualn¹ wartoœæ osi x
        Vector2 scale = transform.localScale;
        // zmienia wartoœæ osi x na ujemn¹
        scale.x *= -1;
        // przypisuje odwrócon¹ ju¿ wartoœæ scale.x
        // co daje wra¿enie, ¿e obiekt patrzy siê w drug¹ strone ni¿ patrzy³ siê wczeœniej
        transform.localScale = scale;
    }

    // funkcja wykonuje siê gdy obiekt dostanie instrukcjê usuniêcia siê.
    // instrukcja ta jest wywo³ywana przez gracza w skrypcie PlayerController.
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;

        // Tworzy nowego prefaba w miejscu Obiektu.
        // Lokalizacja prefaba znajduje siê w Prefabs/DeathAnimation
        Instantiate(Resources.Load("Prefabs/DeathAnimation") as GameObject, transform.position, Quaternion.identity);
    }
    // Funkcja ta jest wywo³ywana za ka¿dym razem gdy linie nie s¹ narysowane
    // lub gdy pozycja obiektów miêdzy którymi s¹ narysowane liniê siê zmieni
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
                // wczytujê do Points wspó³rzêdne pierwszego punktu i zachowuje je w Points[]
                Points[i] = new Vector2(patrolPoints[i].position.x, patrolPoints[i].position.y);
                // wy³apuje wspó³rzêdne drugiego punktu (points[]+1)
                int nextIndex = (i + 1) % patrolPoints.Length;
                // Rysuje liniê miêdzy obecnym punktem patrolowym a nastêpnym
                Gizmos.DrawLine(Points[i], Points[nextIndex]);
            }
        }
    }
}