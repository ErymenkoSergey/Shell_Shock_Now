using Mirror;
using TMPro;
using UnityEngine;

public abstract class BaseCharacter : NetworkBehaviour
{
    protected GameProcess _gameProcess;

    [SerializeField] protected GameObject[] _bullets;

    [SerializeField] protected TMP_Text _playerNameText;
    [SerializeField] protected TMP_Text _playerScoreText;
    [SerializeField] protected SpriteRenderer _gunMeshRenderer;

    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected float _speed = 2f;
    [SerializeField] protected float _jumpSpeed = 5f;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Transform _wormSprite;

    [SerializeField] protected float _sencetivity = 0.01f;
    [SerializeField] protected float _speedMultiplier = 0.03f;
    [SerializeField] protected Renderer _aimLaserRenderer;
    [SerializeField] protected Renderer _aimLaserRendererRemember;
    [SerializeField] protected Transform _pointerLine;
    [SerializeField] protected Transform _pointerLineRemember;
    [SerializeField] protected GameObject _gun;
}
