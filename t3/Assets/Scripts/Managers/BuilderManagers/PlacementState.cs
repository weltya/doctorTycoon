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

        public void OnAction(Vector3Int gridPosition)
        {
            bool isValidPlacement = CheckPlacementValidity(gridPosition, _selectedObjectIndex);
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
            selectedData.AddObjectAt(gridPosition, database.objectsData[_selectedObjectIndex].Size, database.objectsData[_selectedObjectIndex].ID, index);

            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
        }

        private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
        {
            GridData selectedData = room1;

            return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
        }

        public void UpdateState(Vector3Int gridPosition)
        {
            bool isValidPlacement = CheckPlacementValidity(gridPosition, _selectedObjectIndex);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), isValidPlacement);
        }
    }
}

