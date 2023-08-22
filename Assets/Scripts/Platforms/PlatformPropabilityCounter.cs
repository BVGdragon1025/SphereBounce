using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPropabilityCounter : MonoBehaviour
{
    [SerializeField, Tooltip("List of propabilities. Should have the same amount of elements, as the amount of platform types."), Range(0.1f, 1.0f)]
    private List<float> _platformChance = new();

    [Header("Propability Tweaks")]
    [SerializeField, Tooltip("Winner propability value decrease, in float. This value will be divided in half and added to losers."), Range(0.05f, 0.25f)]
    private float _propabilityDecrease;


    public int CheckPropability()
    {
        float randomFloat = Random.Range(0.1f, 1.0f);

        Debug.Log($"Random number is: {randomFloat}");

        if(0 < randomFloat && randomFloat <= (1 - _platformChance[0]))
        {
            Debug.Log("Now spawning: Normal Platform");
            /*
            _normalPlatformChance = Mathf.Clamp(_normalPlatformChance - _propabilityDecrease, 0.1f, 1.0f);
            _doublePlatformChance = Mathf.Clamp(_doublePlatformChance + (_propabilityDecrease / 2), 0f, 1.0f);
            _longPlatformChance = Mathf.Clamp(_longPlatformChance + (_propabilityDecrease / 2), 0f, 1.0f);
            */
            return 0;
        }

        if((1 - _platformChance[0]) < randomFloat && randomFloat <= (1 - _platformChance[1]))
        {
            Debug.Log("Now spawning: Long Platform");
            /*
            _longPlatformChance = Mathf.Clamp(_longPlatformChance - _propabilityDecrease, 0.1f, 1.0f);
            _normalPlatformChance = Mathf.Clamp(_normalPlatformChance + (_propabilityDecrease / 2), 0f, 1.0f);
            _doublePlatformChance = Mathf.Clamp(_doublePlatformChance + (_propabilityDecrease / 2), 0f, 1.0f);
            */
            return 1;
        }

        if((1 - _platformChance[1]) < randomFloat && randomFloat <= (1 - _platformChance[2]))
        {
            Debug.Log("Now spawning: High Jump Platform");
            /*
            _doublePlatformChance = Mathf.Clamp(_doublePlatformChance - _propabilityDecrease, 0.1f, 1.0f);
            _normalPlatformChance = Mathf.Clamp(_normalPlatformChance + (_propabilityDecrease / 2), 0f, 1.0f);
            _longPlatformChance = Mathf.Clamp(_longPlatformChance + (_propabilityDecrease / 2), 0f, 1.0f);
            */
            return 2;
        }

        if (1 - _platformChance[2] < randomFloat && (randomFloat <= (1 - _platformChance[3]) || randomFloat <= 1.0f))
        {
            Debug.Log("Now spawning: Speed Platform");
            return 3;
        }

         
        
        return -1;
        
    }


}
