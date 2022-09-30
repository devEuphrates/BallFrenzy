using Euphrates;
using System;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    float _multiplierValue;
    public float MultiplierValue => _multiplierValue;

    public Action OnMultiplierChange;

    [SerializeField] ULongSO _cash;
    [Space]
    [SerializeField] FloatSO _baseMultiplier;
    [SerializeField] FloatSO _globalAddition;
    [SerializeField] float _addition;
    [Space]
    [SerializeField] MultiplierText _text;
    [SerializeField] FloaterManager _floaters;
    IAnim _ballCatachedAnim;

    private void Awake()
    {
        _ballCatachedAnim = GetComponent<IAnim>();
    }

    private void OnEnable()
    {
        _baseMultiplier.OnChange += SetMultiplier;
    }

    private void OnDisable()
    {
        _baseMultiplier.OnChange -= SetMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;

        if (go.layer != 3 || !go.TryGetComponent<Ball>(out var ball) || ball.IsCatched)
            return;

        float val = ball.Value;
        ulong added = (ulong)(val * (int)(_multiplierValue * 10f));

        _cash.Value += added;
        _floaters.SpawnFloater(added);

        ball.Catched();
        _ballCatachedAnim.Play();
    }

    private void Start()
    {
        SetMultiplier(0);
    }

    void SetMultiplier(float _)
    {
        _multiplierValue = _baseMultiplier.Value + _addition + _globalAddition;
        OnMultiplierChange?.Invoke();

        _text.UpdateText(_multiplierValue);
    }
}
