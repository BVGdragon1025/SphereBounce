using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [SerializeField, Tooltip("Amount of force impulse the sphere gets on tap or click")]
    private float _downForce;
    public bool isInAir;
    public bool touchedAntiGravity;
    public bool touchedStickyPlatform;

    public static SphereController Instance { get; set; }
    [HideInInspector]
    public Rigidbody sphereRb;
    private bool _isDead;
    public bool IsPlayerDead { get { return _isDead; } }
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

        _isDead = false;
        touchedAntiGravity = false;
        touchedStickyPlatform = false;
        
    }

    void Start()
    {
        sphereRb = GetComponent<Rigidbody>();
        defaultPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {
            Debug.Log($"Collided with: {collision.collider}, tag: {collision.gameObject.tag}");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if(isInAir && !_isDead && GameManager.Instance.isGameStarted)
            {
                if (touchedAntiGravity)
                    sphereRb.AddForce(Vector3.up * _downForce, ForceMode.Impulse);
                else
                    sphereRb.AddForce(Vector3.down * _downForce, ForceMode.Impulse);
            }
            

        }
    }

    public void MarkAsDead(bool isPlayerDead)
    {
        _isDead = isPlayerDead;
        Debug.Log("Player is dead!");
    }

    public void ResetPlayerPosition()
    {
        transform.position = defaultPosition;
    }

}
