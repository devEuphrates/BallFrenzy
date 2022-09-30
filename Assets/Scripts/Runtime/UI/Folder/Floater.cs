using Euphrates;
using TMPro;
using UnityEngine;

public class Floater : MonoBehaviour
{
    RectTransform _transform;

    [SerializeField] CanvasGroup _group;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] AnimationCurveSO _curve;
    [SerializeField] float _moveUpAmount = 1f;
    [SerializeField] float _scaleFrom = 1f;
    [SerializeField] float _scaleTo = 2f;

    Vector2 _startPos;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Cancle();
    }

    private void OnDisable()
    {
        Cancle();
    }

    void AnimStep(float val)
    {
        float step = _curve.Value.Evaluate(val);
        _transform.localScale = Vector3.one * _scaleFrom + (_scaleTo - _scaleFrom) * val * Vector3.one;
        _transform.anchoredPosition = _startPos + _moveUpAmount * val * Vector2.up;
        _group.alpha = step;
    }

    void AnimFinish()
    {
        _working = false;
        _group.alpha = 0f;
        _transform.localScale = Vector3.one;
    }

    bool _working = false;
    TweenData _twd;
    public void Display(string text, Vector2 pos, float t)
    {
        if (_working)
        {
            _twd.Stop();
            AnimFinish();
        }

        _startPos = pos;
        _text.text = text;
        _twd = Tween.DoTween(0f, 1f, t, Ease.Lerp, AnimStep, AnimFinish);
    }

    void Cancle()
    {
        _transform.position = Vector2.zero;
        _startPos = Vector2.zero;
        _text.text = "";
        _twd?.Stop();
    }
}
