using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace View
{
    public class ConnectionLine : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float width;

        [Header("Links")]
        [SerializeField] private LineSegment lineSegmentPrefab;

        [Header("Monitoring")]
        [SerializeField] private List<LineSegment> line;

        public int SegmentsCount => line.Count;

        public void AddSegment(Vector3 startPoint, Vector3 endPoint, Color color)
        {
            if (SegmentsCount > 0) line[SegmentsCount - 1].SetEndPoint(startPoint);

            var segment = Instantiate(lineSegmentPrefab, transform, true);
            segment.SetUp(startPoint, endPoint, width, color);
            line.Add(segment);
        }

        public LineSegment GetLastSegment()
        {
            return line[SegmentsCount - 1];
        }

        public void RemoveSegment(int index)
        {
            for (int i = index + 1; i < SegmentsCount; i++)
            {
                line[i].SetPoints(line[i - 1].StartPoint, line[i].StartPoint);
            }

            var segment = line[index];
            line.RemoveAt(index);
            Destroy(segment.gameObject);
        }

        public void RemoveAllSegments()
        {
            line = new List<LineSegment>();
            transform.DestroyImmediateChildren();
        }
    }
}