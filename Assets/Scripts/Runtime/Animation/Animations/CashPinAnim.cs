using UnityEngine;

public class CashPinAnim : MonoBehaviour, IAnim
{
    Transform _transform;

    [SerializeField] float _duration = .25f;
    [SerializeField] float _rotateAmount = -30f;
    [SerializeField] Axis _rotateAxis;

    Quaternion _cachedRotation;
    Quaternion _targetRotation;

    bool _playing = false;

    private void Awake()
    {
        _transform = transform;
    }

    public void Play()
    {
        if (_playing)
        {
            float halfDur = (_duration * .5f);

            if (_timePassed < halfDur)
                return;

            _timePassed =  halfDur - (_timePassed - halfDur);
            return;
        }

        Vector3 axis = _rotateAxis switch
        {
            Axis.X => Vector3.right,
            Axis.Y => Vector3.up,
            Axis.Z => Vector3.forward,
            _ => Vector3.zero
        };

        _targetRotation = _cachedRotation * Quaternion.Euler(axis * _rotateAmount);

        _cachedRotation = _transform.rotation;
        _playing = true;
        _timePassed = 0f;
        SetRotationByStep(0f);
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
            SetRotationByStep(0f);
            _transform.rotation = _cachedRotation;
            return;
        }

        float step = _timePassed / _duration;

        float rotateStep = step < .5f ? step * 2f : 1f - (step - .5f) * 2f;
        SetRotationByStep(rotateStep);
    }

    void SetRotationByStep(float step) => _transform.rotation = Quaternion.Lerp(_cachedRotation, _targetRotation, step);
}

public enum Axis { X, Y, Z }