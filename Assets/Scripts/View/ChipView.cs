using System.Collections;
using Model;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChipView : MonoBehaviour
    {
        [Header("Settings")]
        // [SerializeField] private float dropSpeed = 1f;
        [SerializeField] private float dropTime = 1f;
        [SerializeField] private AnimationCurve dropProgress;
        [SerializeField] private float destroyDelay = 0.1f;

        [Header("Links")]
        [SerializeField] private ChipTypesConfig chipTypesConfig;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;

        [Header("Monitoring")]
        [SerializeField] private ChipType chipType;

        public ChipType ChipType => chipType;

        public void SetUp(ChipType chipType)
        {
            this.chipType = chipType;
            spriteRenderer.color = chipTypesConfig[chipType].color;
        }

        public void ScoreOut()
        {
            //TODO animation
            Destroy(gameObject, destroyDelay);
        }

        public void Drop(Vector3 startPoint, Vector3 endPoint)
        {
            var distance = Vector3.Distance(startPoint, endPoint);
            // var fullTime = distance / dropSpeed;
            var fullTime = dropTime;

            StartCoroutine(DropCoroutine(startPoint, endPoint, fullTime));
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private IEnumerator DropCoroutine(Vector3 startPoint, Vector3 endPoint, float dropTime)
        {
            var moveVector = endPoint - startPoint;
            var curTime = 0f;
            while (curTime < dropTime)
            {
                transform.position = startPoint + dropProgress.Evaluate(curTime / dropTime) * (moveVector);
                curTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPoint;
            yield break;
        }
    }
}