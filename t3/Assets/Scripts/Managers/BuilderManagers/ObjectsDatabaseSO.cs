using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    /**
     * @class ObjectsDatabaseSO
     * @brief ScriptableObject class for storing object data in the game.
     */
    [CreateAssetMenu]
    public class ObjectsDatabaseSO : ScriptableObject
    {
        /**
         * @brief List of ObjectData instances representing objects in the game.
         */
        public List<ObjectData> objectsData;
    }

    /**
     * @class ObjectData
     * @brief Serializable class representing data for a specific object in the game.
     */
    [Serializable]
    public class ObjectData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;
        [field: SerializeField] public GameObject prefab { get; private set; }
        [field: SerializeField] public int Prix {get; private set;}
    }
}
