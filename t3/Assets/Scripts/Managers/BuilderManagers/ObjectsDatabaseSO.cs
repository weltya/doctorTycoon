using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    [CreateAssetMenu]
    public class ObjectsDatabaseSO : ScriptableObject
    {
        public List<ObjectData> objectsData;
    }


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
