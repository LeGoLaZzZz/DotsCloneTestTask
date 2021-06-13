using System;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

namespace View.Editor
{
    [CustomEditor(typeof(CellView))] [CanEditMultipleObjects]
    public class CellViewCustomEditor : UnityEditor.Editor
    {
        private CellView _cellView;

        public static void DrawBorder(CellView cellView, float borderSize)
        {
            var position = cellView.transform.position;
            var scale = cellView.transform.localScale;
            scale.z = 0;

            var leftDown = position - scale / 2;
            var rightUp = position + scale / 2;
            var leftUp = leftDown + Vector3.up * scale.y;
            var rightDown = rightUp + Vector3.down * scale.y;


            Handles.DrawDottedLine(leftDown, leftUp, borderSize);
            Handles.DrawDottedLine(leftUp, rightUp, borderSize);
            Handles.DrawDottedLine(rightUp, rightDown, borderSize);
            Handles.DrawDottedLine(rightDown, leftDown, borderSize);
        }

        public static void DrawCoords(CellView cellView)
        {
            Handles.Label(cellView.transform.position, cellView.Coords.ToString());
        }

        private void Awake()
        {
            _cellView = target as CellView;
        }


        private void OnSceneGUI()
        {
            DrawCoords(_cellView);
            DrawBorder(_cellView, 3f);
        }
    }
}