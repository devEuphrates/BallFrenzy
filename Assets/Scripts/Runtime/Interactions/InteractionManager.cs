using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] InputReaderSO _inputReader;
    [SerializeField] LayerMask _observedLayers;
    [SerializeField] float _distance = 100f;

    RaycastHit[] _results = new RaycastHit[10];

    [SerializeField] Camera _camera;

    private void OnEnable()
    {
        _inputReader.OnTouchDown += OnTouchDown;
    }

    private void OnDisable()
    {
        _inputReader.OnTouchDown -= OnTouchDown;
    }

    void OnTouchDown(Vector2 pos)
    {
        int cnt = ScreenPointRayCast(pos);
        for (int i = 0; i < cnt; i++)
        {
            Transform sel = _results[i].transform;

            if (!sel.TryGetComponent<IInteractable>(out var interactable))
                continue;

            interactable.Interact();
        }
    }

    int ScreenPointRayCast(Vector2 point)
    {
        Ray ray = _camera.ScreenPointToRay(point);
        int count = Physics.RaycastNonAlloc(ray, _results, _distance, _observedLayers);
        return count;
    }
}
