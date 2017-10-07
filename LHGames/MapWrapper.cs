using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using StarterProject.Web.Api;

namespace LHGames
{
    public class MapWrapper
    {
        public Dictionary<Point, Tile> Map;
        public List<Point> TraveledPositions;

        public MapWrapper(Point startPosition, Tile[,] map)
        {
            CreateMap(map);
        }

        private void CreateMap(Tile[,] map)
        {
            foreach (Tile t in map)
            {
                Map.TryAdd(new Point(t.X, t.Y), t);
            }
        }

        public void UpdateMap(Tile[,] map)
        {
            
        }

        public List<Tile> ChangedTiles()
        {
            return new List<Tile>();
        }

        private List<Point> TilesToDiscover()
        {
            return new List<Point>();
        }
    }
}
