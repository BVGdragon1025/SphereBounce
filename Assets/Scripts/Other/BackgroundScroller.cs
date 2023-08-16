using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    private RawImage _bgImage;
    [SerializeField]
    public float scrollSpeed;
    public float startingSpeed;

    private void Start()
    {
        startingSpeed = scrollSpeed;
    }

    void Update()
    {
        if(GameManager.Instance.isGameStarted)
            _bgImage.uvRect = new Rect(_bgImage.uvRect.position + new Vector2(scrollSpeed, 0.0f) * Time.deltaTime, _bgImage.uvRect.size);
    }
}
