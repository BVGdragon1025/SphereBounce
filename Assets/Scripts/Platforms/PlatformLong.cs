using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLong : MonoBehaviour
{
    private LeftBoundManager _leftBound;
    [SerializeField]
    private GameObject[] _children;

    // Start is called before the first frame update
    void Start()
    {
        _leftBound = GameObject.FindGameObjectWithTag("LeftBound").GetComponent<LeftBoundManager>();
        _children = new GameObject[transform.childCount];

        for(int i = 0; i < _children.Length; i++)
        {
            _children[i] = transform.GetChild(i).gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!SphereController.Instance.IsPlayerDead && GameManager.Instance.isGameStarted)
            transform.Translate(PlatformManager.Instance.PlatformSpeed * Time.deltaTime * Vector3.left);

        DisablePlatforms();
    }

    private void OnEnable()
    {
        ChangeChildPatformState(true);
    }

    void DisablePlatforms()
    {
        if (transform.position.x < (_leftBound.transform.position.x * 1.5))
        {
            gameObject.SetActive(false);
            ChangeChildPatformState(false);
        }
    }

    void ChangeChildPatformState(bool shouldBeActive)
    {
        for(int i = 0; i < _children.Length; i++)
        {
            _children[i].SetActive(shouldBeActive);
        }
    }



}
