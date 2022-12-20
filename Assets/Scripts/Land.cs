using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Land : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private ColliderRenderer _colliderRenderer;

    //private void Awake()
    //{
    //    _meshFilter.ena
    //}

    public void SetPath(List<List<Point>> paths)
    {
        _collider.pathCount = paths.Count;

        for (int i = 0; i < paths.Count; i++)
        {
            List<Vector2> path = new List<Vector2>();

            for (int p = 0; p < paths[i].Count; p++)
            {
                path.Add(paths[i][p].Position);
            }
            _collider.SetPath(i, path);
        }
        _colliderRenderer.CreateMesh();
    }
}