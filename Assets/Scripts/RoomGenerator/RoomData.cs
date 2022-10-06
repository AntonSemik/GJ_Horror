using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create RoomData", fileName = "NewRoomData")]
public class RoomData : ScriptableObject
{
    public GameObject _roomPrefab;

    public bool _hasTopOpening;
    public bool _hasRightOpening;
    public bool _hasBottomOpening;
    public bool _hasLeftOpening;


}
