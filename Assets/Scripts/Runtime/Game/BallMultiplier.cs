using Euphrates;
using System;
using UnityEngine;

public class BallMultiplier : MonoBehaviour
{
    int _multiplierValue;
    public int MultiplierValue => _multiplierValue;

    public Action OnMultiplierChange;

    [SerializeField] ULongSO _cash;
    [Space]
    [SerializeField] IntSO _baseMultiplier;
    [SerializeField] int _addition;
    [Space]
    [SerializeField] MultiplierText _text;
    [SerializeField] FloaterManager _floaters;
    [SerializeField] BallManagerSO _ballManager;
    IAnim _ballCatachedAnim;

    private void Awake()
    {
        _ballCatachedAnim = GetComponent<IAnim>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;

        if (go.layer != 3 || !go.TryGetComponent<Ball>(out var ball) || ball.IsCatched)
            return;

        float val = ball.Value;
        ulong added = (ulong)(val * _multiplierValue);

        _cash.Value += added;
        _floaters.SpawnFloater(added);

        ball.Catched();
        _ballCatachedAnim.Play();

        for (int i = 0; i < _multiplierValue - 1; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-.1f, .1f), UnityEngine.Random.Range(-.1f, .1f), 0f);
            Ball spawned = Pooler.Spawn("Ball", null, transform.position + pos, Quaternion.identity).GetComponent<Ball>();
            spawned.SetMaterial(ball.GetMaterial());
            spawned.Catched();
        }
    }

    private void Start()
    {
        SetMultiplier(0);
    }

    void SetMultiplier(float _)
    {
        _multiplierValue = _baseMultiplier.Value + _addition;
        OnMultiplierChange?.Invoke();

        _text.UpdateText(_multiplierValue);
    }
}
