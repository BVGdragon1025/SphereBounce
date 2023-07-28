using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundManager : MonoBehaviour
{
    [SerializeField]
    private int _platformCount;
    [SerializeField]
    private int _maxPlatformCount;

    private void Start()
    {
        _maxPlatformCount = 1;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("StartingPlatform"))
        {
            GameObject platform = PlatformManager.Instance.GetPooledObject(_maxPlatformCount);

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
        if(platformsCount == _maxPlatformCount)
        {
            platform.transform.position = new Vector3(PlatformManager.Instance.GetSpaceBetweenPlatforms * 3, PlatformManager.Instance.transform.position.y, PlatformManager.Instance.transform.position.z);
            _maxPlatformCount = Random.Range(0, PlatformManager.Instance.MaxPoolCount);
        }
        else
        {
            platform.transform.position = PlatformManager.Instance.transform.position;
        }
        platform.SetActive(true);
    }

}
