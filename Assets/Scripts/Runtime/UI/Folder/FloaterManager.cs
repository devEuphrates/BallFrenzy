using UnityEngine;

public class FloaterManager : MonoBehaviour
{
    [SerializeField] Floater[] _floaters = new Floater[10];
    [SerializeField] float _animDuration = 1f;
    int _index = 0;

    public void SpawnFloater(ulong amount)
    {
        Floater sel = _floaters[_index];
        sel.Display($"${amount.ConverCashToText()}", Vector2.zero, _animDuration);
        _index = _index == _floaters.Length - 1 ? 0 : _index + 1;
    }
}
