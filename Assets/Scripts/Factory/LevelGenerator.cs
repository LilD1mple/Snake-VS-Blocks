using SnakeVSBlocks.Blocks;
using SnakeVSBlocks.Bonuses;
using SnakeVSBlocks.FinishLine;
using SnakeVSBlocks.Levels;
using SnakeVSBlocks.Save;
using SnakeVSBlocks.SpawnPoints;
using SnakeVSBlocks.Walls;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.Factory
{
	public class LevelGenerator : MonoBehaviour
	{
		[Header("Templates")]
		[SerializeField] private Block _blockTemplate;
		[SerializeField] private Wall _wallTemplate;
		[SerializeField] private Bonus _bonusTemplate;
		[SerializeField] private Finish _finishTemplate;

		[Header("Container")]
		[SerializeField] private Transform _blocksContainer;

		[Header("Constants")]
		[SerializeField] private LevelConfiguration[] _levelConfigurations;

		[Inject] private readonly DiContainer _diContainer;

		private BlockSpawnPoint[] _blockSpawnPoints;
		private WallSpawnPoint[] _wallSpawnPoints;
		private BonusSpawnPoint[] _bonusSpawnPoints;
		private FinishSpawnPoint[] _finishSpawnPoints;

		private LevelConfiguration _currentConfiguration;

		private const string SavedLevelConfiguration = "LevelConfiguration";

		private const int DefaultIndexConfiguration = 0;

		private void Start()
		{
			_currentConfiguration = _levelConfigurations[SaveUtility.LoadValue(SavedLevelConfiguration, DefaultIndexConfiguration)];

			LoadComponents();

			GenerateLevel();
		}

		private void LoadComponents()
		{
			_blockSpawnPoints = GetComponentsInChildren<BlockSpawnPoint>();

			_wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();

			_bonusSpawnPoints = GetComponentsInChildren<BonusSpawnPoint>();

			_finishSpawnPoints = GetComponentsInChildren<FinishSpawnPoint>();
		}

		private void GenerateLevel()
		{
			Random.InitState(_currentConfiguration.Seed);

			for (int i = 0; i < _currentConfiguration.RepeatCount; i++)
			{
				PlaceObstacles();
			}

			PlaceFinish();
		}

		private void PlaceFinish()
		{
			MoveSpawnFactory(_currentConfiguration.DistanceBetweenFullLine);

			GenerateFullLineElements(_finishTemplate, _finishSpawnPoints);
		}

		private void PlaceObstacles()
		{
			MoveSpawnFactory(_currentConfiguration.DistanceBetweenFullLine);

			GenerateRandomElements(_bonusTemplate, _bonusSpawnPoints, _currentConfiguration.BonusSpawnChance, _bonusTemplate.transform.localScale.y, Random.Range(-1f, 1f));

			GenerateRandomElements(_wallTemplate, _wallSpawnPoints, _currentConfiguration.WallSpawnChance, _currentConfiguration.DistanceBetweenFullLine / 2f);

			GenerateFullLineElements(_blockTemplate, _blockSpawnPoints);

			MoveSpawnFactory(_currentConfiguration.DistanceBetweenRandomLine);

			GenerateRandomElements(_wallTemplate, _wallSpawnPoints, _currentConfiguration.WallSpawnChance, _currentConfiguration.DistanceBetweenFullLine / 2f);

			GenerateRandomElements(_blockTemplate, _blockSpawnPoints, _currentConfiguration.BlockSpawnChance, _blockTemplate.transform.localScale.y);
		}

		private void GenerateFullLineElements<T>(T generatedElement, SpawnPoint[] spawnPoints) where T : Component
		{
			for (int i = 0; i < spawnPoints.Length; i++)
				GenerateElement(generatedElement, spawnPoints[i].transform.position);
		}

		private void GenerateRandomElements<T>(T generatedElement, SpawnPoint[] spawnPoints, int spawnChance, float scaleY = 1f, float offsetY = 0f) where T : Component
		{
			for (int i = 0; i < spawnPoints.Length; i++)
			{
				if (Random.Range(0, 100) < spawnChance)
				{
					T element = GenerateElement(generatedElement, spawnPoints[i].transform.position, offsetY);

					element.transform.localScale = new Vector3(element.transform.localScale.x, scaleY, element.transform.localScale.z);
				}
			}
		}

		private T GenerateElement<T>(T generatedElement, Vector3 spawnPoint, float offsetY = 0f) where T : Component
		{
			spawnPoint.y -= offsetY;

			return _diContainer.InstantiatePrefab(generatedElement, spawnPoint, Quaternion.identity, _blocksContainer).GetComponent<T>();
		}

		private void MoveSpawnFactory(int distanceY)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
		}
	}
}
