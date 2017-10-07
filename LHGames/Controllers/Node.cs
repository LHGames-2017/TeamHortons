using StarterProject.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHGames.Controllers
{
    public class Node
    {
        public string ID { get => X + ";" + Y; }

        public byte C { get => Tile.C; }
        public int X { get => Tile.X; }
        public int Y { get => Tile.Y; }
        public Tile Tile { get; set; }

        public Point Location { get; private set; }
        public bool IsWalkable { get; set; }
        public float G { get; set; }
        public float H { get; set; }
        public float F { get => G + H; }

        public TileType Type { get => (TileType)Tile.C; }
        
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
            Location = new Point(tile.X, tile.Y);
            State = States.Dunno;
            H = int.MaxValue;
            G = 0;

            Tile = tile;
            switch ((TileType)Tile.C) {
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

        public void Reset() {
            State = States.Dunno;
            parentNode = null;
        }

        public static float GetTraversalCost(Point location, Point otherLocation)
        {
            float deltaX = otherLocation.X - location.X;
            float deltaY = otherLocation.Y - location.Y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        public override string ToString()
        {
            return "{" + X + "," + Y + "} => " + Enum.GetName(State.GetType(), State);
        }
    }
}
