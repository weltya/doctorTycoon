using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    /**
     * @class RemovingState
     * @brief State class for handling the removal of objects in the game.
     */
    public class RemovingState : IBuildingState
    {
        private int _gameObjectIndex = -1;
        Grid grid;
        PreviewSystem previewSystem;
        GridData room1;
        ObjectPlacer objectPlacer;

        /**
         * @brief Constructor for the RemovingState class.
         * @param {Grid} grid - The game grid.
         * @param {PreviewSystem} previewSystem - The preview system for showing object removal previews.
         * @param {GridData} room1 - The grid data for the removal state.
         * @param {ObjectPlacer} objectPlacer - The object placer manager.
         */
        public RemovingState(Grid grid, PreviewSystem previewSystem, GridData room1, ObjectPlacer objectPlacer)
        {
            this.grid = grid;
            this.previewSystem = previewSystem;
            this.room1 = room1;
            this.objectPlacer = objectPlacer;

            previewSystem.StartShowingRemovePreview();
        }

        /**
         * @brief Method called to end the removal state.
         */
        public void EndState()
        {
            previewSystem.StopShowingPreview();
        }

        /**
         * @brief Method called when a removal action is performed.
         * @param {Vector3Int} gridPosition - The grid position of the removal action.
         * @param {Quaternion} rotation - The rotation of the object.
         */
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

        /**
         * @brief Checks if the removal selection is valid.
         * @param {Vector3Int} gridPosition - The grid position to check for removal.
         * @param {Quaternion} rotation - The rotation of the object.
         * @return {bool} - True if the removal is valid, false otherwise.
         */
        private bool CheckIfSelectionIsValid(Vector3Int gridPosition,Quaternion rotation)
        {
            return !room1.CanPlaceObjectAt(gridPosition, Vector2Int.one,rotation);
        }

        /**
         * @brief Method called to update the removal state.
         * @param {Vector3Int} gridPosition - The grid position for the update.
         * @param {Quaternion} rotation - The rotation of the object.
         */
        public void UpdateState(Vector3Int gridPosition,Quaternion rotation)
        {
            bool validity = CheckIfSelectionIsValid(gridPosition,rotation);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
        }
        public void RotatePreview(){
            
        }

    }

}
