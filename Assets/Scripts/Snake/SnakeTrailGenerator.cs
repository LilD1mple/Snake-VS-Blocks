using System.Collections.Generic;
using UnityEngine;

namespace SnakeVSBlocks.Snakes
{
    public class SnakeTrailGenerator : MonoBehaviour
    {
        [SerializeField] private SnakeSegment _snakeSegmentTemplate;

        public List<SnakeSegment> GenerateSnakeTrail(int trailSize)
        {
            List<SnakeSegment> segments = new();

            for (int i = 0; i < trailSize; i++)
                segments.Add(Instantiate(_snakeSegmentTemplate, transform));

            return segments;
        }
    }
}