using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPoolsComponent : MonoBehaviour
{
    [Header("Platform Prefabs Section")]
    [SerializeField]
    private GameObject _normalPlatformPrefab;
    [SerializeField]
    private GameObject _longPlatformPrefab;
    [SerializeField]
    private GameObject _doublePlatformPrefab;
    [SerializeField]
    private GameObject _speedPlatformPrefab;
    [SerializeField]
    private GameObject _gravityPlatform;

    [Header("Platform Pools Section")]
    public List<GameObject> normalPlatformsPool;
    public List<GameObject> longPlatformsPool;
    public List<GameObject> doublePlatformsPool;
    public List<GameObject> speedPlatformsPool;
    public List<GameObject> gravityPlatformsPool;
    [SerializeField]
    private int _maxAmountToPool;
    public int MaxPoolCount { get { return _maxAmountToPool; }}
    private PlatformPropabilityCounter _propabilityComponent;

    private void Start()
    {
        _propabilityComponent = GetComponent<PlatformPropabilityCounter>();
    }

    public void SetupPools()
    {
        normalPlatformsPool = new List<GameObject>();
        longPlatformsPool = new List<GameObject>();
        doublePlatformsPool = new List<GameObject>();
        speedPlatformsPool = new List<GameObject>();

        SetObjectPool(_normalPlatformPrefab, normalPlatformsPool);
        SetObjectPool(_longPlatformPrefab, longPlatformsPool);
        SetObjectPool(_doublePlatformPrefab, doublePlatformsPool);
        SetObjectPool(_speedPlatformPrefab, speedPlatformsPool);
        SetObjectPoolSpecial(_gravityPlatform, gravityPlatformsPool);

    }

    private void SetObjectPool(GameObject objectPrefab, List<GameObject> objectPool)
    {
        GameObject tempObject;

        for (int i = 0; i < _maxAmountToPool; i++)
        {
            tempObject = Instantiate(objectPrefab);
            tempObject.SetActive(false);
            objectPool.Add(tempObject);
        }
    }

    private void SetObjectPoolSpecial(GameObject objectPrefab, List<GameObject> objectPool)
    {
        GameObject tempObject;

        for(int i = 0; i < 2; i++)
        {
            tempObject = Instantiate(objectPrefab);
            tempObject.SetActive(false);
            objectPool.Add(tempObject);
        }
    }

    public GameObject GetPooledObject()
    {
        var winningPool = GetWinnerPool();

        for (int i = 0; i <= _maxAmountToPool; i++)
        {
            if (!winningPool[i].activeInHierarchy)
            {
                return winningPool[i];
            }
        }
        return null;
    }

    public GameObject GetPooledObjectSpecial()
    {
        var winningPool = GetWinnerPoolSpecial();

        if(winningPool != null)
        {
            for(int i = 0; i < 2; i++)
            {
                if (!winningPool[i].activeInHierarchy)
                {
                    return winningPool[i];
                }
            }
        }

        return null;
    }

    private List<GameObject> GetWinnerPool()
    {
        switch (_propabilityComponent.CheckPropability())
        {
            case 0: return normalPlatformsPool;
            case 1: return longPlatformsPool;
            case 2: return doublePlatformsPool;
            case 3: return speedPlatformsPool;
            default: return null;
        }
    }

    private List<GameObject> GetWinnerPoolSpecial()
    {
        switch(_propabilityComponent.CheckPropabilitySpecial())
        {
            case 0: return gravityPlatformsPool;
            default: return null;
        }
    }

    public void ResetAllPooledPlatforms()
    {
        for (int i = 0; i < _maxAmountToPool; i++)
        {
            if (normalPlatformsPool[i].activeInHierarchy)
                normalPlatformsPool[i].SetActive(false);

            if (doublePlatformsPool[i].activeInHierarchy)
                doublePlatformsPool[i].SetActive(false);

            if (speedPlatformsPool[i].activeInHierarchy)
                speedPlatformsPool[i].SetActive(false);

            if (longPlatformsPool[i].activeInHierarchy)
                longPlatformsPool[i].SetActive(false);
        }

        for(int i = 0; i < 2; i++)
        {
            if(gravityPlatformsPool[i].activeInHierarchy)
                gravityPlatformsPool[i].SetActive(false);
        }
    }

}
