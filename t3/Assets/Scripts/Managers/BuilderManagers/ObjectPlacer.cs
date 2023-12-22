using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using  Scripts.UII;

using UnityEngine.UIElements;



namespace Scripts.Managers.BuilderManagers
{
    /**
     * @class ObjectPlacer
     * @brief Handles the placement and removal of objects in the game world.
     */
    public class ObjectPlacer : MonoBehaviour
    {
        internal event Action<Room> onObjectPlaced;
        [SerializeField] private List<GameObject> _placedGameObject = new();
        [SerializeField] private UIScoreManager score;
   


        /**
         * @brief Places an object in the game world at a specified position.
         * @param prefab The prefab of the object to be placed.
         * @param vector3 The position where the object should be placed.
         * @param ID The ID of the object.
         * @return The index of the placed object in the _placedGameObject list.
         */
        public int PlaceObject(GameObject prefab, Vector3 vector3, int ID)
        {

            bool can = score.canBuy(ID);
          
           //if batiment ne peut pas etre placer
            if(!can)
           {
                Debug.Log("pas assez d'argent");
            return -1;
           }
            GameObject go = Instantiate(prefab);
            go.transform.position = vector3;
            
            Room room=go.GetComponent<Room>();
        
            onObjectPlaced?.Invoke(room);
            _placedGameObject.Add(go);
            return _placedGameObject.Count - 1;
        }

        /**
         * @brief Removes the object at the specified index from the game world.
         * @param gameObjectIndex The index of the object to be removed.
         */
        internal void RemoveObjectAt(int gameObjectIndex)
        {
            if (_placedGameObject.Count <= gameObjectIndex || _placedGameObject[gameObjectIndex] == null) { return;  }
            Destroy(_placedGameObject[gameObjectIndex]);
            _placedGameObject[gameObjectIndex] = null;
        }
    }
}

