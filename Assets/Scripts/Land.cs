using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Land : CommonBehaviour
{
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private ColliderRenderer _colliderRenderer;
    [SerializeField] private MeshRenderer _renderer;

    private void OnEnable()
    {
        _colliderRenderer.OnMeshCreat += LandRendererOn;
    }

    private void OnDisable()
    {
        _colliderRenderer.OnMeshCreat -= LandRendererOn;
    }

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
        LandRendererOn();
    }


    public PolygonCollider2D GetPolygon()
    {
        return _collider;
    }

    private void LandRendererOn()
    {
        _renderer.enabled = true;
    }
}