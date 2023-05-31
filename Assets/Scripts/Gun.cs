using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up, horizontal * 2f);
        transform.Rotate(Vector3.right, vertical * 2f);
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
