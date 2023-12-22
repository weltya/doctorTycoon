using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    public interface IBuildingState
    {
        void EndState();
        void OnAction(Vector3Int gridPosition,Quaternion rotation);
        void UpdateState(Vector3Int gridPosition,Quaternion rotation);
        void RotatePreview();
    }
}