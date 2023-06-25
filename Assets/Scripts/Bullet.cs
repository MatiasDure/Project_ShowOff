using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;

    int _objectsToHit;
    int _objectsHit;

    public void SetProperties(Quaternion newRot, int hittables)
    {
        transform.rotation = newRot;
        _objectsToHit = hittables;
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
        Debug.Log("okay");
        if (!other.CompareTag("Balloon")) return;

        _objectsHit++;
        other.GetComponent<IHittable>().Hit();

        if (_objectsHit == _objectsToHit) Destroy(gameObject);
    }
}
