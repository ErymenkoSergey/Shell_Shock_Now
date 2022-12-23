using UnityEngine;

[ExecuteAlways]
public class Circle : MonoBehaviour
{
    [SerializeField] private ColliderRenderer _colliderRenderer;
    [SerializeField] private int _sides;
    [SerializeField] private PolygonCollider2D _collider;

    private void OnValidate()
    {
        CreateCircle();
    }

    private void CreateCircle()
    {
        _collider.CreatePrimitive(_sides, Vector2.one);
        _colliderRenderer.CreateMesh();
    }

    public void SetSize(bool Mult, float value)
    {
        if (Mult)
            transform.localScale *= value;
        else
            transform.localScale /= value; 
    }
}
