using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] BallManagerSO _ballManager;

    private void OnEnable()
    {
        _ballManager.OnSpawn += SpawnSloted;
    }

    private void OnDisable()
    {
        _ballManager.OnSpawn -= SpawnSloted;
    }

    void SpawnSloted()
    {
        for (int i = 5; i < _ballManager.BallSlots.Length; i++)
        {
            int sel = _ballManager.BallSlots[i];

            if (sel == -1)
                continue;

            Vector3 pos = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0f);
            Ball ball = Pooler.Spawn("Ball", transform, transform.position + pos, Quaternion.identity).GetComponent<Ball>();

            ball.Value = sel;
            ball.SetMaterial(_ballManager.GetSkin(sel));
        }
    }
}
