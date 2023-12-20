using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using  Scripts.UII;

using UnityEngine.UIElements;



namespace Scripts.Managers.BuilderManagers
{
    public class ObjectPlacer : MonoBehaviour
    {
        internal event Action<Room> onObjectPlaced;
        [SerializeField] private List<GameObject> _placedGameObject = new();
        [SerializeField] private UIScoreManager score;
   


        public int PlaceObject(GameObject prefab, Vector3 vector3, int ID)
        {

            bool can = score.canBuy(ID);
          
           //if batiment ne peut pas etre placer
            if(!can)
           {
            Debug.Log("coucou");
            return -1;
           }
            GameObject go = Instantiate(prefab);
            go.transform.position = vector3;
            
            Room room=go.GetComponent<Room>();
        
            onObjectPlaced?.Invoke(room);
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

