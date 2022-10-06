using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DungeonOrigin : MonoBehaviour
{
    public static S_DungeonOrigin _Inst;

    [SerializeField] RoomData[] _roomArray;

    List<RoomData> _roomDataTop;
    List<RoomData> _roomDataLeft;
    List<RoomData> _roomDataBottom;
    List<RoomData> _roomDataRight;

    private void Awake()
    {
        _Inst = this;

        _roomDataTop = new List<RoomData>();
        _roomDataLeft = new List<RoomData>();
        _roomDataBottom = new List<RoomData>();
        _roomDataRight = new List<RoomData>();

        foreach (RoomData _data in _roomArray)
        {
            if (_data._hasTopOpening) _roomDataTop.Add(_data);
            if (_data._hasRightOpening) _roomDataRight.Add(_data);
            if (_data._hasBottomOpening) _roomDataBottom.Add(_data);
            if (_data._hasLeftOpening) _roomDataLeft.Add(_data);
        }
    }

    #region ReturnRoom
    public RoomData ReturnTopRoom()
    {
        return ReturnRandomRoom(_roomDataTop);
    }

    public RoomData ReturnRightRoom()
    {
        return ReturnRandomRoom(_roomDataRight);
    }
    public RoomData ReturnBottomRoom()
    {
        return ReturnRandomRoom(_roomDataBottom);
    }
    public RoomData ReturnLeftRoom()
    {
        return ReturnRandomRoom(_roomDataLeft);
    }

    RoomData ReturnRandomRoom(List<RoomData> _list)
    {
        return _list[Random.Range(0, _list.Count)];
    }
    #endregion
}
