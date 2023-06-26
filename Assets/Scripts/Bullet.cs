using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [Tooltip("Used when there are no objects in line of bullet")]
    [SerializeField] float _secondsToDie = 2f;

    int _objectsToHit;
    int _objectsHit;

    public void SetProperties(int hittables)
    {
        _objectsToHit = hittables;

        CheckNoHittable();
    }

    private void CheckNoHittable()
    {
        if (_objectsToHit > 0) return;

        StartCoroutine(DieAfterSeconds(_secondsToDie));
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(new Vector3(0, 0, _speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IHittable>(out var hittable)) return;

        hittable.Hit();

        if (++_objectsHit == _objectsToHit) Destroy(gameObject);
    }

    IEnumerator DieAfterSeconds(float pSeconds)
    {
        yield return new WaitForSeconds(pSeconds);

        Destroy(gameObject);
    }
}
