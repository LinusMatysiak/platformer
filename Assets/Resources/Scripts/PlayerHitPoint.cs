using UnityEngine;

public class PlayerHitPoint : MonoBehaviour
{
    /* jak dzia³a ten skrypt?
     * skrypt usuwa obiekt nadrzêdny przeciwnika z którym koliduje je¿eli obiekt ma tag "WeakPoint" czyli s³aby punk przeciwnika.
     * 
     * Jak u¿yc ten skrypt? wystarczy podpi¹æ go pod obiekt HitPoint Gracza
     * 
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "WeakPoint":
                PlayerPrefs.SetInt("EnemiesKilled", PlayerPrefs.GetInt("EnemiesKilled") + 1);
                Destroy(collision.transform.parent?.parent?.gameObject);
                GetComponentInParent<PlayerController>().Jump();
                break;
        }
    }
}
