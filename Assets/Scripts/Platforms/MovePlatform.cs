using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private float _platformSpeed;

    // Update is called once per frame
    void Update()
    {
        if (!SphereController.Instance.isDead)
            transform.Translate(_platformSpeed * Time.deltaTime * Vector3.left);
    }
}
