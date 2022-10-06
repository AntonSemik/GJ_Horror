using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] Transform _roomOrigin;
    [SerializeField] Collider2D _collider;

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("DestroyRoomField"))
        {
            _collider.enabled = false;
            Destroy(_roomOrigin.gameObject, 0.1f);
        }
    }
}
