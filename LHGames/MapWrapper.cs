﻿using System;
using System.Collections.Generic;
using System.Linq;
using StarterProject.Web.Api;
using LHGames.Controllers;

namespace LHGames
{
    public class MapWrapper
    {
        public AStar Map;
        public List<Point> TraveledPositions;
        public Point HousePosition;

        public MapWrapper(Point startPosition, Tile[,] map)
        {
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

        private List<Point> TilesToDiscover(Point position)
        {
            throw new NotImplementedException();
        }

        private bool ShouldDiscover(Point newPosition)
        {
            return !TraveledPositions.Contains(newPosition);
        }

        private Node FindNearestNodeInternal(TileType type, Point position)
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

            return closest;
        }


    }
}
