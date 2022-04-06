using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehaviour : MonoBehaviour
{
    [Header("Movement")] public float speed = 50f;

    void Update()
    {
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + movement);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;
    
        RemoveProjectile();
    }

    void RemoveProjectile()
    {
        gameObject.SetActive(false);
    }
}