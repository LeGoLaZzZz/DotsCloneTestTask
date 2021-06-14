using System.Collections;
using Model;
using UnityEngine;

namespace View
{
    public class ChipView : MonoBehaviour
    {
        private static readonly int Dropped = Animator.StringToHash("Dropped");
        private static readonly int Removed = Animator.StringToHash("Removed");

        [Header("Settings")]
        [SerializeField] private float dropTime = 1f;
        [SerializeField] private AnimationCurve dropProgress;
        [SerializeField] private float destroyDelay = 0.1f;

        [Header("Links")]
        [SerializeField] private ChipTypesConfig chipTypesConfig;
        [SerializeField] private SpriteRenderer[] spriteRenderers;
        [SerializeField] private Animator animator;

        [Header("Monitoring")]
        [SerializeField] private ChipType chipType;
        private static readonly int InteractStarted = Animator.StringToHash("InteractStarted");

        public ChipType ChipType => chipType;

        public void SetUp(ChipType chipType)
        {
            this.chipType = chipType;
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = chipTypesConfig[chipType].color;
            }
        }

        public void ScoreOut()
        {
            animator.SetTrigger(Removed);
            Destroy(gameObject, destroyDelay);
        }

        public void Drop(Vector3 startPoint, Vector3 endPoint)
        {
            StartCoroutine(DropCoroutine(startPoint, endPoint, dropTime));
        }

        public void InteractStartedView()
        {
            animator.SetTrigger(InteractStarted);
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
            animator.SetTrigger(Dropped);
        }
    }
}