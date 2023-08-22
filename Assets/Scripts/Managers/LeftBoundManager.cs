using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundManager : MonoBehaviour
{
    [SerializeField]
    private List<string> _allowedTags;

    private void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < _allowedTags.Count; i++)
        {
            if (other.CompareTag(_allowedTags[i]))
            {
                other.gameObject.SetActive(false);
            }
        }

    }
}
