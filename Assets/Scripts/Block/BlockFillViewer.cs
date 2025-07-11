using TMPro;
using UnityEngine;

namespace SnakeVSBlocks.Blocks
{
    public class BlockFillViewer : MonoBehaviour
    {
        [SerializeField] private Block _block;
        [SerializeField] private TMP_Text _fillText;

        private void OnEnable()
        {
            _block.FillingChanged += OnFillChanged;
        }

        private void OnDisable()
        {
            _block.FillingChanged -= OnFillChanged;
        }

        private void OnFillChanged(int fill)
        {
            _fillText.text = fill.ToString();
        }
    }
}