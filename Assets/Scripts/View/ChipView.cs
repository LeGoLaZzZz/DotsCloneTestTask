using Model;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChipView : MonoBehaviour
    {
        [SerializeField] private ChipTypesConfig chipTypesConfig;
        [SerializeField] private ChipType chipType;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public ChipType ChipType => chipType;

        public void SetUp(ChipType chipType)
        {
            this.chipType = chipType;
            spriteRenderer.color = chipTypesConfig[chipType].color;
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}