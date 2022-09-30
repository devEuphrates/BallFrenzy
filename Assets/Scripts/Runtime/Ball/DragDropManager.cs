using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropManager : MonoBehaviour
{
    [SerializeField] BallManagerSO _ballManager;
    [SerializeField] InputReaderSO _inputReader;
    [SerializeField] MeshRenderer _draggedVisual;
    [SerializeField] Camera _camera;
    [SerializeField] LayerMask _combineableLayer;
    [SerializeField] LayerMask _backgroundLayer;

    private void OnEnable()
    {
        _inputReader.OnTouchDown += TouchDown;
        _inputReader.OnTouchMove += TouchMoved;
        _inputReader.OnTouchUp += TouchRelease;
    }

    private void OnDisable()
    {
        _inputReader.OnTouchDown -= TouchDown;
        _inputReader.OnTouchMove -= TouchMoved;
        _inputReader.OnTouchUp -= TouchRelease;
    }

    Vector2 _lastPos = Vector2.zero;
    int _dragged = -1;
    void TouchDown(Vector2 pos)
    {
        if (!CastRay(pos, out var hit, 1000f, _combineableLayer) || !hit.transform.TryGetComponent<Slot>(out var slot) || _ballManager.BallSlots[slot.SlotIndex] == -1)
            return;

        _lastPos = pos;

        _dragged = slot.SlotIndex;
        Material mat = _ballManager.GetSkin(_ballManager.BallSlots[_dragged]);
        _draggedVisual.gameObject.SetActive(true);
        _draggedVisual.material = mat;
        slot.VisualState(false);
    }

    void TouchMoved(Vector2 pos)
    {
        if (_dragged == -1)
            return;

        _lastPos = pos;
    }

    void TouchRelease(Vector2 pos)
    {
        if (_dragged == -1)
            return;

        if (!CastRay(pos, out var hit, 1000f, _combineableLayer)
            || !hit.transform.TryGetComponent<Slot>(out var slot)
            || _dragged == slot.SlotIndex)
        {
            ReleaseBall();
            return;
        }

        if (_ballManager.BallSlots[slot.SlotIndex] == -1)
        {
            _ballManager.ChangeSlotValue(slot.SlotIndex, _ballManager.BallSlots[_dragged]);
            _ballManager.ChangeSlotValue(_dragged, -1);
            ReleaseBall();
            return;
        }

        void SwitchBalls()
        {
            int tmp = _ballManager.BallSlots[_dragged];
            _ballManager.ChangeSlotValue(_dragged, _ballManager.BallSlots[slot.SlotIndex]);
            _ballManager.ChangeSlotValue(slot.SlotIndex, tmp);
            ReleaseBall();
        }

        if (!CanCombine(_dragged, slot.SlotIndex))
        {

            SwitchBalls();
            return;
        }

        CombineBalls(_dragged, slot.SlotIndex);
    }

    private void LateUpdate()
    {
        if (_dragged == -1)
            return;

        if (!CastRay(_lastPos, out var hit, 1000f, _backgroundLayer))
            return;

        Vector3 pos = hit.point;
        pos += Vector3.back * 2f + Vector3.up * 2f;
        _draggedVisual.transform.position = pos;
    }

    void ReleaseBall()
    {
        _dragged = -1;
        _draggedVisual.gameObject.SetActive(false);
        _ballManager.InvokeMeshRefresh();
    }

    bool CanCombine(int slot1, int slot2)
    {
        int val1 = _ballManager.BallSlots[slot1];
        int val2 = _ballManager.BallSlots[slot2];

        return val1 == val2;
    }

    void CombineBalls(int slot1, int slot2)
    {
        _dragged = -1;
        _draggedVisual.gameObject.SetActive(false);
        int val = _ballManager.BallSlots[slot2];

        _ballManager.ChangeSlotValue(slot1, -1);
        _ballManager.ChangeSlotValue(slot2, val + 1);
    }

    RaycastHit[] results = new RaycastHit[10];
    bool CastRay(Vector2 screenPos, out RaycastHit hit, float distance, LayerMask mask)
    {
        Ray ray = _camera.ScreenPointToRay(screenPos);
        int count = Physics.RaycastNonAlloc(ray, results, distance, mask);

        hit = new RaycastHit();

        if (count > 0)
            hit = results[0];

        return count > 0;
    }

    bool CastRay(Vector2 screenPos, out RaycastHit hit) => CastRay(screenPos, out hit, 1000f, int.MaxValue);
}
