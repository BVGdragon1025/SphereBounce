using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Normal Platform Section")]
    [SerializeField]
    private GameObject _normalPlatformPrefab;
    [SerializeField]
    private int _amountToPool;
    [SerializeField]
    private float _spaceBetweenPlatforms;

    public float SpaceBetweenPlatforms { get { return _spaceBetweenPlatforms; }}
    public static PlatformManager Instance;
    public List<GameObject> normalPlatformsPool;

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
        
    }

    // Start is called before the first frame update
    void Start()
    {
        normalPlatformsPool = new List<GameObject>();
        GameObject temp;

        for(int i = 0; i <= _amountToPool; i++)
        {
            temp = Instantiate(_normalPlatformPrefab);
            temp.SetActive(false);
            normalPlatformsPool.Add(temp);

        }

    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i <= _amountToPool; i++)
        {
            if (!normalPlatformsPool[i].activeInHierarchy)
            {
                return normalPlatformsPool[i];
            }
        }
        return null;
    }

}
