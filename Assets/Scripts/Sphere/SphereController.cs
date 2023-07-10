using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [SerializeField, Tooltip("Amount of force impulse the sphere gets on tap or click")]
    private float _downForce;
    public bool isInAir;

    public static SphereController Instance { get; set; }
    [HideInInspector]
    public Rigidbody sphereRb;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        sphereRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInAir)
        {
            sphereRb.AddForce(Vector3.down * _downForce, ForceMode.Impulse);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            isInAir = true;
    }

}
