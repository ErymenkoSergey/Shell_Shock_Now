using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class ColliderRenderer : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private MeshFilter _meshFilter;

    private void Start()
    {
        CreateMesh();
    }

    private void Update()
    {
        if (transform.hasChanged)
            CreateMesh();
    }

    public void CreateMesh()
    {
        Mesh mesh = _collider.CreateMesh(true, true);

        if (_meshFilter != null)
            _meshFilter.mesh = mesh;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int p = 0; p < _collider.pathCount; p++)
        {
            for (int i = 0; i < _collider.GetPath(p).Length; i++)
            {
                Handles.Label(_collider.transform.TransformPoint(_collider.GetPath(p)[i]), i.ToString());
            }
        }
    }

#endif
}
