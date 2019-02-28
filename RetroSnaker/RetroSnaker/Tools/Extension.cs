using System.Collections.Generic;

namespace RetroSnaker
{
    public static class Extension
    {
        public static List<Node> ToList(this Node[,] map)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    nodes.Add(map[i, j]);
                }
            }

            return nodes;
        }
    }
}