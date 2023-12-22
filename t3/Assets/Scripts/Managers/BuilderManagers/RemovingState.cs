using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    public class RemovingState : IBuildingState
    {
        private int _gameObjectIndex = -1;
        Grid grid;
        PreviewSystem previewSystem;
        GridData room1;
        ObjectPlacer objectPlacer;

        public RemovingState(Grid grid, PreviewSystem previewSystem, GridData room1, ObjectPlacer objectPlacer)
        {
            this.grid = grid;
            this.previewSystem = previewSystem;
            this.room1 = room1;
            this.objectPlacer = objectPlacer;

            previewSystem.StartShowingRemovePreview();
        }

        public void EndState()
        {
            previewSystem.StopShowingPreview();
        }

        public void OnAction(Vector3Int gridPosition,Quaternion rotation)
        {
            GridData selectedData = null;
            if (!room1.CanPlaceObjectAt(gridPosition, Vector2Int.one,rotation))
            {
                selectedData = room1;
            }

            if (selectedData == null)
            {
                //rien a supprimer
            } else
            {
                _gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
                if (_gameObjectIndex == -1)
                {
                    return;
                }
                selectedData.RemoveObjectAt(gridPosition);
                objectPlacer.RemoveObjectAt(_gameObjectIndex);
            }
            Vector3 cellPosition = grid.CellToWorld(gridPosition);
            previewSystem.UpdatePosition(cellPosition, CheckIfSelectionIsValid(gridPosition,rotation));
        }

        private bool CheckIfSelectionIsValid(Vector3Int gridPosition,Quaternion rotation)
        {
            return !room1.CanPlaceObjectAt(gridPosition, Vector2Int.one,rotation);
        }

        public void UpdateState(Vector3Int gridPosition,Quaternion rotation)
        {
            bool validity = CheckIfSelectionIsValid(gridPosition,rotation);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
        }

        public void RotatePreview() {}
    }

}
