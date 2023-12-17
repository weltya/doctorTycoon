using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Managers.BuilderManagers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Camera _sceneCamera;

        private Vector3 _position = Vector3.zero;

        public event Action OnClicked, OnExit;

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnClicked?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnExit?.Invoke();
            }
        }

        /*settings go UI>BuildPanel : Raycast target off*/
        public bool IsPointerOverUi()
            => EventSystem.current.IsPointerOverGameObject();

        public bool IsPointerOverUi2()
        {
            return false;
        }
            
        public Vector3 GetSelectedMapToWorld()
        {
            Vector3 mousePos = Input.mousePosition;
            //restrict to object visible for the camera
            mousePos.z = _sceneCamera.nearClipPlane;
            Ray ray = _sceneCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, _layerMask))
            {
                _position = hit.point;
            }
            return _position;
        }
    }
}

