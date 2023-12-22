using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Scripts.UII;

namespace Scripts.Managers.BuilderManagers
{
    public class PlacementSystem : MonoBehaviour
    {
        private int _selectedObjectIndex = -1;
        [SerializeField] InputManager _inputManager;
        [SerializeField] private Grid _grid;
        [SerializeField] private ObjectsDatabaseSO _database;
        [SerializeField] private GameObject _gridVisualization;
        private GridData room1;
        [SerializeField] private PreviewSystem _previewSystem;
        private Vector3Int _lastDetectedPosition = Vector3Int.zero;
        [SerializeField] private ObjectPlacer _objectPlacer;
        [SerializeField] private UI _ui;
        IBuildingState buildingState;
        [SerializeField] private UIScoreManager score;

        private void Start()
        {
           
            StopPlacement();
            room1 = new GridData();
            _inputManager.onRotation += Rotation;
        }

        public void Rotation() {
            buildingState?.RotatePreview();
        }

        public void StartPlacement(int ID)
        {
            StopPlacement();
            _ui.NoDisplayAllBuildPanel();
            _gridVisualization.SetActive(true);

            _selectedObjectIndex = _database.objectsData.FindIndex(data => data.ID == ID);

            if (_selectedObjectIndex > -1)
            {
                var previewObject = _database.objectsData[_selectedObjectIndex].prefab;
                Quaternion rotation = previewObject.transform.rotation;
                buildingState = new PlacementState(ID, _grid, _previewSystem, _database, room1, _objectPlacer);
                _inputManager.OnClicked += PlaceStructure;
                _inputManager.OnExit += StopPlacement;
            }
            else
            {
                throw new System.Exception($"(no room with id : {ID}");
            }
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

            if (_selectedObjectIndex > -1)
            {
                var previewObject = _database.objectsData[_selectedObjectIndex].prefab;
                Quaternion rotation = previewObject.transform.rotation;
                buildingState.OnAction(gridPosition, rotation);
            }
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
            //_inputManager.onRotation -= Rotation;
        }

        private void Update()
        {
           if (buildingState == null) { return; }
            Vector3 mousePosition = _inputManager.GetSelectedMapToWorld();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

            if (_selectedObjectIndex > -1)
            {
                var previewObject = _database.objectsData[_selectedObjectIndex].prefab;
                Quaternion rotation = previewObject.transform.rotation;

                if (_lastDetectedPosition != gridPosition)
                {
                    buildingState.UpdateState(gridPosition, rotation);
                    _lastDetectedPosition = gridPosition;
                }
            }
        }
    
    }
}

