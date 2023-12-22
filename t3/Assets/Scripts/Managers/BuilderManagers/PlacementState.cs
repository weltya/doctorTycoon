using Scripts.Managers.BuilderManagers;
using Scripts.Utils.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Managers.BuilderManagers
{
    public class PlacementState : IBuildingState
    {
        private int _selectedObjectIndex = -1;
        int ID;
        Grid grid;
        PreviewSystem previewSystem;
        ObjectsDatabaseSO database;
        GridData room1;
        ObjectPlacer objectPlacer;

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

        public void EndState()
        {
            previewSystem.StopShowingPreview();
        }

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

        private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex,Quaternion rotation)
        {
            GridData selectedData = room1;

            return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size,rotation);
        }

        public void UpdateState(Vector3Int gridPosition,Quaternion rotation)
        {
            bool isValidPlacement = CheckPlacementValidity(gridPosition, _selectedObjectIndex, rotation);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), isValidPlacement);
        }

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

