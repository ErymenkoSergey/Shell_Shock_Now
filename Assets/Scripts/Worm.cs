using Mirror;
using UnityEngine;

public class Worm : NetworkBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _wormSprite;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity += new Vector2(0, _jumpSpeed);
            _animator.SetBool("Grounded", false);
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            _wormSprite.localScale = new Vector3(-horizontal, 1f,1f);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = horizontal * _speed;
            _rigidbody.velocity = velocity;
            _animator.SetBool("Walk", true);
        }
        else {
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
}
