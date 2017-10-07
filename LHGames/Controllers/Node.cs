using StarterProject.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHGames.Controllers
{
    public class Node
    {
        private Tile t;

        public byte C { get => t.C; }
        public int X { get => t.X; }
        public int Y { get => t.Y; }
        public Tile Tile { get => t; }

        public Point Location { get; private set; }
        public bool IsWalkable { get; set; }
        public float G { get; private set; }
        public float H { get; private set; }
        public float F { get { return G + H; } }

        private Node parentNode;
        public Node ParentNode {
            get { return parentNode; }
            set {
                // When setting the parent, also calculate the traversal cost from the start node to here (the 'G' value)
                parentNode = value;
                G = parentNode.G + GetTraversalCost(Location, parentNode.Location);
            }
        }

        public Node(Tile tile)
        {
            t = tile;

            switch ((TileType)t.C) {
                case TileType.W:
                    IsWalkable = false;
                    break;
                case TileType.L:
                    IsWalkable = false;
                    break;
                default:
                    IsWalkable = true;
                    break;
            }
        }

        public States State { get; set; }
        public enum States { Dunno, Open, Closed }

        internal static float GetTraversalCost(Point location, Point otherLocation)
        {
            float deltaX = otherLocation.X - location.X;
            float deltaY = otherLocation.Y - location.Y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ") => " + Enum.GetName(State.GetType(), State);
        }
    }
}
