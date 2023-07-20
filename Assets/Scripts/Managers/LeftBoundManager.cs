using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundManager : MonoBehaviour
{
    [SerializeField]
    private int _platformCount;

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log($"Current collision with LeftBound: {other.gameObject.tag}");

        if (other.gameObject.CompareTag("Platform"))
        {
            GameObject platform = PlatformManager.Instance.GetPooledObject();
            //Debug.Log($"Platform from pool status: {platform}");

            if (platform != null)
            {
                _platformCount++;
                //Debug.Log($"Platform spawn location before 1: {platform.transform.position}, platform spawn: {platform.transform.localPosition}");
                SpawnPlatform(platform, _platformCount);
                //Debug.Log($"Platform spawn location after: {platform.transform.position}, platform spawn: {platform.transform.localPosition}");
                
            }

            other.gameObject.SetActive(false);
        }
    }

    private void SpawnPlatform(GameObject platform, int platformsCount)
    {
        if(platformsCount == 1 || (platformsCount % 5 == 0))
        {
            platform.transform.position = new Vector3(PlatformManager.Instance.SpaceBetweenPlatforms * 3, PlatformManager.Instance.transform.position.y, PlatformManager.Instance.transform.position.z);
        }
        else
        {
            platform.transform.position = PlatformManager.Instance.transform.position;
        }
        platform.SetActive(true);
    }

}
