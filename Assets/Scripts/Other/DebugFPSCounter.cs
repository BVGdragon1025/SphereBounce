using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugFPSCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _debugCounter;

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_ANDROID && UNITY_EDITOR
    _debugCounter.gameObject.SetActive(true);
        
#endif
    }

    // Update is called once per frame
    void Update()
    {
        _debugCounter.text = $"FPS: {Mathf.Round(1f/ Time.deltaTime)}";
    }
}
