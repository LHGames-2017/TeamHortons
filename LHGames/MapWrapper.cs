using System;
using System.Collections.Generic;
using System.Linq;
using StarterProject.Web.Api;
using LHGames.Controllers;

namespace LHGames
{
    public class MapWrapper
    {
        public static MapWrapper Instance { get; private set; }

        public AStar Map { get; private set; }
        public List<Point> TraveledPositions { get; private set; }
        public static Point HousePosition { get; private set; }

        public MapWrapper(Point startPosition, Tile[,] map)
        {
            Instance = this;

            HousePosition = startPosition;
            TraveledPositions = new List<Point>();
            Map = new AStar(map);
            TraveledPositions.Add(startPosition);
        }

        public void UpdateMap(Tile[,] map, Point position)
        {
            if (ShouldDiscover(position))
            {
                foreach (Tile t in map)
                {
                    Map.Add(t);
                }
                TraveledPositions.Add(position);
            }
        }

        //public List<Node> ChangedTiles(Tile[,] map)
        //{
        //    List<Node> result = new List<Node>();
        //    foreach (Tile t in map)
        //    {
        //        Node temp;
        //        Map.TryGetValue(new Point(t.X, t.Y), out temp);
        //        if(temp != null)
        //        {
        //            result.Add(temp);
        //        }
        //    }
        //    return result;
        //}

        public List<Node> GetPathToNearestType(TargetType t, Point position)
        {
            switch(t)
            {
                case TargetType.Ennemy:
                    return Map.FindPath(position, AStar.players.Aggregate((c, d) => Point.Distance(position, c) < Point.Distance(position, d) ? c : d));
                case TargetType.Shop:
                    return Map.FindPath(position, FindNearestNodeInternal(TileType.S, position));
                case TargetType.EnnemyHouse:
                    return Map.FindPath(position, FindNearestNodeInternal(TileType.H, position));
                case TargetType.Ressource:
                    return Map.FindPath(position, FindNearestNodeInternal(TileType.R, position));
                default:
                    break;
            }

            return new List<Node>();
        }

        public enum TargetType { Ennemy, Shop, EnnemyHouse, Ressource, }

        private List<Point> TilesToDiscover(Point position)
        {
            return new List<Point>();
        }

        private bool ShouldDiscover(Point newPosition)
        {
            return !TraveledPositions.Contains(newPosition);
        }

        private Point FindNearestNodeInternal(TileType type, Point position)
        {
            Node closest = null;
            double shortestDist = double.PositiveInfinity;
            foreach(Node n in Map.Values.Where( x => (TileType)x.Tile.C == type))
            {
                if(closest == null)
                {
                    closest = n;
                    shortestDist = Point.Distance(closest.Location, position);
                }
                else if(Point.Distance(n.Location, position) < shortestDist)
                {
                    closest = n;
                    shortestDist = Point.Distance(n.Location, position);
                }
            }

            return closest.Location;
        }


    }
}
