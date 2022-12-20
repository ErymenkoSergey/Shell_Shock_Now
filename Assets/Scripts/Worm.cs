using Mirror;
using UnityEngine;

public class Worm : CommonBehaviour, IMoveble
{
    private GameProcess _gameProcess;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _wormSprite;
    [SerializeField] private Throwing _throwing;

    private void Start()
    {
        _gameProcess = FindObjectOfType<GameProcess>();
        var cam = _gameProcess.GetCamera();
        cam.GetComponent<CameraControl>().SetGameObject(transform);

        var input = _gameProcess.Input;
        input.SetPlayer(this.gameObject);

    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        _rigidbody.velocity += new Vector2(0, _jumpSpeed);
    //        _animator.SetBool("Grounded", false);
    //    }
    //}

    private void FixedUpdate()
    {
        //if (!isLocalPlayer)
        //    return;
        //float horizontal = Input.GetAxisRaw("Horizontal");
        if (_horizontalMoved != 0)
        {
            _wormSprite.localScale = new Vector3(-_horizontalMoved, 1f,1f);
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
        //if (!isLocalPlayer)
        //    return;

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
        //if (!isLocalPlayer)
        //    return;
        _throwing.Rotate(vector);
    }

    public void Bounce()
    {
        //if (!isLocalPlayer)
        //    return;
        Jump();
    }

    private void Jump()
    {
        _rigidbody.velocity += new Vector2(0, _jumpSpeed);
        _animator.SetBool("Grounded", false);
    }

    public void Fire(PressedStatus status)
    {
        //if (!isLocalPlayer)
        //    return;
        _throwing.Fire(status);
    }
}
