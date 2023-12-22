using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View
{
    /**
     * @class CameraController
     * @brief Controls the movement and zoom of the camera in the game.
     */
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 40f;
        [SerializeField] private float _zoomSpeed = 10f;
        [SerializeField] private float _minZoom = 10f;
        [SerializeField] private float _maxZoom = 30f;

        private float _currentZoom = 10f;


        /**
         * @brief LateUpdate is called once per frame, after other updates.
         * Handles camera movement and zoom based on user input.
         */
        private void LateUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = transform.right * horizontal + transform.forward * vertical;
            movement.y = 0;
            transform.Translate(movement * _moveSpeed * Time.deltaTime, Space.World);

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            _currentZoom -= scroll * _zoomSpeed;
            _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);

            Vector3 newPosition = transform.position + transform.forward * scroll * _zoomSpeed;
            newPosition.y = Mathf.Clamp(newPosition.y, _minZoom, _maxZoom);
            if (newPosition.y < _maxZoom && newPosition.y > _minZoom)
            {
                transform.position = newPosition;
            }
        }
    }
}

