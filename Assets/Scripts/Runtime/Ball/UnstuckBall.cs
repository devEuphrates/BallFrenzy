using UnityEngine;

public class UnstuckBall : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] float _checkDelay = 1f;

    bool _wasStuck = false;
    float _timePassed = 0f;
    private void FixedUpdate()
    {
        if ((_timePassed += Time.fixedDeltaTime) < _checkDelay)
            return;

        _timePassed = 0f;

        if (_rigidbody.velocity.magnitude > .1f)
        {
            _wasStuck = false;
            return;
        }

        if (_wasStuck)
        {
            _wasStuck = false;
            _rigidbody.AddForce(new Vector2(Random.Range(-1f, 1f), 0f));
            return;
        }

        _wasStuck = true;
    }
}
