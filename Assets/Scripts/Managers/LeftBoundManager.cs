using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Vector3 spawnLocation = new(PlatformManager.Instance.transform.position.x, PlatformManager.Instance.transform.position.y, PlatformManager.Instance.transform.position.z);


        Debug.Log($"Current collision with LeftBound: {other.gameObject.tag}");

        if (other.gameObject.CompareTag("Platform"))
        {
            GameObject platform = PlatformManager.Instance.GetPooledObject();
            Debug.Log($"Platform from pool status: {platform}");

            if (platform != null)
            {
                platform.transform.position = spawnLocation;
                platform.SetActive(true);
                
            }

            other.gameObject.SetActive(false);
        }
    }
}
