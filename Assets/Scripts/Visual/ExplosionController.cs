using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private float _destroyTime;

    private void Awake()
    {
        StartCoroutine(Destroying());
    }

    private IEnumerator Destroying()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }
}
