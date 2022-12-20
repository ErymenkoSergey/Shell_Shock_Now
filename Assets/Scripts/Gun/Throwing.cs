using UnityEngine;

public class Throwing : MonoBehaviour
{
    Vector2 mouseStart;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _sencetivity = 0.01f;
    [SerializeField] private float _speedMultiplier = 0.03f;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _pointerLine;
    private bool _isProcessAim;
    private bool _isSetStartPosMouse;
    private bool _isFire;
    private Vector3 _delta;
    private Vector3 _velocity;

    private void Start()
    {
        _renderer.enabled = false;
    }

    public void Fire(PressedStatus status)
    {
        if (status == PressedStatus.Down)
        {
            _renderer.enabled = true;
            _isSetStartPosMouse = true;
        }

        if (status == PressedStatus.Pressed) 
        {

        }

        if (status == PressedStatus.Up)
        {
            _isFire = true;
        }
    }

    public void Rotate(Vector2 vector)
    {
        if (_isSetStartPosMouse)
        {
            mouseStart = vector;
            _isProcessAim = true;
        }

        if (_isProcessAim)
        {
            _isSetStartPosMouse = false;

            Vector2 delta = vector - mouseStart;
            transform.right = delta;
            _pointerLine.localScale = new Vector3(delta.magnitude * _sencetivity, 1, 1);
        }

        if (_isFire)
        {
            _isProcessAim = false;

            _delta = vector - mouseStart;
            _velocity = _delta * _speedMultiplier;
            
            _renderer.enabled = false;
            Bomb newBomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            newBomb.SetVelocity(_velocity);
            _isFire = false;
        }
    }
}
