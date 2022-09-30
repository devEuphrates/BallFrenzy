using UnityEngine;

public class ButtonClickHandler : MonoBehaviour, IInteractable
{
    [SerializeReference] BallManagerSO _ballManager;
    IAnim _pushAnim;

    private void Awake()
    {
        _pushAnim = GetComponent<IAnim>();
    }

    public void Interact()
    {
        _ballManager.InvokeSpawn();
        _pushAnim.Play();
    }
}
