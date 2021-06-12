using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Model.Editor
{
    [CustomEditor(typeof(ModelTester))]
    public class ModelTesterCustomEditor : UnityEditor.Editor
    {
        private List<Vector2Int> cells=new List<Vector2Int>();
        private int cellCount;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var tester = target as ModelTester;

            if (GUILayout.Button("Init grid")) tester.InitCellGrid();
            if (GUILayout.Button("Fill grid")) tester.FillGrid();
            GUILayout.Space(10);
            DrawCellsList();
            if (GUILayout.Button("Remove cells")) tester.RemoveCells(cells);
            if (GUILayout.Button("Drop chips")) tester.DropChips();
            if (GUILayout.Button("Spawn new chips")) tester.SpawnNewChips();
            if (GUILayout.Button("Connect cells")) tester.Connect(cells);
        }

        private void DrawCellsList()
        {
            cellCount =Mathf.Max(0,  EditorGUILayout.IntField("cells count", cellCount));

            while (cellCount < cells.Count)
                cells.RemoveAt( cells.Count - 1 );
            while (cellCount > cells.Count)
                cells.Add(default);
            
            GUILayout.BeginVertical("box");
            for (int i = 0; i < cellCount; i++)
            {
                cells[i] = EditorGUILayout.Vector2IntField("cell" + i, cells[i]);
            }
            GUILayout.EndVertical();
        }
        
    }
}