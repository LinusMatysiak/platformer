using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Tabela zawieraj�ca punkty mi�dzy kt�rymi porusza si� wr�g
    public Transform[] patrolPoints;

    // Predko�� poruszania si�
    public float moveSpeed = 3;

    // Ustanawia czas oczekiwania na ka�dym punkcie patrolowym
    public float waitTimer;

    // Zmienna �ledz�ca aktualny czas na przystanku
    private float waitTime;

    // Zmienna �ledz�ca nast�pny punkt do kt�rego si� uda
    private int patrolDestination = 0;

    // Zmienna od kt�rej zale�y w kt�r� stron� jest skierowany sprite obiektu
    private bool isFacingRight = true;

    // Gdy True powoduje, �e obiekt si� nie rusza do momentu gdy waitTime == waitTimer
    private bool isWaiting;

    // odwo�anie do obiektu obs�uguj�cego fizyk�
    private Rigidbody2D rb;

    // zmienna u�ywana przy rysowaniu linii mi�dzy punktami
    private Vector2[] Points;
    private void Start() {
        // sprawdza czy obiekt ma mniej ni� 2 punkty po kt�rych mo�e si� porusza�
        // je�eli ma mniej to wypisze ostrze�enie i wska�e kt�ry to obiekt
        rb = GetComponent<Rigidbody2D>();
        if (patrolPoints.Length <= 1)
        {
            Debug.Log(transform.parent.name + " This Object Doesnt have atleast 2 patrolPoints");
        }
    }
    private void Update() {
        // Sprawdzenie, czy istnieje co najmniej 2 punkty patrolowe i czy wr�g nie czeka
        if (patrolPoints.Length >= 2 && isWaiting == false)
        {
            MoveTowardsPatrolPoint(); // odpowiada za logik� poruszania si�
            CheckDirection(); // odpowiada za obr�cenie sprite'a obiektu
        }
        else if (isWaiting == true)
        {
            // dodaje czas w waitTIme
            waitTime += Time.deltaTime;
            // je�eli obiekt przeczeka wskazany czas, odblokuje si� mo�liwo�� ponownego poruszania
            if (waitTime >= waitTimer)
            {
                waitTime = 0;
                isWaiting = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) // sprawdza po tagu z czym obiekt wchodzi w kolizj�
        {
            case "Enemy":
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider); // ignoruje kolizj� z innym obiektem o tagu "enemy"
                break;
        }
    }
    private void MoveTowardsPatrolPoint()
    {
        // poruszanie si� obiektu w pozycj� patrolPoints[patrolDestination]
        rb.position = Vector2.MoveTowards(rb.position, patrolPoints[patrolDestination].position, moveSpeed * Time.deltaTime);

        // je�eli obiekt zbli�y si� do punktu patrolowego
        // zmienia jego kierunek na nast�pny patrolDestination
        // i daje instrukcj� by przy nast�pnej pr�bie poruszania si� poczeka�
        if (Vector2.Distance(rb.position, patrolPoints[patrolDestination].position) < 0.2f)
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
            isWaiting = true;
        }
    }

    private void CheckDirection()
    {
        // sprawdza czy pozycja obiektu zgadza si� z pozycj� docelow� i w kt�r� stron� jest aktualnie skierowany obiekt
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
        // pobiera aktualn� warto�� osi x
        Vector2 scale = transform.localScale;
        // zmienia warto�� osi x na ujemn�
        scale.x *= -1;
        // przypisuje odwr�con� ju� warto�� scale.x
        // co daje wra�enie, �e obiekt patrzy si� w drug� strone ni� patrzy� si� wcze�niej
        transform.localScale = scale;
    }

    // funkcja wykonuje si� gdy obiekt dostanie instrukcj� usuni�cia si�.
    // instrukcja ta jest wywo�ywana przez gracza w skrypcie PlayerController.
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;

        // Tworzy nowego prefaba w miejscu Obiektu.
        // Lokalizacja prefaba znajduje si� w Prefabs/DeathAnimation
        Instantiate(Resources.Load("Prefabs/DeathAnimation") as GameObject, transform.position, Quaternion.identity);
    }
    // Funkcja ta jest wywo�ywana za ka�dym razem gdy linie nie s� narysowane
    // lub gdy pozycja obiekt�w mi�dzy kt�rymi s� narysowane lini� si� zmieni
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
                // wczytuj� do Points wsp�rz�dne pierwszego punktu i zachowuje je w Points[]
                Points[i] = new Vector2(patrolPoints[i].position.x, patrolPoints[i].position.y);
                // wy�apuje wsp�rz�dne drugiego punktu (points[]+1)
                int nextIndex = (i + 1) % patrolPoints.Length;
                // Rysuje lini� mi�dzy obecnym punktem patrolowym a nast�pnym
                Gizmos.DrawLine(Points[i], Points[nextIndex]);
            }
        }
    }
}