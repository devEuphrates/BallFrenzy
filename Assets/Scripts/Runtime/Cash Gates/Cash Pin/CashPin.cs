using System.Collections.Generic;
using UnityEngine;

public class CashPin : MonoBehaviour
{
    [SerializeField] LayerMask _observedlayer;
    [SerializeField] ULongSO _cash;
    [SerializeField] FloaterManager _floaters;
    [SerializeField] ULongSO _cashPinValue;

    [SerializeReference] Transform _animObject;
    IAnim _pinAnim;

    List<GameObject> _inTrigger = new List<GameObject>();

    private void Awake() => _pinAnim = _animObject.GetComponent<IAnim>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        int pow = Mathf.RoundToInt(Mathf.Pow(2, layer));

        if ((_observedlayer & pow) != pow || _inTrigger.Exists(p => p == collision.gameObject))
            return;

        _inTrigger.Add(collision.gameObject);

        ulong added = _cashPinValue.Value;
        _cash.Value += added;
        _floaters.SpawnFloater(added);
        _pinAnim.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _inTrigger.Remove(collision.gameObject);
    }
}

