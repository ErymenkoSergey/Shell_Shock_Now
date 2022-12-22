using UnityEngine;

[ExecuteAlways]
public class Circle : MonoBehaviour
{
    [SerializeField] private ColliderRenderer _colliderRenderer;
    [SerializeField] private int _sides;
    [SerializeField] private PolygonCollider2D _collider;
    //[SerializeField] private InputControl inputControl;

    private void OnValidate()
    {
        CreateCircle();
    }

    //private void Update()
    //{
    //    if (Application.isPlaying)
    //    {
    //        float scroll = Input.mouseScrollDelta.y; //inputControl.MousePos.y
    //        if (scroll != 0)
    //        {
    //            _sides += (int)scroll;
    //            CreateCircle();
    //        }
    //        if (Input.GetKeyDown(KeyCode.Equals))
    //        {
    //            transform.localScale *= 1.1f;
    //        }
    //        if (Input.GetKeyDown(KeyCode.Minus))
    //        {
    //            transform.localScale /= 1.1f;
    //        }
    //    }
    //}

    private void CreateCircle()
    {
        _collider.CreatePrimitive(_sides, Vector2.one);
        _colliderRenderer.CreateMesh();
    }
}
