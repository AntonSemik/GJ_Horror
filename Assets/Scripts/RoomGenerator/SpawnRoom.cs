using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    S_DungeonOrigin _dungeonOrigin;

    RoomData _roomToSpawn;

    bool _canCreateRoom = true;

    public enum Direction
    {
        top,
        right,
        bottom,
        left
    }

    public Direction _requireRoomWithOpening;

    private void Start()
    {
        _dungeonOrigin = S_DungeonOrigin._Inst;
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Room"))
        {
            _canCreateRoom = false;
        }

        if (_collision.CompareTag("CreateRoomField"))
        {
            Invoke("CheckRoomCreation", 0.1f);
        }
    }

    void CheckRoomCreation()
    {
        if (_canCreateRoom)
        {
            CreateRoom();
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Room"))
        {
            _canCreateRoom = true;
        }
    }

    GameObject _temp;
    void CreateRoom()
    {
        switch (_requireRoomWithOpening)
        {
            case Direction.top:
                _roomToSpawn = _dungeonOrigin.ReturnTopRoom();
                break;

            case Direction.right:
                _roomToSpawn = _dungeonOrigin.ReturnRightRoom();
                break;

            case Direction.bottom:
                _roomToSpawn = _dungeonOrigin.ReturnBottomRoom();
                break;

            case Direction.left:
                _roomToSpawn = _dungeonOrigin.ReturnLeftRoom();
                break;
        }

        _temp = Instantiate(_roomToSpawn._roomPrefab, transform.position, Quaternion.identity);
        _temp.transform.parent = _dungeonOrigin.transform;

        _canCreateRoom = false;
    }
}
