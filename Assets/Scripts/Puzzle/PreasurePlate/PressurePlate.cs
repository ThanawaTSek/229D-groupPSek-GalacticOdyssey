using System;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private PressurePlateDoor[] doors;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        foreach (PressurePlateDoor pressurePlateDoor in doors)
        {
            pressurePlateDoor.OpenDoor();
        }
    }
}
