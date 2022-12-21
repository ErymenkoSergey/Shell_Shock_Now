using Mirror;
using UnityEngine;

public class Worm : NetworkBehaviour, IMoveble
{
    private GameProcess _gameProcess;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _wormSprite;
    //[SerializeField] private Throwing _throwing;
    [SyncVar] public uint _netId;

    public override void OnStartLocalPlayer()
    {
        _gameProcess = FindObjectOfType<GameProcess>();
        var cam = _gameProcess.GetCamera();
        cam.GetComponent<CameraControl>().SetGameObject(transform);

        var input = _gameProcess.Input;
        input.SetPlayer(this.gameObject);

        _netId = netId;

    }

    private void FixedUpdate()
    {
        if (!isOwned)
            return;

        if (!isLocalPlayer)
            return;

        if (_horizontalMoved != 0)
        {
            _wormSprite.localScale = new Vector3(-_horizontalMoved, 1f, 1f);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = _horizontalMoved * _speed;
            _rigidbody.velocity = velocity;
            _animator.SetBool("Walk", true);
        }
        else
        {
            _animator.SetBool("Walk", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetBool("Grounded", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _animator.SetBool("Grounded", false);
    }

    private float _horizontalMoved;

    public void Move(Controls controls, bool isOn)
    {
        if (!isLocalPlayer)
            return;

        switch (controls)
        {
            case Controls.Left:
                _horizontalMoved = isOn ? -1f : 0f;
                break;
            case Controls.Right:
                _horizontalMoved = isOn ? 1f : 0f;
                break;
        }
    }

    public void RotateMouse(Vector2 vector)
    {
        if (!isLocalPlayer)
            return;

        Rotate(vector);
    }

    public void Bounce()
    {
        if (!isLocalPlayer)
            return;

        Jump();
    }

    private void Jump()
    {
        _rigidbody.velocity += new Vector2(0, _jumpSpeed);
        _animator.SetBool("Grounded", false);
    }

    public void Fire(PressedStatus status)
    {
        if (!isLocalPlayer)
            return;

        Fire2(status);
    }




    Vector2 mouseStart;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _sencetivity = 0.01f;
    [SerializeField] private float _speedMultiplier = 0.03f;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _pointerLine;
    [SerializeField] private GameObject _gun;
    private bool _isProcessAim;
    private bool _isSetStartPosMouse;
    private bool _isFire;
    private Vector3 _delta;
    private Vector3 _velocity;

    private void Start()
    {
        _renderer.enabled = false;
    }

    public void Fire2(PressedStatus status)
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
            _gun.transform.right = delta;
            _pointerLine.localScale = new Vector3(delta.magnitude * _sencetivity, 1, 1);
        }

        if (_isFire)
        {
            _isProcessAim = false;

            _delta = vector - mouseStart;
            _velocity = _delta * _speedMultiplier;

            _renderer.enabled = false;
            CmdCreateBomb();
            _isFire = false;
        }
    }

    [Command]
    public void CmdCreateBomb()
    {
        SetCreateBomb();
    }

    [Server]
    public void SetCreateBomb()
    {
        Bomb newBomb = Instantiate(_bombPrefab, _gun.transform.position, Quaternion.identity);
        newBomb.SetVelocity(_velocity);
    }
}