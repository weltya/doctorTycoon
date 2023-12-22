using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Managers.BuilderManagers
{
    /**
     * @class InputManager
     * @brief Handles input events for the building system.
     */
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Camera _sceneCamera;

        private Vector3 _position = Vector3.zero;

        public event Action OnClicked, OnExit, onRotation;

        /**
         * @brief Update is called once per frame and checks for input events.
         */
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnClicked?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnExit?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                onRotation?.Invoke();
            }
        }

        /**
         * @brief Checks if the pointer is over a UI element.
         * @return True if the pointer is over a UI element, false otherwise.
         */
        public bool IsPointerOverUi()
            => EventSystem.current.IsPointerOverGameObject();


        /**
         * @brief Placeholder method for checking if the pointer is over a UI element.
         * @return Always returns false.
         */
        public bool IsPointerOverUi2()
        {
            return false;
        }
        
       /**
         * @brief Gets the world position of the selected map location.
         * @return The world position of the selected map location.
         */
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

