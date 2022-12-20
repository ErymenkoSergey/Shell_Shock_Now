using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Cutter _cut;
    private bool _dead;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _explosionPrefab;
    private float _destroyTime = 5f;

    public void SetVelocity(Vector2 value) 
    {
        _rigidbody.velocity = value;
        _rigidbody.AddTorque(Random.Range(-8f,8f));
    }

    private void Start()
    {
        _cut = FindObjectOfType<Cutter>();
        StartCoroutine(AutoDestroy());
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(_destroyTime);
        DoCut();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_dead) 
            return;

        _cut.transform.position = transform.position;
        Invoke(nameof(DoCut), 0.001f);
        _dead = true;
    }

    private void DoCut() 
    {
        _cut.DoCut();
        Destroy(gameObject);
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }
}
