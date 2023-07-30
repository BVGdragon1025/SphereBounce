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

    public float PlatformSpeed { get { return _platformSpeed; } set { _platformSpeed = value; } }
    public float GetSpaceBetweenPlatforms { get { return _spaceBetweenPlatforms; }}
    public int MaxPoolCount { get { return _maxAmountToPool; }}
    public static PlatformManager Instance;

    [Header("Platform Pools Section")]
    public List<GameObject> normalPlatformsPool;
    public List<GameObject> longPlatformsPool;
    public List<GameObject> doublePlatformsPool;

    //Other variables
    private PlatformPropabilityCounter _propabilityComponent;
    private float _defaultSpeed;
    public float DefaultSpeed { get { return _defaultSpeed;} }

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
        _defaultSpeed = _platformSpeed;
        normalPlatformsPool = new List<GameObject>();
        longPlatformsPool = new List<GameObject>();
        doublePlatformsPool = new List<GameObject>();

        SetObjectPool(_normalPlatformPrefab, normalPlatformsPool);
        SetObjectPool(_longPlatformPrefab, longPlatformsPool);
        SetObjectPool(_doublePlatformPrefab, doublePlatformsPool);

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.tag}");

        if (other.CompareTag("Platform") || other.CompareTag("DoublePlatform"))
        {
            GameObject platform = GetPooledObject();

            if(platform != null)
            {
                Debug.Log("Platform is not null");
                SpawnPlatform(platform, other.gameObject);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        var winningPool = GetWinnerPool();

        for(int i = 0; i <= _maxAmountToPool; i++)
        {
            if (!winningPool[i].activeInHierarchy)
            {
                return winningPool[i];
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

    private void SpawnPlatform(GameObject platform, GameObject otherPlatform)
    {

        Vector3 otherObjectPos = otherPlatform.transform.position;
        Debug.Log($"{otherObjectPos}");

        switch (otherPlatform.tag)
        {
            case "DoublePlatform":
                platform.transform.position = new Vector3(otherObjectPos.x + (_spaceBetweenPlatforms * 2), otherObjectPos.y, otherObjectPos.z);
                break;
            case "Platform":
                platform.transform.position = new Vector3(otherObjectPos.x + _spaceBetweenPlatforms, otherObjectPos.y, otherObjectPos.z);
                break;
            default:
                Debug.Log("Non-platform object has exited the collider.");
                break;
        }

        platform.SetActive(true);

        Debug.Log($"{otherObjectPos}");
    }

}
