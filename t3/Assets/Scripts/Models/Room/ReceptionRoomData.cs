using UnityEngine;
using System;
using Scripts.Utils.Enum;

/**
 * @class ReceptionRoomData
 * @brief Represents a reception room in the game.
 * @extends Room
 */
public class ReceptionRoomData : Room
{
    public ReceptionRoomData Reception;
    public Transform Point;
    public Boolean IsAvailable = true;
    public EnumRoom TypeRoom = EnumRoom.Reception;
}
