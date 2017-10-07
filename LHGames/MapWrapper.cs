using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using StarterProject.Web.Api;
using LHGames.Controllers;

namespace LHGames
{
    public class MapWrapper
    {
        public AStar Map { get; private set; }
        public List<Point> TraveledPositions { get; private set; }
        public Point HousePosition { get; private set; }

        public MapWrapper(Point startPosition, Tile[,] map)
        {
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

        public List<Node> GetPathToNearestType(TargetType t)
        {
            switch(t)
            {
                case TargetType.Ennemy:
                    break;
                case TargetType.Shop:
                    break;
                case TargetType.House:
                    break;
                case TargetType.Ressource:
                    break;
                default:
                    break;
            }

            return new List<Node>();
        }

        public enum TargetType { Ennemy, Shop, House, Ressource}

        private List<StarterProject.Web.Api.Point> TilesToDiscover(StarterProject.Web.Api.Point position)
        {
            return new List<StarterProject.Web.Api.Point>();
        }

        private bool ShouldDiscover(StarterProject.Web.Api.Point newPosition)
        {
            return !TraveledPositions.Contains(newPosition);
        }
    }
}
