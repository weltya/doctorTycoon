using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Scripts.Managers.BuilderManagers
{
    public class PreviewSystem : MonoBehaviour
    {
        [SerializeField] private float _previewOffsetY = 0.05f;
        [SerializeField] private GameObject _cellIndicator;
        private GameObject _previewObject;
        [SerializeField] Material _previewMaterialPrefab;
        private Material _previewMaterialInstance;
        private Renderer _cellIndicatorRenderer;

        

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
                    Debug.Log(component.ToString());
                }
                Debug.LogError("_cellIndicatorRenderer vaut null");
            }
        }

        public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
        {
            _previewObject = Instantiate(prefab);
            PreparePreaview(_previewObject);
            PrepareCursor(size);
            _cellIndicator.SetActive(true);
        }

        private void PrepareCursor(Vector2Int size)
        {            
            if (size.x > 0 || size.y > 0)
            {
                //offset dans database so size.x et z - 2f (correction du pivot 0.5 -> 1)
                _cellIndicator.transform.localScale = new Vector3(size.x, 1, -size.y);
                _cellIndicator.GetComponentInChildren<Renderer>().material.mainTextureScale = size;
            }
        }

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

        public void StopShowingPreview()
        {
            _cellIndicator.SetActive(false);
            if ( _previewObject != null )
            {
                Destroy(_previewObject);
            }
            
        }

        public void UpdatePosition(Vector3 position, bool validity)
        {
            if( _previewObject != null)
            {
                MovePreview(position);
                ApplyFeedbackToPreview(validity);
            }
            
            MoveCursor(position);
            ApplyFeedbackToCursor(validity);
        }

        private void ApplyFeedbackToPreview(bool validity)
        {
            Color color = validity ? Color.green : Color.red;
            color.a = 0.5f;
            _previewMaterialInstance.color = color;
        }

        private void ApplyFeedbackToCursor(bool validity)
        {
            Color color = validity ? Color.green : Color.red;
            color.a = 0.5f;
            _cellIndicatorRenderer.material.color = color;
        }

        private void MoveCursor(Vector3 position)
        {
            _cellIndicator.transform.position = position;
        }

        private void MovePreview(Vector3 position)
        {
            _previewObject.transform.position = new Vector3(position.x, position.y + _previewOffsetY, position.z);
        }

        internal void StartShowingRemovePreview()
        {
            _cellIndicator.SetActive(true);
            PrepareCursor(Vector2Int.one);
            ApplyFeedbackToCursor(false);
        }
    }

}
