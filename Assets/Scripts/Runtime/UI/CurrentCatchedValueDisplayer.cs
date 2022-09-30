using TMPro;
using UnityEngine;

public class CurrentCatchedValueDisplayer : MonoBehaviour
{
    [SerializeField] BallManagerSO _ballManager;
    [SerializeField] TextMeshPro _text;

    private void OnEnable()
    {
        _ballManager.OnCatched += UpdateValue;
    }

    private void OnDisable()
    {
        _ballManager.OnCatched -= UpdateValue;
    }

    void UpdateValue()
    {
        var balls = _ballManager.GetCatchedBalls;

        int val = 0;
        foreach (var ball in balls)
            val += ball.Value;

        UpdateText(val);
    }

    void UpdateText(int val) => _text.text = $"${val.ConverCashToText()}";
}
