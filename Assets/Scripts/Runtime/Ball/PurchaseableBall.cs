using Euphrates;
using TMPro;
using UnityEngine;

public class PurchaseableBall : MonoBehaviour, IInteractable
{
    [SerializeField] BallManagerSO _ballManager;
    [SerializeField] int _slotIndex;
    [SerializeField] ULongSO _newBallCost;
    [SerializeField] ULongSO _costIncrease;
    [SerializeField] IntSO _purchaseCount;
    [SerializeField] ULongSO _cash;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] GameObject _deactivePanel;

    private void OnEnable()
    {
        _cash.OnChange += OnCashChange;
        _purchaseCount.OnChange += SetCost;

        SetActivated();
        SetCost(0);
    }

    private void OnDisable()
    {
        _cash.OnChange -= OnCashChange;
        _purchaseCount.OnChange -= SetCost;
    }

    private void Start()
    {
        SetActivated();
        SetCost(0);
    }

    public void Interact()
    {
        PurchaseBall();
    }

    ulong RealCost()
    {
        ulong cost = _newBallCost.Value;

        for (int i = 1; i < _purchaseCount.Value + 1; i++)
            cost += (ulong)i * _costIncrease.Value;

        return cost;
    }

    public void PurchaseBall()
    {
        if (_ballManager.BallSlots[_slotIndex] != -1)
            return;

        ulong realCost = RealCost();

        if (_cash.Value < realCost)
            return;

        _cash.Value -= realCost;
        _purchaseCount.Value++;

        _ballManager.ChangeSlotValue(_slotIndex, 0);
    }

    void OnCashChange(ulong _) => SetActivated();

    void SetActivated() => _deactivePanel.SetActive(_cash.Value < RealCost());

    void SetCost(int _) => _text.text = RealCost().ConverCashToText();
}
