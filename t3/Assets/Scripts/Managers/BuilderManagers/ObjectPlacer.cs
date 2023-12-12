using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts.Managers.BuilderManagers
{
    public class ObjectPlacer : MonoBehaviour
    {
        
       [SerializeField] private List<GameObject> _placedGameObject = new();

        public int PlaceObject(GameObject prefab, Vector3 vector3)
        {
            GameObject go = Instantiate(prefab);
            go.transform.position = vector3;
            _placedGameObject.Add(go);
            return _placedGameObject.Count - 1;
        }

        internal void RemoveObjectAt(int gameObjectIndex)
        {
            if (_placedGameObject.Count <= gameObjectIndex || _placedGameObject[gameObjectIndex] == null) { return;  }
            Destroy(_placedGameObject[gameObjectIndex]);
            _placedGameObject[gameObjectIndex] = null;
        }
    }
}

