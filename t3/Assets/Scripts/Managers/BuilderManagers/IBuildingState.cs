using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    public interface IBuildingState
    {
        void EndState();
        void OnAction(Vector3Int gridPosition);
        void UpdateState(Vector3Int gridPosition);
    }
}