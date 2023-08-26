using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPropabilityCounter : MonoBehaviour
{
    [SerializeField, Tooltip("List of propabilities. Should have the same amount of elements, as the amount of platform types."), Range(0.1f, 1.0f)]
    private List<float> _platformChance = new();

    [Header("Propability Tweaks")]
    [SerializeField, Tooltip("Propability value decrease for winner, in float. This value will be divided in half and added to losers."), Range(0.05f, 0.25f)]
    private float _propabilityDecrease;

    private float _currentPropability;
    private PlatformManager _manager;
    //Number of platforms in one section
    [SerializeField]
    private int _maxAmountToSpawn;


    private void Start()
    {
        _manager = GetComponent<PlatformManager>();
        _maxAmountToSpawn = GameManager.Instance.StartAmountToSpawn;

    }

    public int CheckPropability()
    {
        float randomFloat = ReturnCurrentChance();

        Debug.Log($"Random number is: {randomFloat}");

        if(0 < randomFloat && randomFloat <= (1 - _platformChance[0]))
        {
            Debug.Log("Now spawning: Normal Platform");

            _platformChance[0] = Mathf.Clamp(_platformChance[0] - _propabilityDecrease, 0.1f, 1.0f);
            _platformChance[2] = Mathf.Clamp(_platformChance[2] + (_propabilityDecrease / 2), 0f, 1.0f);
            _platformChance[1] = Mathf.Clamp(_platformChance[1] + (_propabilityDecrease / 2), 0f, 1.0f);
            _platformChance[3] = Mathf.Clamp(_platformChance[3] + (_propabilityDecrease / 2), 0f, 1.0f);

            return 0;
        }

        if((1 - _platformChance[0]) < randomFloat && randomFloat <= (1 - _platformChance[1]))
        {
            Debug.Log("Now spawning: Long Platform");

            _platformChance[1] = Mathf.Clamp(_platformChance[1] - _propabilityDecrease, 0.1f, 1.0f);
            _platformChance[0] = Mathf.Clamp(_platformChance[0] + (_propabilityDecrease / 2), 0f, 1.0f);
            _platformChance[2] = Mathf.Clamp(_platformChance[2] + (_propabilityDecrease / 2), 0f, 1.0f);
            _platformChance[3] = Mathf.Clamp(_platformChance[3] + (_propabilityDecrease / 2), 0f, 1.0f);

            return 1;
        }

        if((1 - _platformChance[1]) < randomFloat && randomFloat <= (1 - _platformChance[2]))
        {
            Debug.Log("Now spawning: High Jump Platform");

            _platformChance[2] = Mathf.Clamp(_platformChance[2] - _propabilityDecrease, 0.1f, 1.0f);
            _platformChance[0] = Mathf.Clamp(_platformChance[0] + (_propabilityDecrease / 2), 0f, 1.0f);
            _platformChance[1] = Mathf.Clamp(_platformChance[1] + (_propabilityDecrease / 2), 0f, 1.0f);
            _platformChance[3] = Mathf.Clamp(_platformChance[3] + (_propabilityDecrease / 2), 0f, 1.0f);

            return 2;
        }

        if (1 - _platformChance[2] < randomFloat && (randomFloat <= (1 - _platformChance[3]) || randomFloat <= 1.0f))
        {
            Debug.Log("Now spawning: Speed Platform");
            _platformChance[3] = Mathf.Clamp(_platformChance[3] - _propabilityDecrease, 0.1f, 1.0f);
            _platformChance[0] = Mathf.Clamp(_platformChance[0] + (_propabilityDecrease / 2), 0f, 1.0f);
            _platformChance[1] = Mathf.Clamp(_platformChance[1] + (_propabilityDecrease / 2), 0f, 1.0f);
            _platformChance[2] = Mathf.Clamp(_platformChance[2] + (_propabilityDecrease / 2), 0f, 1.0f);
            return 3;
        }

        return -1;
        
    }

    float ReturnCurrentChance()
    {
        if(_manager.CurrentSpawnAmount >= _maxAmountToSpawn)
        {
            _manager.CurrentSpawnAmount = 0;
            _maxAmountToSpawn = Random.Range(1, (_manager.MaxPoolCount / 2) + 1);
            float randomFloat = Random.Range(0.1f, 1.0f);
            _currentPropability = randomFloat;
            return _currentPropability;
        }
        
        return _currentPropability;

    }


}
