using Euphrates;
using System.Collections.Generic;
using UnityEngine;

public class TestDespawnBalls : MonoBehaviour
{
    List<GameObject> _released = new List<GameObject>();
    private void OnCollisionEnter(Collision other)
    {
        GameObject go = other.gameObject;

        if (go.layer != 3 || _released.Exists(p => p == go))
            return;

        _released.Add(go);

        go.transform.DoScale(Vector3.zero, .5f, Ease.OutExpo, null, () =>
        {
            _released.Remove(go);
            Pooler.Release("Ball", go);
        });

        go.transform.position = go.transform.position;
    }
}
