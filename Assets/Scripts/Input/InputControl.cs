using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Controls
{
    None = 0,
    Jump = 1,
    Left = 2,
    Right = 3,
    Fire = 4
}

public enum PressedStatus
{
    None = 0,
    Down = 1,
    Pressed = 2,
    Up = 3
}

public class InputControl : CommonBehaviour
{
    [SerializeField] private GameObject _player;
    private IMoveble _iMoveblePlayer;

    [SerializeField] private InputActionAsset _inputActions;
    private InputActionMap _playerActionMap;

    private InputAction _left;
    private InputAction _right;
    private InputAction _mousePos;
    private InputAction _jump;
    private InputAction _fire;

    private bool _isPlayerOn;

    public Vector2 MousePos { get; private set; }

    private void OnEnable()
    {
        SetLinks();
        Subscribe();

    }

    private void OnDisable()
    {
        UnSubscribe();

    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
        SetPlayer();
    }

    private void SetPlayer()
    {
        if (_player.gameObject.TryGetComponent(out IMoveble moveble))
        {
            _iMoveblePlayer = moveble;
            _isPlayerOn = true;
        }
    }

    private void SetLinks()
    {
        _playerActionMap = _inputActions.FindActionMap("Player");

        _left = _playerActionMap.FindAction("LeftMove");
        _right = _playerActionMap.FindAction("RightMove");
        _mousePos = _playerActionMap.FindAction("Mouse");
        _fire = _playerActionMap.FindAction("Fire");
        _jump = _playerActionMap.FindAction("Jump");
    }

    private void Subscribe()
    {
        _left.started += LeftMove;
        _left.canceled += LeftMove;

        _right.started += RightMove;
        _right.canceled += RightMove;

        _mousePos.started += MousePosition;
        _mousePos.performed += MousePosition;
        _mousePos.canceled += MousePosition;

        _fire.started += Fire;
        _fire.performed += Fire;
        _fire.canceled += Fire;


        _jump.started += Jump;

        _playerActionMap.Enable();
        _inputActions.Enable();
    }

    private void UnSubscribe()
    {
        _left.started -= LeftMove;
        _left.canceled -= LeftMove;
        _right.started -= RightMove;
        _right.canceled -= RightMove;

        _mousePos.started -= MousePosition;
        _mousePos.performed -= MousePosition;
        _mousePos.canceled -= MousePosition;

        _fire.started -= Fire;
        _fire.performed -= Fire;
        _fire.canceled -= Fire;

        _jump.started -= Jump;

        _playerActionMap.Disable();
        _inputActions.Disable();
    }

    private void LeftMove(InputAction.CallbackContext Context)
    {
        if (Context.started)
        {
            _iMoveblePlayer.Move(Controls.Left, true);
        }
        if (Context.canceled)
        {
            _iMoveblePlayer.Move(Controls.Left, false);
        }
    }

    private void RightMove(InputAction.CallbackContext Context)
    {
        if (Context.started)
        {
            _iMoveblePlayer.Move(Controls.Right, true);
        }
        if (Context.canceled)
        {
            _iMoveblePlayer.Move(Controls.Right, false);
        }
    }

    private void MousePosition(InputAction.CallbackContext Context)
    {
        if (!_isPlayerOn)
            return;

        var pos = Context.ReadValue<Vector2>();
        _iMoveblePlayer.RotateMouse(pos);

        MousePos = pos;
    }

    private void Jump(InputAction.CallbackContext Context)
    {
        _iMoveblePlayer.Bounce();
    }

    private void Fire(InputAction.CallbackContext Context)
    {

        if (Context.started)
        {
            _iMoveblePlayer.Fire(PressedStatus.Down);
        }

        if (Context.performed)
        {
            _iMoveblePlayer.Fire(PressedStatus.Pressed);
        }

        if (Context.canceled)
        {
            _iMoveblePlayer.Fire(PressedStatus.Up);
        }
    }
}
