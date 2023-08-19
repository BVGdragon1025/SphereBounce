using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxParent : MonoBehaviour
{
    public float defaultParallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        defaultParallaxSpeed = PlatformManager.Instance.CurrentPlatformSpeed;
    }
}
