using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _mouseIndicator, _cellIndicator;
        [SerializeField] InputManager _inputManager;
        [SerializeField] private Grid _grid;
        [SerializeField] private ObjectsDatabaseSO _database;
        private int _selectedObjectIndex = -1;
        [SerializeField] private GameObject _gridVisualization;
        private GridData room1;
        private Renderer previewRenderer;
        private List<GameObject> _placedGameObject = new();

        private void Start()
        {
            StopPlacement();
            room1 = new GridData();
            previewRenderer = _cellIndicator.GetComponent<Renderer>();
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
            _cellIndicator.SetActive( true );
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
            GameObject go = Instantiate(_database.objectsData[_selectedObjectIndex].prefab);
            go.transform.position = _grid.CellToWorld(gridPosition);
            _placedGameObject.Add(go);

            GridData selectedData = room1;
            selectedData.AddObjectAt(gridPosition, _database.objectsData[_selectedObjectIndex].Size, _database.objectsData[_selectedObjectIndex].ID, _placedGameObject.Count - 1);
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
            _cellIndicator.SetActive(false);
            _inputManager.OnClicked -= PlaceStructure;
            _inputManager.OnExit -= StopPlacement;
        }

        private void Update()
        {
            if (_selectedObjectIndex < 0) { return; }
            Vector3 mousePosition = _inputManager.GetSelectedMapToWorld();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

            bool isValidPlacement = CheckPlacementValidity(gridPosition, _selectedObjectIndex);
            previewRenderer.material.color = isValidPlacement ? Color.white : Color.red;

            _mouseIndicator.transform.position = mousePosition;
            _cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
        }
    }
}

