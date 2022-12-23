using Mirror;
using TMPro;
using UnityEngine;

public class Worm : NetworkBehaviour, IMoveble
{
    private GameProcess _gameProcess;
    [SyncVar(hook = nameof(UpdatePlayerName))] public string PlayerName;
    [SyncVar(hook = nameof(UpdateHitCount))] public int Score;
    [SyncVar(hook = nameof(UpdateColor))] public Color PlayerColor;

    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playerScoreText;
    public SpriteRenderer _gunMeshRenderer;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private Animator _animator;
    public Transform _wormSprite;
    [SyncVar] public float _horizontalMoved;

    Vector2 mouseStart;
    [SerializeField] private Renderer _aimLaserRenderer;
    [SerializeField] private float _sencetivity = 0.01f;
    [SerializeField] private float _speedMultiplier = 0.03f;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _pointerLine;
    [SerializeField] private GameObject _gun;

    public bool _isProcessAim;
    public bool _isSetStartPosMouse;
    public bool _isFire;
    public Vector3 _delta;
    public Vector3 _velocity;

    public override void OnStartLocalPlayer()
    {
        _gameProcess = FindObjectOfType<GameProcess>();
        var cam = _gameProcess.GetCamera();
        cam.GetComponent<CameraControl>().SetGameObject(transform);

        var module = FindObjectOfType<GameNetConfigurator>();
        CmdSetPlayerName(module.GetName());

        IsAimLaserRenderer(false);
        var input = _gameProcess.Input;
        input.SetPlayer(gameObject);
    }

    [Client]
    private void UpdatePlayerName(string oldName, string newName)
    {
        _playerNameText.text = newName;
    }

    [Client]
    private void UpdateColor(Color oldColor, Color newColor)
    {
        _playerNameText.color = newColor;
        _playerScoreText.color = newColor;
    }

    [Client]
    private void UpdateHitCount(int oldScore, int newScore)
    {
        _playerScoreText.text = $"Score: {newScore}";
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        PlayerName = name;
    }

    [Client]
    private void FixedUpdate()
    {
        if (!isOwned)
            return;

        if (!isLocalPlayer)
            return;

        if (_horizontalMoved != 0)
        {
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = _horizontalMoved * _speed;
            _rigidbody.velocity = velocity;
        }
    }

    [Command]
    private void CmdRotateCharacter()
    {
        RpcRotateCharacter();
    }

    [ClientRpc]
    private void RpcRotateCharacter()
    {
        if (_horizontalMoved != 0f)
            _wormSprite.localScale = new Vector3(-_horizontalMoved, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetAnimation("Grounded", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetAnimation("Grounded", false);
    }

    public void SetAnimation(string name, bool isOn)
    {
        _animator.SetBool(name, isOn);
    }

    public void Move(Controls controls, bool isOn)
    {
        if (!isLocalPlayer)
            return;

        CmdMoved(controls, isOn);
    }

    [Command]
    public void CmdMoved(Controls controls, bool isOn)
    {
        SetMoved(controls, isOn);
    }

    [ClientRpc]
    public void SetMoved(Controls controls, bool isOn)
    {
        Moved(controls, isOn);
    }

    public void Moved(Controls controls, bool isOn)
    {
        switch (controls)
        {
            case Controls.Left:
                _horizontalMoved = isOn ? -1f : 0f;
                break;
            case Controls.Right:
                _horizontalMoved = isOn ? 1f : 0f;
                break;
        }

        SetStatusAnim();
    }

    private void SetStatusAnim()
    {
        CmdRotateCharacter();

        if (_horizontalMoved != 0f)
            SetAnimation("Walk", true);
        else
            SetAnimation("Walk", false);
    }

    public void JumpUp()
    {
        if (!isLocalPlayer)
            return;

        Jump();
    }

    private void Jump()
    {
        _rigidbody.velocity += new Vector2(0, _jumpSpeed);
        SetAnimation("Grounded", false);
    }

    public void Fire(PressedStatus status)
    {
        if (!isLocalPlayer)
            return;

        CmdFire(status);
    }

    [Command]
    public void CmdFire(PressedStatus status)
    {
        RpcFire(status);
    }

    [ClientRpc]
    public void RpcFire(PressedStatus status)
    {
        SetFire(status);
    }

    public void SetFire(PressedStatus status)
    {
        if (status == PressedStatus.Down)
        {
            if (isLocalPlayer)
                IsAimLaserRenderer(true);
            _isSetStartPosMouse = true;
        }

        if (status == PressedStatus.Up)
        {
            _isFire = true;
        }
    }

    public void RotateMouse(Vector2 vector)
    {
        if (!isLocalPlayer)
            return;

        CmdRotate(vector);
    }

    [Command]
    public void CmdRotate(Vector2 vector)
    {
        RpcRotate(vector);
    }

    [ClientRpc]
    public void RpcRotate(Vector2 vector)
    {
        SetRotate(vector);
    }

    public void SetRotate(Vector2 vector)
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

            IsAimLaserRenderer(false);
            SetCreateBomb();
            _isFire = false;
        }
    }

    [Client]
    private void IsAimLaserRenderer(bool isOn)
    {
        _aimLaserRenderer.enabled = isOn;
    }

    public void SetCreateBomb()
    {
        Bomb newBomb = Instantiate(_bombPrefab, _gun.transform.position, Quaternion.identity);
        newBomb.SetVelocity(_velocity);
    }
}