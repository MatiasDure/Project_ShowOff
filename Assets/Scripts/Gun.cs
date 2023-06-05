using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = .2f;

    LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 lineEnd = transform.position + transform.forward * 20f;
        lineRenderer.SetPositions(new Vector3[] { transform.position, lineEnd });
        if (Input.GetKeyDown(KeyCode.M)) Shoot();

        RotateGun();
    }

    private void RotateGun()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
               
        transform.Rotate(Vector3.up, horizontal * _rotateSpeed);
        transform.Rotate(Vector3.right * -1, vertical * _rotateSpeed);


        //Debug.Log("X: " + transform.rotation.eulerAngles.x);
        //Debug.Log("Y: " + transform.rotation.eulerAngles.y);

        float angleX = transform.rotation.eulerAngles.x;
        float angleY = transform.rotation.eulerAngles.y;

        float newX = angleX > 180 ? angleX - 360 : angleX;
        float newY = angleY > 180 ? angleY - 360 : angleY;

        newX = Mathf.Clamp(newX, -22f, 6f);
        newX = newX < 0 ? 360 - newX * -1 : newX;
        
        newY = Mathf.Clamp(newY, -37f, 37f);
        newY = newY < 0 ? 360 - newY * -1 : newY;

        Quaternion rotation = new();
        rotation.eulerAngles = new Vector3(newX, newY, 0);

        transform.rotation = rotation;
    }

    private void Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

        foreach (RaycastHit hit in hits)
        {
            if (!hit.transform.TryGetComponent<IHittable>(out IHittable hittable)) continue;

            hittable.Hit();
        }

        //Debug.Log(hits.Length);
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red, .1f);
    }
}
