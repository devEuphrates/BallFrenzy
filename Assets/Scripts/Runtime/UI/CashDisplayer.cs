using Euphrates;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashDisplayer : MonoBehaviour
{
    [SerializeField] ULongSO _cash;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float _buildUpDuration = .2f;

    private void OnEnable()
    {
        _cash.OnChange += UpdateText;
    }

    private void OnDisable()
    {
        _cash.OnChange -= UpdateText;
    }

    private void Start()
    {
        SetText(_cash.Value);
    }

    bool _animating = false;

    ulong _from;
    ulong _target;

    float _timePassed = 0f;
    private void Update()
    {
        if (!_animating)
            return;

        _timePassed += Time.deltaTime;

        if (_timePassed > _buildUpDuration)
        {
            _animating = false;
            _timePassed = 0f;
            SetText(_cash.Value);
            return;
        }

        float step = _timePassed / _buildUpDuration;
        ulong val = Lerp(_from, _target, step);
        SetText(val);
    }

    void SetText(ulong val)
    {
        _text.text = val.ConverCashToText();
    }

    ulong Lerp(ulong x, ulong y, float t)
    {
        t = Mathf.Clamp01(t);
        ulong diff = y - x;
        ulong passed = (ulong)(diff * t);
        return x + passed;
    }

    void UpdateText(ulong change)
    {
        _animating = true;
        _from = _cash.Value - change;
        _target = _cash.Value;
        _timePassed = 0f;
    }
}
