using UnityEngine;

public class PlayerHitPoint : MonoBehaviour
{
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
