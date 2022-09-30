using UnityEngine;

public class CombineableBall : MonoBehaviour
{
    [SerializeField] MeshRenderer _mesh;

    public void ChangeMaterial(Material mat)
    {
        _mesh.material = mat;
    }
}
