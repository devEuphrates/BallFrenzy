using UnityEngine;

public class TestBallSpawner : MonoBehaviour
{
    [SerializeField] InputReaderSO _inputs;

    private void OnEnable()
    {
        _inputs.OnTouchDown += OnTouch;
    }

    private void OnDisable()
    {
        _inputs.OnTouchDown -= OnTouch;
    }

    void OnTouch(Vector2 point)
    {
        for (int i = 0; i < 5; i++)
            Pooler.Spawn("Ball", transform, transform.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0f), Quaternion.identity);
    }
}
