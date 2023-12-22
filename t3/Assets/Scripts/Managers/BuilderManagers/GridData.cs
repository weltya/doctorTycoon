using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts.Managers.BuilderManagers
{
    /**
     * @class GridData
     * @brief Manages grid data for object placement in a builder system.
     */
    public class GridData
    {
        /**
         * @brief Rotates a vector based on a given rotation quaternion.
         * @param vector The vector to be rotated.
         * @param rotation The rotation quaternion.
         * @return The rotated vector.
         */
        private Vector3Int RotateVector(Vector3Int vector, Quaternion rotation)
        {
            Vector3 rotatedVector = rotation * vector;
            return new Vector3Int(Mathf.RoundToInt(rotatedVector.x), Mathf.RoundToInt(rotatedVector.y), Mathf.RoundToInt(rotatedVector.z));
        }
        Dictionary<Vector3Int, PlacementData> _placeObjects = new();

         /**
         * @brief Adds an object at a specific grid position with the given size, ID, and rotation.
         * @param gridPosition The grid position to add the object to.
         * @param objectSize The size of the object in the grid.
         * @param ID The ID of the object.
         * @param placedObjectIndex The index representing the placed object.
         * @param rotation The rotation of the object.
         */
        public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex,Quaternion rotation) 
        {
             List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize, rotation);
            PlacementData placementData = new PlacementData(positionToOccupy, ID, placedObjectIndex);
            foreach (var position in positionToOccupy)
            {
                if (_placeObjects.ContainsKey(position))
                {
                    //throw new Exception($"Dictionary contains already this cell pos {position}");
                    return;
                }
                _placeObjects[position] = placementData;
            }
        }

        /**
         * @brief Calculates the grid positions that an object will occupy based on its size and rotation.
         * @param gridPosition The grid position of the object.
         * @param objectSize The size of the object in the grid.
         * @param rotation The rotation of the object.
         * @return The list of grid positions that the object will occupy.
         */
        public List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize,Quaternion rotation)
        {
            List<Vector3Int> returnVam = new List<Vector3Int>();
            for (int x = 0; x < objectSize.x; x++)
            {
                for (int y = 0; y < objectSize.y; y++)
                {
                    Vector3Int rotatedOffset = RotateVector(new Vector3Int(x, 0, y), rotation);
                    returnVam.Add(gridPosition + rotatedOffset);
                }
            }
            return returnVam;
        }

         /**
         * @brief Checks if an object can be placed at a specific grid position based on its size and rotation.
         * @param gridPosition The grid position to check for placement.
         * @param objectSize The size of the object in the grid.
         * @param rotation The rotation of the object.
         * @return True if the object can be placed, false otherwise.
         */
        public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize,Quaternion rotation)
        {
            List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize,rotation);
            foreach(var position in positionToOccupy)
            {
                if (_placeObjects.ContainsKey(position))
                    return false;
            }
            return true;
        }

        /**
         * @brief Gets the index representing the object at a specific grid position.
         * @param gridPosition The grid position to get the index for.
         * @return The index representing the placed object.
         */
        internal int GetRepresentationIndex(Vector3Int gridPosition)
        {
            if (!_placeObjects.ContainsKey(gridPosition))
            {
                return -1;
            }
            return _placeObjects[gridPosition].PlacedObjectIndex;
        }
        /**
         * @brief Removes an object from the grid at a specific grid position.
         * @param gridPosition The grid position to remove the object from.
         */

        internal void RemoveObjectAt(Vector3Int gridPosition)
        {
            try
            {
                foreach (var pos in _placeObjects[gridPosition].occupiedPositions)
                {
                    _placeObjects.Remove(pos);
                }
            } catch(Exception e)
            {
                
            }
            
        }
    }

    /**
     * @class PlacementData
     * @brief Data structure to store placement information for an object.
     */
    public class PlacementData
    {
        public List<Vector3Int> occupiedPositions;
        public int ID {  get; private set; }
        public int PlacedObjectIndex { get; private set; }

        /**
         * @brief Constructor for PlacementData.
         * @param occupiedPositions List of grid positions occupied by the object.
         * @param ID The ID of the object.
         * @param placedObjectIndex The index representing the placed object.
         */
        public PlacementData(List<Vector3Int> occupiedPositions, int ID, int placedObjectIndex)
        {
            this.occupiedPositions = occupiedPositions;
            this.ID = ID;
            PlacedObjectIndex = placedObjectIndex;
        }
    }
    

}

