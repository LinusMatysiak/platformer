using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "WeakPoint":
                Statistics.stats[0] += 1;
                Destroy(collision.transform.parent?.parent?.gameObject);
                GetComponentInParent<PlayerController>().Jump();
                break;
        }
    }
}
