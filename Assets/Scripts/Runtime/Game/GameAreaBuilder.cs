using UnityEngine;

public class GameAreaBuilder : MonoBehaviour
{
    [SerializeField] GameObject _bouncerPrefab;
    [SerializeField] int _height = 5;
    [SerializeField] int _firstCount = 3;
    [SerializeField] float _minDistance = 1f;

    public void Build()
    {
        Vector3 parentPos = transform.position;

        for (int i = 0; i < _height; i++)
        {
            int cnt = _firstCount + i;
            float startX = (cnt - 1) * _minDistance * -.5f;

            for (int j = 0; j < cnt; j++)
            {
                GameObject go = Instantiate(_bouncerPrefab);
                Vector3 pos = new Vector3(parentPos.x + startX + j * _minDistance, parentPos.y - i * _minDistance, parentPos.z);

                go.transform.position = pos;
                go.transform.parent = transform;
            }
        }
    }
}
