using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField] InputManager _inputManager;
        [SerializeField] private Grid _grid;
        [SerializeField] private ObjectsDatabaseSO _database;
        [SerializeField] private GameObject _gridVisualization;
        private GridData room1;
        [SerializeField] private PreviewSystem _previewSystem;
        private Vector3Int _lastDetectedPosition = Vector3Int.zero;
        [SerializeField] private ObjectPlacer _objectPlacer;
        IBuildingState buildingState;

        private void Start()
        {
            StopPlacement();
            room1 = new GridData();
        }

        public void StartPlacement(int ID)
        {
            StopPlacement();
            _gridVisualization.SetActive(true);
            buildingState = new PlacementState(ID, _grid, _previewSystem, _database, room1, _objectPlacer);
            _inputManager.OnClicked += PlaceStructure;
            _inputManager.OnExit += StopPlacement;
        }

        public void StartRemoving()
        {
            StopPlacement();
            _gridVisualization.SetActive(true);
            buildingState = new RemovingState(_grid, _previewSystem, room1, _objectPlacer);
            _inputManager.OnClicked += PlaceStructure;
            _inputManager.OnExit += StopPlacement;
        }

        private void PlaceStructure()
        {
            if (_inputManager.IsPointerOverUi())
            {
                return;
            }
            Vector3 mousePosition = _inputManager.GetSelectedMapToWorld();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
            buildingState.OnAction(gridPosition);
        }

        //private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
        //{
        //    GridData selectedData = room1;

        //    return selectedData.CanPlaceObjectAt(gridPosition, _database.objectsData[selectedObjectIndex].Size);
        //}

        private void StopPlacement()
        {
            if (buildingState == null) { return; }
            _gridVisualization.SetActive(false);
            buildingState.EndState();
            _inputManager.OnClicked -= PlaceStructure;
            _inputManager.OnExit -= StopPlacement;
            _lastDetectedPosition = Vector3Int.zero;
            buildingState = null;
        }

        private void Update()
        {
            if (buildingState == null) { return; }
            Vector3 mousePosition = _inputManager.GetSelectedMapToWorld();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
            
            if (_lastDetectedPosition != gridPosition) 
            {
                buildingState.UpdateState(gridPosition);
                _lastDetectedPosition = gridPosition;
            }  

            
        }
    }
}

