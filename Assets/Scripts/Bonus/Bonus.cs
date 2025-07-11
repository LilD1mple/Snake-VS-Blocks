using TMPro;
using UnityEngine;

namespace SnakeVSBlocks.Bonuses
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class Bonus : MonoBehaviour
	{
		[SerializeField] private Color[] _bonusColorsVariants;
		[SerializeField] private Vector2Int _bonusSizeRange;
		[SerializeField] private TMP_Text _bonusScoreText;

		private SpriteRenderer _spriteRenderer;

		private int _bonusScore;

		public int BonusScore => _bonusScore;

		private void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();

			SetBonus();

			SetBonusColor();
		}

		private void SetBonus()
		{
			_bonusScore = Random.Range(_bonusSizeRange.x, _bonusSizeRange.y);

			_bonusScoreText.text = _bonusScore.ToString();
		}

		private void SetBonusColor() => _spriteRenderer.color = _bonusColorsVariants[Random.Range(0, _bonusColorsVariants.Length)];
	}
}
