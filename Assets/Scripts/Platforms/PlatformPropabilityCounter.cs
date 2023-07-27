using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPropabilityCounter : MonoBehaviour
{
    [Header("Propability Section")]
    [SerializeField, Tooltip("Propability of normal platform spawn, in float"), Range(0.1f, 1.0f)]
    private float _normalPlatformChance;
    [SerializeField, Tooltip("Propability of long platform spawn, in float"), Range(0.1f, 1.0f)]
    private float _longPlatformChance;
    [SerializeField, Tooltip("Propability of double platform spawn, in float"), Range(0.1f, 1.0f)]
    private float _doublePlatformChance;

    [Header("Propability Tweaks")]
    [SerializeField, Tooltip("Winner propability value decrease, in float. This value will be divided in half and added to losers."), Range(0.05f, 0.25f)]
    private float _propabilityDecrease;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CheckPropability()
    {
        float randomFloat = Random.Range(0.1f, 1.0f);

        Debug.Log($"Random number is: {randomFloat}");

        if(randomFloat >= (1 - _normalPlatformChance))
        {
            Debug.Log("Now spawning: Normal Platforms");
            return 0;
        }

        if(randomFloat >= (1 - _doublePlatformChance))
        {
            Debug.Log("Now Spawning: Double Platforms");
            return 1;
        }

        if(randomFloat >= (1 - _longPlatformChance))
        {
            Debug.Log("Now spawning: Long Platforms");
            return 2;
        }

        return -1;
        
    }
}
