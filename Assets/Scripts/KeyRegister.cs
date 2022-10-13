using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRegister : MonoBehaviour
{
    public delegate void OnKeyUpdateAction(int i);
    public static OnKeyUpdateAction OnKeyUpdate;

    int _keysCollected = 0;

    private void Start()
    {
        Key.OnKeyCollected += KeyCollected;
    }


    void KeyCollected()
    {
        _keysCollected++;

        if (OnKeyUpdate != null)
        {
            OnKeyUpdate(_keysCollected);
        }
    }

    private void OnDestroy()
    {
        Key.OnKeyCollected -= KeyCollected;
    }
}
