using Euphrates;
using UnityEngine;

public class Ball : MonoBehaviour, IPoolable
{
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] TrailRenderer _trailRenderer;
    [SerializeField] MeshRenderer _mesh;
    [SerializeField] BallManagerSO _ballManager;

    bool _isCatched;
    public bool IsCatched => _isCatched;

    int _layerCache;

    private void Awake()
    {
        _layerCache = gameObject.layer;
    }

    private void OnDisable()
    {
        _ballManager.RemoveCatchedBall(this);
    }

    int _value = 1;
    public int Value
    {
        get { return _value; }
        set { _value = Mathf.RoundToInt(Mathf.Pow(2, value)); }
    }

    public void OnDestroyed()
    {
    }

    public Material GetMaterial() => _mesh.material;

    public void OnGet()
    {
        gameObject.layer = _layerCache;
        _isCatched = false;
        transform.localScale = Vector3.zero;
        transform.DoScale(Vector3.one * .5f, .5f);
    }

    public void OnReleased()
    {
        _rigidbody.velocity = Vector2.zero;
        _trailRenderer.Clear();
    }

    public void Catched()
    {
        gameObject.layer = 0;
        _ballManager.AddCatchecdBall(this);
        _isCatched = true;
    }

    public void SetMaterial(Material mat)
    {
        _mesh.material = mat;
    }

    public void Despawn()
    {
        transform.DoScale(Vector3.zero, .5f, Ease.Lerp, null, () => Pooler.Release("Ball", gameObject));
    }
}
