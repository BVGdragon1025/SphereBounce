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
    private float _platformSpeedMod;
    [SerializeField]
    private List<string> _platformTags;

    [Header("Platform modifiers")]
    public bool isSpeedActive;
    [HideInInspector]
    public bool isSpecialSection;
    private Vector3 _defaultGravity;
    public Vector3 DefaultGravity { get { return _defaultGravity; } }
    [SerializeField]
    private float _speedIncrease;

    public float PlatformSpeed { get { return _platformSpeed; } set { _platformSpeed = value; } }
    public float PlatformSpeedMod { get { return _platformSpeedMod; } }
    public int CurrentSpawnAmount { get { return _currentSpawnAmount; } set { _currentSpawnAmount = value; } }
    public int OverallSpawnAmount { get { return _allSpawnedAmount; } }
    public int SetOverallSpawnAmount { set {  _allSpawnedAmount = value; } }
    public static PlatformManager Instance;

    //Other variables
    private PlatformPoolsComponent _poolsComponent;
    private PlatformPropabilityCounter _propabilityCounter;
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

        _poolsComponent = GetComponent<PlatformPoolsComponent>();
        _propabilityCounter = GetComponent<PlatformPropabilityCounter>();
        _defaultGravity = Physics.gravity;
        isSpecialSection = false;
        isSpeedActive = false;

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
                GameObject specialPlatform = _poolsComponent.GetPooledObjectSpecial();

                if (platform != null)
                {
                    Debug.Log("Platform is not null");
                    if (specialPlatform != null && (!isSpecialSection || (isSpecialSection && _currentSpawnAmount == (_propabilityCounter.MaxAmountToSpawn - 1))))
                    {
                        Debug.Log("Special platform is spawned!");
                        SpawnPlatform(specialPlatform, other.gameObject);
                        isSpecialSection = true;
                    }
                    else
                    {
                        SpawnPlatform(platform, other.gameObject);
                    }
                    _currentSpawnAmount++;
                    _allSpawnedAmount++;

                    IncreasePlatformSpeed(20, _speedIncrease);

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
            case "HighJumpPlatform":
                SetSpawnPosition(platform, otherObjectPos, 1.5f);
                break;
            case "SpawnNext":
                SetSpawnPosition(platform, otherObjectPos, 2.2f);
                break;
            case "GravityPlatform":
                if (GameManager.Instance.isGravityReversed)
                {
                    platform.transform.SetPositionAndRotation(new Vector3(otherObjectPos.x + _spaceBetweenPlatforms, -otherObjectPos.y * 2, otherObjectPos.z), Quaternion.Euler(270.0f, 0, 0));
                }
                else
                {
                    platform.transform.SetPositionAndRotation(new Vector3(otherObjectPos.x + _spaceBetweenPlatforms, -otherObjectPos.y / 2, otherObjectPos.z), Quaternion.Euler(90.0f, 0, 0));
                    isSpecialSection = false;
                }
                break;
            case "Untagged":
                Debug.Log("Non-platform object has exited the collider.");
                break;
            default:
                SetSpawnPosition(platform, otherObjectPos);
                break;
        }

        platform.SetActive(true);

        Debug.Log($"{otherObjectPos}");
    }

    private void IncreasePlatformSpeed(int platformCount, float modifier)
    {
        _platformSpeedMod = _platformSpeed;
        if (_allSpawnedAmount % platformCount == 0)
        {
            _platformSpeed += modifier;
            _platformSpeedMod = _platformSpeed;
        }
    }

    private void SetSpawnPosition(GameObject platform, Vector3 spawnPosition)
    {
        if (GameManager.Instance.isGravityReversed)
        {
            platform.transform.SetPositionAndRotation(new Vector3(spawnPosition.x + _spaceBetweenPlatforms, spawnPosition.y, spawnPosition.z), Quaternion.Euler(270.0f, 0, 0));
        }
        else
        {
            platform.transform.SetPositionAndRotation(new Vector3(spawnPosition.x + _spaceBetweenPlatforms, spawnPosition.y, spawnPosition.z), Quaternion.Euler(90.0f, 0f, 0f));

        }
        
    }

    private void SetSpawnPosition(GameObject platform, Vector3 spawnPosition, float distanceMod)
    {
        if (GameManager.Instance.isGravityReversed)
        {
            platform.transform.SetPositionAndRotation(new Vector3(spawnPosition.x + (_spaceBetweenPlatforms * distanceMod), spawnPosition.y, spawnPosition.z), Quaternion.Euler(270.0f, 0, 0));
        }
        else
        {
            platform.transform.SetPositionAndRotation(new Vector3(spawnPosition.x + (_spaceBetweenPlatforms * distanceMod), spawnPosition.y, spawnPosition.z), Quaternion.Euler(90.0f, 0, 0));
        }
    }

    public void ResetAllPooledPlatforms()
    {
        _poolsComponent.ResetAllPooledPlatforms();
    }

}
