using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundManager : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Platform") || other.CompareTag("StartingPlatform")
            || other.CompareTag("DoublePlatform") || other.CompareTag("GoodPlatform"))
        {
            other.gameObject.SetActive(false);
        }

    }
}
