using UnityEngine;

namespace SnakeVSBlocks.Levels
{
    [CreateAssetMenu(fileName = "NewLevelConfig", menuName = "SnakeVSBlocks/New Level Configuration", order = 56)]
    public class LevelConfiguration : ScriptableObject
    {
        [Header("Obstacles Count")]
        [SerializeField] private int _repeatCount;

        [Header("Distance")]
        [SerializeField] private int _distanceBetweenFullLine;
        [SerializeField] private int _distanceBetweenRandomLine;

        [Header("Spawn Chance")]
        [SerializeField] private int _blockSpawnChance;
        [SerializeField] private int _wallSpawnChance;
        [SerializeField] private int _bonusSpawnChance;

        [Header("Level Seed")]
        [SerializeField] private int _seed;

        public int RepeatCount => _repeatCount;
        
        public int DistanceBetweenFullLine => _distanceBetweenFullLine;    
        
        public int DistanceBetweenRandomLine => _distanceBetweenRandomLine;
        
        public int BlockSpawnChance => _blockSpawnChance;
        
        public int WallSpawnChance => _wallSpawnChance;
        
        public int BonusSpawnChance => _bonusSpawnChance;

		public int Seed => _seed;
	}
}