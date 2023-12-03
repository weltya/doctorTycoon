using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _mouseIndicator;
        [SerializeField] InputManager _inputManager;
        [SerializeField] private Grid _grid;
        [SerializeField] private ObjectsDatabaseSO _database;
        private int _selectedObjectIndex = -1;
        [SerializeField] private GameObject _gridVisualization;
        private GridData room1;
        [SerializeField] private PreviewSystem _previewSystem;
        private Vector3Int _lastDetectedPosition = Vector3Int.zero;
        [SerializeField] private ObjectPlacer _objectPlacer;

        private void Start()
        {
            StopPlacement();
            room1 = new GridData();
        }

        public void StartPlacement(int ID)
        {
            StopPlacement();
            _selectedObjectIndex = _database.objectsData.FindIndex(data => data.ID == ID);
            if(_selectedObjectIndex < 0 )
            {
                Debug.LogError($"No ID found {ID}");
                return;
            }
            _gridVisualization.SetActive( true );
            _previewSystem.StartShowingPlacementPreview(_database.objectsData[_selectedObjectIndex].prefab, _database.objectsData[_selectedObjectIndex].Size);
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

            bool isValidPlacement = CheckPlacementValidity(gridPosition, _selectedObjectIndex);
            if (!isValidPlacement)
            {
                return;
            }
            int index = _objectPlacer.PlaceObject(_database.objectsData[_selectedObjectIndex].prefab, _grid.CellToWorld(gridPosition));
            

            GridData selectedData = room1;
            selectedData.AddObjectAt(gridPosition, _database.objectsData[_selectedObjectIndex].Size, _database.objectsData[_selectedObjectIndex].ID, index);

            _previewSystem.UpdatePosition(_grid.CellToWorld(gridPosition), false);
        }

        private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
        {
            GridData selectedData = room1;

            return selectedData.CanPlaceObjectAt(gridPosition, _database.objectsData[selectedObjectIndex].Size);
        }

        private void StopPlacement()
        {
            _selectedObjectIndex = -1;
            _gridVisualization.SetActive(false);
            _previewSystem.StopShowingPreview();
            _inputManager.OnClicked -= PlaceStructure;
            _inputManager.OnExit -= StopPlacement;
            _lastDetectedPosition = Vector3Int.zero;
        }

        private void Update()
        {
            if (_selectedObjectIndex < 0) { return; }
            Vector3 mousePosition = _inputManager.GetSelectedMapToWorld();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
            
            if (_lastDetectedPosition != gridPosition) 
            {
                bool isValidPlacement = CheckPlacementValidity(gridPosition, _selectedObjectIndex);

                _mouseIndicator.transform.position = mousePosition;
                _previewSystem.UpdatePosition(_grid.CellToWorld(gridPosition), isValidPlacement);
                _lastDetectedPosition = gridPosition;
            }  

            
        }
    }
}

