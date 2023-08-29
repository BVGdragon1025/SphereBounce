using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPropabilityCounter : MonoBehaviour
{
    [SerializeField, Tooltip("List of propabilities. Should have the same amount of elements, as the amount of platform types."), Range(0.1f, 1.0f)]
    private List<float> _platformChance = new();
    [SerializeField, Tooltip("Special platform propability"), Range(0.0f, 1.0f)]
    private float _specialPlatformChance;
    [SerializeField, Tooltip("Specifies after how many platforms special should spawn;")]
    private int _specialPlatformUnlock;

    private float _currentPropability;
    private float _currentSpecialPropability;
    private int _currentSpecialPlatform;
    private PlatformManager _manager;
    private PlatformPoolsComponent _poolsComponent;

    //Number of platforms in one section
    [SerializeField]
    private int _maxAmountToSpawn;
    public int MaxAmountToSpawn { get { return _maxAmountToSpawn; } set { _maxAmountToSpawn = value; } }

    private void Start()
    {
        _manager = GetComponent<PlatformManager>();
        _poolsComponent = GetComponent<PlatformPoolsComponent>();
        _maxAmountToSpawn = GameManager.Instance.StartAmountToSpawn;

    }

    public int CheckPropability()
    {
        float randomFloat = ReturnCurrentChance();

        Debug.Log($"Random number is: {randomFloat}");

        if(0 < randomFloat && randomFloat <= (1 - _platformChance[0]))
        {
            return 0;
        }

        if((1 - _platformChance[0]) < randomFloat && randomFloat <= (1 - _platformChance[1]))
        {
            return 1;
        }

        if((1 - _platformChance[1]) < randomFloat && randomFloat <= (1 - _platformChance[2]))
        {
            return 2;
        }

        if (1 - _platformChance[2] < randomFloat && (randomFloat <= (1 - _platformChance[3]) || randomFloat <= 1.0f))
        {
            return 3;
        }

        return -1;
        
    }

    public int CheckPropabilitySpecial()
    {
        if(_manager.OverallSpawnAmount > _specialPlatformUnlock)
        {
            float randomFloat = ReturnCurrentChanceSpecial();

            if(randomFloat >= (1 - _specialPlatformChance) && randomFloat <= 1)
            {
                int randomPlatform = ReturnCurrentSpecialPlatform();
                return randomPlatform;
            }
        }

        return -1;

    }

    float ReturnCurrentChanceSpecial()
    {
        if(_manager.CurrentSpawnAmount == 0 && !_manager.isSpecialSection)
        {
            float randomFloat = Random.value;
            _currentSpecialPropability = randomFloat;
            return _currentSpecialPropability;
        }
        return _currentSpecialPropability;
    }

    int ReturnCurrentSpecialPlatform()
    {
        if(!_manager.isSpecialSection)
        {
            int randomPlatform = Random.Range(0, 2);
            _currentSpecialPlatform = randomPlatform;
            return _currentSpecialPlatform;
        }

        return _currentSpecialPlatform;
    }

    float ReturnCurrentChance()
    {
        if(_manager.CurrentSpawnAmount >= _maxAmountToSpawn)
        {
            _manager.CurrentSpawnAmount = 0;
            _maxAmountToSpawn = Random.Range(1, (_poolsComponent.MaxPoolCount / 2) + 1);
            float randomFloat = Random.value;
            _currentPropability = randomFloat;
            return _currentPropability;
        }
        
        return _currentPropability;

    }

}
