using TMPro;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] protected int _slotIndex = 0;
    public int SlotIndex => _slotIndex;
    [SerializeField] protected BallManagerSO _ballManager;
    [SerializeField] protected CombineableBall _ballVisual;
    [SerializeField] protected TextMeshProUGUI _cashText;
    [SerializeField] protected GameObject _purchaseBall;

    private void Start()
    {
        SetSlot();
    }

    private void OnEnable()
    {
        _ballManager.OnSlotsChange += SetSlot;
    }

    private void OnDisable()
    {
        _ballManager.OnSlotsChange -= SetSlot;
    }

    public void SetSlot()
    {
        _slotIndex = Mathf.Clamp(_slotIndex, 0, _ballManager.BallSlots.Length - 1);
        int ballId = _ballManager.BallSlots[_slotIndex];

        if (ballId == -1)
        {
            SetEmptySlot();
            return;
        }

        SetPurchaseArea(false);
        _ballVisual.gameObject.SetActive(true);
        _cashText.gameObject.SetActive(true);

        string cashStr = $"{Mathf.Pow(2, ballId)}";
        _cashText.text = cashStr;

        Material mat = _ballManager.GetSkin(ballId);
        _ballVisual.ChangeMaterial(mat);
    }

    public void VisualState(bool active) => _ballVisual.gameObject.SetActive(active);

    protected virtual void SetEmptySlot()
    {
        SetPurchaseArea(true);
        _cashText.gameObject.SetActive(false);
        _ballVisual.gameObject.SetActive(false);
    }

    protected virtual void SetPurchaseArea(bool active) => _purchaseBall.SetActive(active);
}