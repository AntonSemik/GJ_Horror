using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]Transform _player;

    private void LateUpdate()
    {
        transform.position = _player.position;
    }
}
