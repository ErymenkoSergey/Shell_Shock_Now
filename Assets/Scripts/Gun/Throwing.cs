using UnityEngine;

public class Throwing : MonoBehaviour
{
    //Vector3 mouseStart;
    Vector2 mouseStart;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _sencetivity = 0.01f;
    [SerializeField] private float _speedMultiplier = 0.03f;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _pointerLine;

    private void Start()
    {
        _renderer.enabled = false;
    }

    //private void Update()
    //{
    //    //if (Input.GetMouseButtonDown(0))
    //    //{
    //    //    _renderer.enabled = true;
    //    //    mouseStart = Input.mousePosition;
    //    //}
    //    if (Input.GetMouseButton(0))
    //    {
    //        //Vector3 delta = Input.mousePosition - mouseStart;
    //        //transform.right = delta;
    //        //_pointerLine.localScale = new Vector3(delta.magnitude * _sencetivity, 1, 1);
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        //Vector3 delta = Input.mousePosition - mouseStart;
    //        //Vector3 velocity = delta * _speedMultiplier;

    //        //_renderer.enabled = false;
    //        //Bomb newBomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
    //        //newBomb.SetVelocity(velocity);
    //    }
    //}

    private bool _isProcessAim;
    private bool _isSetStartPosMouse;
    private bool _isFire;

    public void Fire(PressedStatus status)
    {
        if (status == PressedStatus.Down)
        {
            _renderer.enabled = true;
            _isSetStartPosMouse = true;
           
            //mouseStart = Input.mousePosition;
        }

        if (status == PressedStatus.Pressed) //?? 
        {
            //Vector3 delta = Input.mousePosition - mouseStart;
            //transform.right = delta;
            //_pointerLine.localScale = new Vector3(delta.magnitude * _sencetivity, 1, 1);
        }

        if (status == PressedStatus.Up)
        {
            _isFire = true;
            //Vector3 delta = Input.mousePosition - mouseStart;
            //Vector3 velocity = delta * _speedMultiplier;

            //_renderer.enabled = false;
            //Bomb newBomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            //newBomb.SetVelocity(_velocity);

        }
    }

    private Vector3 _delta;
    private Vector3 _velocity;

    public void Rotate(Vector2 vector)
    {
        if (_isSetStartPosMouse)
        {
            //mouseStart = Input.mousePosition;
            mouseStart = vector;
            _isProcessAim = true;
        }

        if (_isProcessAim)
        {
            _isSetStartPosMouse = false;

           // Vector3 delta = Input.mousePosition - mouseStart;
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
