using UnityEngine;

public class GlowAnim : MonoBehaviour, IAnim
{
    Transform _transform;

    [SerializeField] MeshRenderer _renderer;
    [SerializeField] float _duration = .25f;
    [SerializeField] float _emissionPeak = 1f;
    [SerializeField] float _dropDownPeak = .5f;

    bool _playing = false;

    Material mat;

    private void Awake()
    {
        _transform = transform;
        mat = _renderer.materials[0];
    }

    Vector3 _cachedPosition;
    public void Play()
    {
        if (_playing)
            _transform.position = _cachedPosition;

        _playing = true;
        _timePassed = 0f;
        SetMaterialEmission(0f);
        _cachedPosition = _transform.position;
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
            SetMaterialEmission(0f);
            _transform.position = _cachedPosition;
            return;
        }

        float step = _timePassed / _duration;

        float ePeakStep = step * _emissionPeak;
        float ddPeakStep = step * _dropDownPeak;

        float emission = ePeakStep < _emissionPeak * .5f ? ePeakStep * 2f : _emissionPeak - (ePeakStep - _emissionPeak * .5f) * 2f;
        float dropDown = ddPeakStep < _dropDownPeak * .5f ? ddPeakStep * 2f : _dropDownPeak - (ddPeakStep - _dropDownPeak * .5f) * 2f;

        SetMaterialEmission(emission);
        SetPositionDropDown(dropDown);
    }

    void SetMaterialEmission(float emission) => mat.SetFloat("_Emission", Mathf.Clamp01(emission));
    void SetPositionDropDown(float dropDown) => _transform.position = _cachedPosition + Vector3.down * dropDown;
}
