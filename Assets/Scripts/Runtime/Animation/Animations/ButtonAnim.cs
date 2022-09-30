using UnityEngine;

public class ButtonAnim : MonoBehaviour, IAnim
{
    Transform _transform;

    [SerializeField] float _duration = .25f;
    [SerializeField] float _pushAmount = .5f;

    Vector3 _cachedPosition;

    bool _playing = false;

    private void Awake()
    {
        _transform = transform;
    }

    public void Play()
    {
        if (_playing)
            _transform.position = _cachedPosition;

        _cachedPosition = _transform.position;
        _playing = true;
        _timePassed = 0f;
        SetPushAmount(0f);
    }

    float _timePassed = 0f;
    private void Update()
    {
        if (!_playing)
            return;

        _timePassed += Time.deltaTime;

        if (_timePassed > _duration)
        {
            _playing = false;
            _timePassed = 0f;
            SetPushAmount(0f);
            _transform.position = _cachedPosition;
            return;
        }

        float step = _timePassed / _duration;

        float pushStep = step * _pushAmount;

        float push = pushStep < _pushAmount * .5f ? pushStep * 2f : _pushAmount - (pushStep - _pushAmount* .5f) * 2f;

        SetPushAmount(push);
    }

    void SetPushAmount(float amount) => _transform.position = _cachedPosition + Vector3.forward * amount;
}
