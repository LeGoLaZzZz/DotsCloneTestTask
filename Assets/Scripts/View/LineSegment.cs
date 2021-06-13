using UnityEngine;

namespace View
{
    public class LineSegment : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [Header("Monitoring")]
        [SerializeField] private Vector3 startPoint;
        [SerializeField] private Vector3 endPoint;
        [SerializeField] private float width;

        private Transform _transform;

        public Vector3 StartPoint => startPoint;
        public Vector3 EndPoint => endPoint;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetUp(Vector3 startPoint, Vector3 endPoint, float width, Color color)
        {
            this.width = width;
            _transform = transform;
            SetPoints(startPoint, endPoint);
            SetColor(color);
        }

        public void SetPoints(Vector3 startPoint, Vector3 endPoint)
        {
            this.endPoint = endPoint;
            this.startPoint = startPoint;

            _transform.position = startPoint;

            // _transform.LookAt(endPoint);
            // _transform.Rotate(Vector3.Angle(_transform.forward, endPoint - _transform.position), 0, 0);
            _transform.localRotation = Quaternion.LookRotation(endPoint - _transform.position,_transform.up);
            var length = Vector3.Distance(startPoint, endPoint);
            SetSize(length, width);
        }

        public void SetEndPoint(Vector3 endPoint)
        {
            SetPoints(StartPoint, endPoint);
        }

        public void SetStartPoint(Vector3 startPoint)
        {
            SetPoints(startPoint, EndPoint);
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        private void SetSize(float length, float width)
        {
            spriteRenderer.size = new Vector2(length, 1);

            var transformLocalScale = transform.localScale;
            transformLocalScale.y = width;
            transform.localScale = transformLocalScale;
        }
    }
}