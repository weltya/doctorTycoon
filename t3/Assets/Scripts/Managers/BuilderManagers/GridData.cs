using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts.Managers.BuilderManagers
{
    public class GridData
    {
        Dictionary<Vector3Int, PlacementData> _placeObjects = new();

        public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex) 
        {
            List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
            PlacementData placementData = new PlacementData(positionToOccupy, ID, placedObjectIndex);
            foreach(var position in positionToOccupy)
            {
                if(_placeObjects.ContainsKey(position))
                {
                    throw new Exception($"Dictionary contains already this cell pos {position}");
                }
                _placeObjects[position] = placementData;
            }
        }

        private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> returnVam = new();
            for(int x = 0; x < objectSize.x; x++)
            {
                for(int y = 0; y < objectSize.y; y++)
                {
                    returnVam.Add(gridPosition + new Vector3Int(x, 0, y));
                }
            }
            return returnVam;
        }

        public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
            foreach(var position in positionToOccupy)
            {
                if (_placeObjects.ContainsKey(position))
                    return false;
            }
            return true;
        }

        internal int GetRepresentationIndex(Vector3Int gridPosition)
        {
            if (!_placeObjects.ContainsKey(gridPosition))
            {
                return -1;
            }
            return _placeObjects[gridPosition].PlacedObjectIndex;
        }

        internal void RemoveObjectAt(Vector3Int gridPosition)
        {
            foreach (var pos in _placeObjects[gridPosition].occupiedPositions)
            {
                _placeObjects.Remove(pos);
            }
        }
    }

    public class PlacementData
    {
        public List<Vector3Int> occupiedPositions;
        public int ID {  get; private set; }
        public int PlacedObjectIndex { get; private set; }

        public PlacementData(List<Vector3Int> occupiedPositions, int ID, int placedObjectIndex)
        {
            this.occupiedPositions = occupiedPositions;
            this.ID = ID;
            PlacedObjectIndex = placedObjectIndex;
        }
    }
}

