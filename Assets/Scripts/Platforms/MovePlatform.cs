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
        transform.Translate(_platformSpeed * Time.deltaTime * Vector3.left);
    }
}
