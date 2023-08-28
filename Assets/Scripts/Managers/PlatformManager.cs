using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformPropabilityCounter))]
[RequireComponent(typeof(PlatformPoolsComponent))]
public class PlatformManager : MonoBehaviour
{
    [Header("Platform Control Section")]
    [SerializeField]
    private int _currentSpawnAmount;
    [SerializeField]
    private int _allSpawnedAmount;
    [SerializeField]
    private float _spaceBetweenPlatforms;
    [SerializeField]
    private float _platformSpeed;
    [SerializeField]
    private List<string> _platformTags;

    public float PlatformSpeed { get { return _platformSpeed; } set { _platformSpeed = value; } }
    public float GetSpaceBetweenPlatforms { get { return _spaceBetweenPlatforms; }}
    public int CurrentSpawnAmount { get { return _currentSpawnAmount; } set { _currentSpawnAmount = value; } }
    public int OverallSpawnAmount { get { return _allSpawnedAmount; } }
    public static PlatformManager Instance;

    //Other variables
    private PlatformPoolsComponent _poolsComponent;
    private float _defaultSpeed;
    public float DefaultSpeed { get { return _defaultSpeed;} }
    public float CurrentPlatformSpeed { get { return _platformSpeed; } }

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

        _poolsComponent = GetComponent<PlatformPoolsComponent>();

    }

    void Start()
    {
        _defaultSpeed = _platformSpeed;
        _poolsComponent.SetupPools();

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.tag}");
        for(int i = 0; i < _platformTags.Count; i++)
        {
            if (other.CompareTag(_platformTags[i]))
            {
                GameObject platform = _poolsComponent.GetPooledObject();

                if (platform != null)
                {
                    Debug.Log("Platform is not null");
                    SpawnPlatform(platform, other.gameObject);
                    _currentSpawnAmount++;
                    _allSpawnedAmount++;
                }
            }
        }
        
    }


    private void SpawnPlatform(GameObject platform, GameObject otherPlatform)
    {

        Vector3 otherObjectPos = otherPlatform.transform.position;
        Debug.Log($"{otherObjectPos}");

        switch (otherPlatform.tag)
        {
            case "DoublePlatform":
                SetSpawnPosition(platform, otherObjectPos, 1.8f);
                break;
            case "Platform":
                SetSpawnPosition(platform, otherObjectPos);
                break;
            case "HighJumpPlatform":
                SetSpawnPosition(platform, otherObjectPos, 1.5f);
                break;
            case "SpawnNext":
                SetSpawnPosition(platform, otherObjectPos, 2.2f);
                break;
            default:
                Debug.Log("Non-platform object has exited the collider.");
                break;
        }

        platform.SetActive(true);

        Debug.Log($"{otherObjectPos}");
    }

    private void SetSpawnPosition(GameObject platform, Vector3 spawnPosition)
    {
        platform.transform.position = new Vector3(spawnPosition.x + _spaceBetweenPlatforms, spawnPosition.y, spawnPosition.z);
    }

    private void SetSpawnPosition(GameObject platform, Vector3 spawnPosition, float distanceMod)
    {
        platform.transform.position = new Vector3(spawnPosition.x, spawnPosition.y + (_spaceBetweenPlatforms * distanceMod) , spawnPosition.z);
    }

    public void ResetAllPooledPlatforms()
    {
        _poolsComponent.ResetAllPooledPlatforms();
    }

}
