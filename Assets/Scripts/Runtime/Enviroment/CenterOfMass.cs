using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CenterOfMass : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField] Transform _point;

    private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    private void Start() => _rigidbody.centerOfMass = transform.InverseTransformPoint(_point.position);
}
