using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformManager))]
public class PlatformManager : MonoBehaviour
{
    [Header("Platform Prefabs Section")]
    [SerializeField]
    private GameObject _normalPlatformPrefab;
    [SerializeField]
    private GameObject _longPlatformPrefab;
    [SerializeField]
    private GameObject _doublePlatformPrefab;

    [Header("Platform Control Section")]
    [SerializeField]
    private int _maxAmountToPool;
    [SerializeField]
    private float _spaceBetweenPlatforms;
    [SerializeField]
    private float _platformSpeed;

    public float GetPlatformSpeed { get { return _platformSpeed; } }
    public float SetPlatformSpeed { set { _platformSpeed = value; } }
    public float GetSpaceBetweenPlatforms { get { return _spaceBetweenPlatforms; }}
    public static PlatformManager Instance;

    [Header("Platform Pools Section")]
    public List<GameObject> normalPlatformsPool;
    public List<GameObject> longPlatformsPool;
    public List<GameObject> doublePlatformsPool;

    //Other variables
    private PlatformPropabilityCounter _propabilityComponent;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        _propabilityComponent = GetComponent<PlatformPropabilityCounter>();

    }

    void Start()
    {
        normalPlatformsPool = new List<GameObject>();
        longPlatformsPool = new List<GameObject>();
        doublePlatformsPool = new List<GameObject>();

        SetObjectPool(_normalPlatformPrefab, normalPlatformsPool);
        SetObjectPool(_longPlatformPrefab, longPlatformsPool);
        SetObjectPool(_doublePlatformPrefab, doublePlatformsPool);

    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i <= _maxAmountToPool; i++)
        {
            if (!GetWinnerPool()[i].activeInHierarchy)
            {
                return GetWinnerPool()[i];
            }
        }
        return null;
    }

    private void SetObjectPool(GameObject objectPrefab, List<GameObject> objectPool)
    {
        GameObject tempObject;

        for(int i = 0; i < _maxAmountToPool; i++)
        {
            tempObject = Instantiate(objectPrefab);
            tempObject.SetActive(false);
            objectPool.Add(tempObject);
        }
    }

    private List<GameObject> GetWinnerPool()
    {
        switch (_propabilityComponent.CheckPropability())
        {
            case 0:
                return normalPlatformsPool;
            case 1:
                return longPlatformsPool;
            case 2:
                return doublePlatformsPool;
            default:
                return null;
        }
    }

}
