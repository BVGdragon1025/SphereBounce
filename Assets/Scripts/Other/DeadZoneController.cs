using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SphereController.Instance.MarkAsDead();
        }
    }
}
