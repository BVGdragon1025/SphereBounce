using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxChild : MonoBehaviour
{
    [SerializeField, Tooltip("Multiplier of default scroling speed. 0 = No movement, 1 = Default scrolling speed")]
    private float _scrollSpeedMultiplier;
    [SerializeField, Tooltip("Should the sprite move left. True = Sprite moves to the left, False = Sprite moves to the right")]
    private bool _shouldScrollLeft;

    private float _singleTextureWidth;
    [SerializeField]
    private float _defaultPositionX;
    private float _scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _scrollSpeed = GetComponentInParent<ParallaxParent>().defaultParallaxSpeed;

        if (_shouldScrollLeft)
            _scrollSpeedMultiplier = -_scrollSpeedMultiplier;

        _defaultPositionX = transform.localPosition.x;
            
        SetupTexture();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {
            _scrollSpeed = GetComponentInParent<ParallaxParent>().defaultParallaxSpeed;
            ScrollTexture();
            ResetTexture();
        }
        
    }

    private void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        _singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;

    }

    void ScrollTexture()
    {
        float speedMultiplied = _scrollSpeed * _scrollSpeedMultiplier;

        float delta = speedMultiplied * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    void ResetTexture()
    {
        if((Mathf.Abs(transform.position.x) - _singleTextureWidth) > 0)
            transform.position = new Vector3(_defaultPositionX, transform.position.y, transform.position.z);

    }

}
