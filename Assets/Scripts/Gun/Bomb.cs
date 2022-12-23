using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Cutter _cut;
    private bool _dead;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _destroyTime = 15f;

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
        //Invoke(nameof(DoCut), 0.002f);
        //_dead = true;

        StartCoroutine(StartCut());
    }

    private IEnumerator StartCut()
    {
        yield return new WaitForSeconds(0.01f);
        DoCut();
        _dead = true;
    }

    private void DoCut() 
    {
        _cut.DoCut();
        Destroy(gameObject);
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }
}
