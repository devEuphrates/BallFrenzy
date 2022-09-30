using TMPro;
using UnityEngine;

public class MultiplierText : MonoBehaviour
{
    [SerializeField] TextMeshPro _text;

    public void UpdateText(float multiplier) => _text.text = $"x{multiplier.ToString("#.#")}";
}
