using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] Slot[] _slots = new Slot[5];

    public int Dragged = -1;

    private void Start()
    {
        SetSlots();
    }

    void SetSlots()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].SetSlot();
        }
    }
}
