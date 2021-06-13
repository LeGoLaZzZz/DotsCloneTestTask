using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace View.Editor
{
    [CustomEditor(typeof(CellGridViewGenerator))]
    public class CellGridViewGeneratorCustomEditor : UnityEditor.Editor
    {
        private Color _gizmosColor = new Color(0.93f, 0.95f, 0.95f, 0.69f);
        private float _borderSize = 6f;

        private CellGridViewGenerator _generator;
        private IEnumerable<CellView> _cells;

        private void Awake()
        {
            _generator = target as CellGridViewGenerator;
            UpdateCells();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("Editor settings");
            if (GUILayout.Button("Update cells")) UpdateCells();
        }


        private void OnSceneGUI()
        {
            if (_cells == null) UpdateCells();
            DrawCoords();
            DrawBorders();
        }

        private void UpdateCells()
        {
            _cells = GetCells();
        }

        private IEnumerable<CellView> GetCells()
        {
            return _generator.GridParent.GetComponentsInChildren<CellView>();
        }

        private void DrawCoords()
        {
            foreach (var cellView in _cells)
            {
                CellViewCustomEditor.DrawCoords(cellView);
            }
        }


        private void DrawBorders()
        {
            Handles.color = _gizmosColor;

            foreach (var cellView in _cells)
            {
                CellViewCustomEditor.DrawBorder(cellView, _borderSize);
            }
        }
    }
}