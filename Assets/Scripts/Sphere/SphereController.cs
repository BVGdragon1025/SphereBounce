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
    public bool isDead;
    public Vector3 defaultPosition;

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

        isDead = false;
        
    }

    void Start()
    {
        sphereRb = GetComponent<Rigidbody>();
        defaultPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if(isInAir && !isDead && GameManager.Instance.isGameStarted)
                sphereRb.AddForce(Vector3.down * _downForce, ForceMode.Impulse);
        }
    }

    public void MarkAsDead()
    {
        isDead = true;
        Debug.Log("Player is dead!");
    }

    public void ResetPlayerPosition()
    {
        transform.position = defaultPosition;
    }

}
