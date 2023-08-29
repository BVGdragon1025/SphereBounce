using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLong : Platform
{
    [SerializeField, Tooltip("Bounce multiplier for 'good platforms (green ones in debug)'. \n 1 - Default Bounce Force "), Range(1.0f, 2.0f)]
    private float _bounceMultiplier;
    [SerializeField]
    private GameObject[] _children;
    [SerializeField]
    private float[] _childrenBounceForce;
    [SerializeField]
    private Renderer[] _childrenRenderer;

    void OnEnable()
    {
        _children = new GameObject[transform.childCount];
        _childrenBounceForce = new float[transform.childCount];
        _childrenRenderer = new Renderer[transform.childCount];

        for (int i = 0; i < _children.Length; i++)
        {
            _children[i] = transform.GetChild(i).gameObject;

            _childrenRenderer[i] = _children[i].GetComponent<Renderer>();

            if (_children[i].CompareTag("GoodPlatform"))
                _childrenBounceForce[i] = bounceForce * _bounceMultiplier;

            if (_children[i].CompareTag("ComboBreaker"))
                _childrenBounceForce[i] = bounceForce;
        }

    }

    public override void BounceSphere()
    {
        int amountOfCollisions = 0;
        int currentCollision = 0;

        for(int i = 0; i < _children.Length; i++)
        {
            if (_childrenRenderer[i].bounds.Intersects(playerColl.bounds))
            {
                amountOfCollisions++;
                currentCollision = i;
                Debug.Log($"Collider intersecting: {_children[i].tag}, amount: {amountOfCollisions}, current: {currentCollision} ");

            }
        }

        if(amountOfCollisions > 1)
        {
            var correctForce = Mathf.Min(bounceForce, bounceForce * _bounceMultiplier);

            if (SphereController.Instance.touchedAntiGravity)
                playerRb.AddForce(Vector3.down * correctForce, ForceMode.Impulse);
            else
                playerRb.AddForce(Vector3.up * correctForce, ForceMode.Impulse);

            GameManager.Instance.CurrentCombo = 1;
        }
        else
        {
            if (SphereController.Instance.touchedAntiGravity)
                playerRb.AddForce(Vector3.down * _childrenBounceForce[currentCollision], ForceMode.Impulse);
            else
                playerRb.AddForce(Vector3.up * _childrenBounceForce[currentCollision], ForceMode.Impulse);

            if (_children[currentCollision].CompareTag("GoodPlatform"))
                GameManager.Instance.CurrentCombo += 1;

            if (_children[currentCollision].CompareTag("ComboBreaker"))
                GameManager.Instance.CurrentCombo = 1;

        }
    }

}