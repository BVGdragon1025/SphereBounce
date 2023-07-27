using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundManager : MonoBehaviour
{
    [SerializeField]
    private int _platformCount;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("StartingPlatform"))
        {
            GameObject platform = PlatformManager.Instance.GetPooledObject();

            if (platform != null)
            {
                _platformCount++;
                SpawnPlatform(platform, _platformCount);
            }

            other.gameObject.SetActive(false);
        }
    }

    private void SpawnPlatform(GameObject platform, int platformsCount)
    {
        if(platformsCount == 1 || (platformsCount % 5 == 0))
        {
            platform.transform.position = new Vector3(PlatformManager.Instance.GetSpaceBetweenPlatforms * 3, PlatformManager.Instance.transform.position.y, PlatformManager.Instance.transform.position.z);
        }
        else
        {
            platform.transform.position = PlatformManager.Instance.transform.position;
        }
        platform.SetActive(true);
    }

}
