using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Cutter _cut;
    private bool _dead;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _destroyTime = 15f;
    [SerializeField] private float _radius;

    public void SetVelocity(Vector2 value) 
    {
        _rigidbody.velocity = value;
        _rigidbody.AddTorque(Random.Range(0,18f));
    }

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_dead) 
            return;

        //_cut.transform.position = transform.position;
        _dead = true;
        SetRadius();
        StartCoroutine(StartCut());
    }

    private IEnumerator StartCut()
    {
        yield return new WaitForSeconds(0.01f);
        DoCut();
    }

    private void DoCut() 
    {
        _cut.DoCut();
        Destroy(gameObject);
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }

    public void SetRadius()
    {
        _cut.transform.localScale *= _radius;
    }
}
