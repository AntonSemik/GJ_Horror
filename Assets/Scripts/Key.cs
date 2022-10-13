using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public delegate void KeyAction();
    public static KeyAction OnKeyCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && OnKeyCollected != null)
        {
            OnKeyCollected();
            Destroy(gameObject);
        }
    }
}
