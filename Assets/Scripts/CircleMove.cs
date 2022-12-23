//using UnityEngine;

//public class CircleMove : MonoBehaviour
//{
//    private Camera _camera;
//    private Plane _plane = new Plane(Vector3.forward, Vector3.zero);
//    [SerializeField] private InputControl inputControl;

//    private void Start()
//    {
//        _camera = Camera.main;
//    }

//    private void Update() // выреззанно  2
//    {
//        if (Input.GetMouseButton(0))
//        {
//            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
//            // Ray ray = _camera.ScreenPointToRay(inputControl.MousePos);
//            if (_plane.Raycast(ray, out float distance))
//            {
//                Vector3 position = ray.GetPoint(distance);
//                transform.position = position;
//            }
//        }
//    }
//}
