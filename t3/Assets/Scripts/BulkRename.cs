using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BulkRename : ScriptableObject
{
    [MenuItem("Tools/Bulk Rename")]
    static void RenameSelectedObjects()
    {
        string newName = "Ground 01 Carpet 02";
        int counter = 1;

        foreach (GameObject obj in Selection.gameObjects)
        {
            Undo.RecordObject(obj, "Bulk Rename");
            obj.name = newName;
            counter++;
        }
    }
}
