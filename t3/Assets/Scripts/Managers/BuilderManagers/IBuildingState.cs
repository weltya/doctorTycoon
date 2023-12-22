using UnityEngine;

namespace Scripts.Managers.BuilderManagers
{
    /**
     * @interface IBuildingState
     * @brief Defines the interface for different building states in a builder system.
     */
    public interface IBuildingState
    {
        /**
         * @brief Method called to end the current building state.
         */
        void EndState();

        /**
         * @brief Method called when a building action is performed.
         * @param gridPosition The grid position of the action.
         * @param rotation The rotation of the building.
         */
        void OnAction(Vector3Int gridPosition,Quaternion rotation);

        /**
         * @brief Method called to update the current building state.
         * @param gridPosition The grid position for the update.
         * @param rotation The rotation of the building.
         */
        void UpdateState(Vector3Int gridPosition,Quaternion rotation);

        /**
         * @brief Method called to rotate the building preview.
         */
        void RotatePreview();
    }
}