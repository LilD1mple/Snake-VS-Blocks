using SnakeVSBlocks.Snakes;
using TMPro;
using UnityEngine;

namespace SnakeVSBlocks.UI.View
{
	public class ScoreViewer : MonoBehaviour
	{
		[SerializeField] private Snake _snake;
		[SerializeField] private TMP_Text _scoreText;

		private void OnEnable()
		{
			_snake.SnakeSegmentUpdated += OnSnakeSegmentUpdated;
		}

		private void OnDisable()
		{
			_snake.SnakeSegmentUpdated -= OnSnakeSegmentUpdated;
		}

		private void OnSnakeSegmentUpdated(int score)
		{
			_scoreText.text = $"—чет: {score}";
		}
	}
}
