using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        if (go.layer != 3 || !go.TryGetComponent<Ball>(out var ball))
            return;

        ball.Despawn();
    }
}
