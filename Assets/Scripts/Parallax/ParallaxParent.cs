using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxParent : MonoBehaviour
{
    public float defaultParallaxSpeed;

    private PlatformManager _manager;

    // Start is called before the first frame update
    void Awake()
    {
        _manager = GameObject.FindGameObjectWithTag("PlatformManager").GetComponent<PlatformManager>();
        defaultParallaxSpeed = _manager.CurrentPlatformSpeed;
    }

    private void Update()
    {
        defaultParallaxSpeed = _manager.CurrentPlatformSpeed;
    }
}
