using Scripts.Managers.BuilderManagers;
using Scripts.Utils.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Managers.BuilderManagers
{
    /**
     * @class PlacementState
     * @implements IBuildingState
     * @brief Represents the state of object placement in the game.
     */
    public class PlacementState : IBuildingState
    {
        private int _selectedObjectIndex = -1;
        int ID;
        Grid grid;
        PreviewSystem previewSystem;
        ObjectsDatabaseSO database;
        GridData room1;
        ObjectPlacer objectPlacer;

        /**
         * @brief Constructor for the PlacementState class.
         * @param {int} iD - The ID of the selected object.
         * @param {Grid} grid - The game grid.
         * @param {PreviewSystem} previewSystem - The preview system for showing object previews.
         * @param {ObjectsDatabaseSO} database - The scriptable object containing object data.
         * @param {GridData} room1 - The grid data for the placement state.
         * @param {ObjectPlacer} objectPlacer - The object placer manager.
         */
        public PlacementState(int iD,
                              Grid grid,
                              PreviewSystem previewSystem,
                              ObjectsDatabaseSO database,
                              GridData room1,
                              ObjectPlacer objectPlacer)
        {
            ID = iD;
            this.grid = grid;
            this.previewSystem = previewSystem;
            this.database = database;
            this.room1 = room1;
            this.objectPlacer = objectPlacer;

            _selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
            if (_selectedObjectIndex > -1)
            {

                previewSystem.StartShowingPlacementPreview(database.objectsData[_selectedObjectIndex].prefab, database.objectsData[_selectedObjectIndex].Size);
            }
            else
                throw new System.Exception($"(no room with id : {iD}");


        }


        /**
         * @brief Method called to end the placement state.
         */
        public void EndState()
        {
            previewSystem.StopShowingPreview();
        }

       /**
         * @brief Method called when a building action is performed.
         * @param {Vector3Int} gridPosition - The grid position of the action.
         * @param {Quaternion} rotation - The rotation of the building.
         */
        public void OnAction(Vector3Int gridPosition,Quaternion rotation)
        {
            bool isValidPlacement = CheckPlacementValidity(gridPosition, _selectedObjectIndex,rotation);
            if (!isValidPlacement)
            {
                return;
            }
            int index = objectPlacer.PlaceObject(database.objectsData[_selectedObjectIndex].prefab, grid.CellToWorld(gridPosition), _selectedObjectIndex);
            if (index < 0)
            {
                EndState();
            }
            GridData selectedData = room1;
            selectedData.AddObjectAt(gridPosition, database.objectsData[_selectedObjectIndex].Size, database.objectsData[_selectedObjectIndex].ID, index,rotation);

            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
        }

        /**
         * @brief Checks if the placement at the specified grid position is valid.
         * @param {Vector3Int} gridPosition - The grid position to check for placement.
         * @param {int} selectedObjectIndex - The index of the selected object in the database.
         * @param {Quaternion} rotation - The rotation of the object.
         * @return {bool} - True if the placement is valid, false otherwise.
         */
        private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex,Quaternion rotation)
        {
            GridData selectedData = room1;

            return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size,rotation);
        }

        /**
         * @brief Method called to update the placement state.
         * @param {Vector3Int} gridPosition - The grid position for the update.
         * @param {Quaternion} rotation - The rotation of the object.
         */
        public void UpdateState(Vector3Int gridPosition,Quaternion rotation)
        {
            bool isValidPlacement = CheckPlacementValidity(gridPosition, _selectedObjectIndex, rotation);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), isValidPlacement);
        }

        /**
         * @brief Method called to rotate the placement preview.
         */
        public void RotatePreview() {
            if (_selectedObjectIndex > -1)
            {
                var previewObject = database.objectsData[_selectedObjectIndex].prefab;

                // Rotate the preview object visually
                previewObject.transform.Rotate(0, 90, 0);
                previewSystem.UpdatePreviewRotation(previewObject.transform.rotation);

                // Update the underlying GridData with the new rotated positions
                Vector3Int gridPosition = grid.WorldToCell(previewObject.transform.position);
                Vector2Int objectSize = database.objectsData[_selectedObjectIndex].Size;

                // Remove the old positions from the GridData
                room1.RemoveObjectAt(gridPosition);

                // Calculate the new rotated positions
                Quaternion rotation = previewObject.transform.rotation;
                List<Vector3Int> rotatedPositions = room1.CalculatePositions(gridPosition, objectSize, rotation);

                // Add the new rotated positions to the GridData with the correct index
                foreach (var position in rotatedPositions)
                {
                    room1.AddObjectAt(position, objectSize, database.objectsData[_selectedObjectIndex].ID, _selectedObjectIndex, rotation);
                }
            }
        }
    }
}

