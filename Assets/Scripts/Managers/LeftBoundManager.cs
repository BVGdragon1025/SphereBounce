using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log($"Current collision with LeftBound: {other.gameObject.tag}");

        if (other.gameObject.CompareTag("Platform"))
        {
            GameObject platform = PlatformManager.Instance.GetPooledObject();
            //Debug.Log($"Platform from pool status: {platform}");

            if (platform != null)
            {
                //Debug.Log($"Platform spawn location before 1: {platform.transform.position}, platform spawn: {platform.transform.localPosition}");
                platform.transform.position = PlatformManager.Instance.transform.position;
                platform.SetActive(true);
                //Debug.Log($"Platform spawn location after: {platform.transform.position}, platform spawn: {platform.transform.localPosition}");
                
            }

            other.gameObject.SetActive(false);
        }
    }
}
