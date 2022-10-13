using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLight : MonoBehaviour
{
    private void Start()
    {
        Key.OnKeyCollected += DestroySelf;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Key.OnKeyCollected -= DestroySelf;
    }
}
