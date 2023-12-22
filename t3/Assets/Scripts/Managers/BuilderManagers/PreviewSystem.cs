using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Scripts.Managers.BuilderManagers
{
    /**
     * @class PreviewSystem
     * @brief Manages the preview of objects and indicators in the game world.
     */
    public class PreviewSystem : MonoBehaviour
    {
        [SerializeField] InputManager _inputManager;
        [SerializeField] private float _previewOffsetY = 0.05f;
        [SerializeField] private GameObject _cellIndicator;
        private GameObject _previewObject;
        [SerializeField] Material _previewMaterialPrefab;
        private Material _previewMaterialInstance;
        private Renderer _cellIndicatorRenderer;


        /**
         * @brief Initializes the PreviewSystem by creating a material instance for previews.
         */
        private void Start()
        {
            _previewMaterialInstance = new Material(_previewMaterialPrefab);
            _cellIndicator.SetActive(false);
            _cellIndicatorRenderer = _cellIndicator.GetComponentInChildren<Renderer>();
            if( _cellIndicatorRenderer == null )
            {
                Debug.Log(_cellIndicator.name);
                Component[] components = _cellIndicator.GetComponents(typeof(Component));
                foreach (Component component in components)
                {
                    Debug.og(component.ToString());
                }
                Debug.LogError("_cellIndicatorRenderer vaut null");
            }
        }


        /**
         * @brief Starts showing the placement preview for a given object.
         * @param {GameObject} prefab - The prefab of the object to be previewed.
         * @param {Vector2Int} size - The size of the object.
         */
        public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
        {
            _previewObject = Instantiate(prefab);
            PreparePreaview(_previewObject);
        }

        /**
        * @brief Prepares the materials for the placement preview.
        * @param {GameObject} previewObject - The object for which to prepare the preview.
        */
        private void PreparePreaview(GameObject previewObject)
        {
            Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                Material[] materials = renderer.materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = _previewMaterialInstance;
                }
                renderer.materials = materials;
            }
        }

        /**
         * @brief Stops showing the placement preview.
         */
        public void StopShowingPreview()
        {
            _cellIndicator.SetActive(false);
            if ( _previewObject != null )
            {
                Destroy(_previewObject);
            }
            
        }

        /**
         * @brief Updates the position and feedback of the placement preview.
         * @param {Vector3} position - The position to update the preview to.
         * @param {bool} validity - The validity status of the placement.
         */
        public void UpdatePosition(Vector3 position, bool validity)
        {
            if( _previewObject != null)
            {
                MovePreview(position);
                ApplyFeedbackToPreview(validity);
            }
            
            MoveCursor(position);
        }

        /**
        * @brief Applies visual feedback to the placement preview based on its validity.
        * @param {bool} validity - The validity status of the placement.
        */
        private void ApplyFeedbackToPreview(bool validity)
        {
            Color color = validity ? Color.green : Color.red;
            color.a = 0.5f;
            _previewMaterialInstance.color = color;
        }

    
        /**
        * @brief Moves the placement preview cursor to the specified position.
        * @param {Vector3} position - The position to move the cursor to.
        */
        private void MoveCursor(Vector3 position)
        {
            _cellIndicator.transform.position = position;
        }

        /**
        * @brief Moves the placement preview object to the specified position.
        * @param {Vector3} position - The position to move the preview object to.
        */
        private void MovePreview(Vector3 position)
        {
            _previewObject.transform.position = new Vector3(position.x, position.y + _previewOffsetY, position.z);
        }

        /**
         * @brief Starts showing the removal preview.
         */
        internal void StartShowingRemovePreview()
        {
            _cellIndicator.SetActive(true);
        }

        /**
         * @brief Updates the rotation of the placement preview.
         * @param {Quaternion} newRotation - The new rotation to apply.
         */
        public void UpdatePreviewRotation(Quaternion newRotation) {
            if (_previewObject != null) {
                _previewObject.transform.rotation = newRotation;
            }
        }
    }

}
