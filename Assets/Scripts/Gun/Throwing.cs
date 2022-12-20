using UnityEngine;

public class Throwing : MonoBehaviour
{
    Vector3 mouseStart;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _sencetivity = 0.01f;
    [SerializeField] private float _speedMultiplier = 0.03f;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _pointerLine;

    private void Start()
    {
        _renderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _renderer.enabled = true;
            mouseStart = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - mouseStart;
            transform.right = delta;
            _pointerLine.localScale = new Vector3(delta.magnitude * _sencetivity, 1, 1);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 delta = Input.mousePosition - mouseStart;
            Vector3 velocity = delta * _speedMultiplier;

            _renderer.enabled = false;
            Bomb newBomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            newBomb.SetVelocity(velocity);
        }
    }
}
