using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeVSBlocks.Blocks
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Block : MonoBehaviour
    {
        [SerializeField] private Vector2Int _destroyPriceRange;
        [SerializeField] private Color[] _blockColorsVariants;

        private SpriteRenderer _spriteRenderer;

        private int _destroyPrice;
        private int _filling;

        public event Action<int> FillingChanged;

        private int LeftToFill => _destroyPrice - _filling;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y);

            FillingChanged?.Invoke(LeftToFill);

            SetBlockColor();
        }

        public void Fill()
        {
            _filling++;

            FillingChanged?.Invoke(LeftToFill);

            if (_filling == _destroyPrice)
                Destroy(gameObject);
        }

        private void SetBlockColor()
        {
            Color newColor = _blockColorsVariants[Random.Range(0, _blockColorsVariants.Length)];

            _spriteRenderer.color = newColor;
        }
    }
}