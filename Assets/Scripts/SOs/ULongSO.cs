using Euphrates;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unsigned Long SO", menuName = "SO Variables/Unsigned Long")]
public class ULongSO : SOVariable<ulong>
{
    protected override ulong SubtractInternal(ulong x, ulong y) => x - y;

    public static implicit operator ulong(ULongSO so) => so.Value;
    public static explicit operator ULongSO(ulong value)
    {
        ULongSO rval = ScriptableObject.CreateInstance<ULongSO>();
        rval.Value = value;

        return rval;
    }
}
