using System;
using UnityEngine;

public class SeeSaw : MonoBehaviour
{
    [SerializeField] private float maxAngle = 90;
    [SerializeField] private float angleSpeed = 30;
    [SerializeField] private float maxAngularDistance = 10;
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Vector3 direction = other.transform.position - transform.position ;
        direction.Normalize();
        float distance = transform.position.x - other.transform.position.x;
        Debug.Log(distance);
        transform.Rotate(Vector3.forward, angleSpeed * Time.deltaTime * (distance / maxAngularDistance));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * transform.localScale.y, transform.right * maxAngularDistance);
        Gizmos.DrawRay(transform.position + Vector3.up * transform.localScale.y, -transform.right * maxAngularDistance);
    }
}
