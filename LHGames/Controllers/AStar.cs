using StarterProject.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHGames.Controllers
{
    public class AStar : Dictionary<Point, Node>
    {
        public static List<Point> players = new List<Point>();

        public AStar(Tile[,] tiles)
        {
            foreach (Tile tile in tiles) {
                Add(new Point(tile.X, tile.Y), new Node(tile));
            }
        }

        public Node this[Tile t] {
            get {
                return this[t.X, t.Y];
            }
            set {
                this[t.X, t.Y] = value;
            }
        }

        public Node this[int x, int y] {
            get {
                return this[new Point(x, y)];
            }
            set {
                this[new Point(x, y)] = value;
            }
        }

        public void Add(Tile tile)
        {
            Point p = new Point(tile.X, tile.Y);
            if (ContainsKey(p)) {
                this[p] = new Node(tile);
            } else {
                Add(p, new Node(tile));
            }
        }

        public void Add(IEnumerable<Tile> tiles)
        {
            foreach (Tile tile in tiles) {
                Add(tile);
            }
        }

        public List<Node> FindPath(Point from, Point to)
        {
            try {
                UpdateHs(this.Select(t => t.Value).ToList(), this[to]);

                // The start node is the first entry in the 'open' list
                List<Point> path = new List<Point>();
                bool success = Search(this[from], this[to]);
                if (success) {
                    // If a path was found, follow the parents from the end node to build a list of locations
                    Node node = this[from];
                    while (node.ParentNode != null) {
                        path.Add(node.Location);
                        node = node.ParentNode;
                    }

                    // Reverse the list so it's in the correct order when returned
                    path.Reverse();
                }

                return path.Select(p => this[p]).ToList();
            } catch (KeyNotFoundException) {
                throw new Exception("Dude, ya une tile qui existe pas");
            }
        }

        private bool Search(Node currTile, Node to)
        {
            currTile.State = Node.States.Closed;
            List<Node> nextNodes = GetAdjacentWalkableNodes(currTile);

            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach (var nextNode in nextNodes) {
                if (Search(nextNode, to)) {
                    return true;
                }
            }
            return false;
        }

        private void UpdateHs(List<Node> nodes, Node to) {
            foreach (var node in this) {
                if (node.Value.lastH != null && node.Value.lastH.ID != to.ID) {
                    node.Value.H = Node.GetTraversalCost(node.Value.Location, to.Location);
                    node.Value.lastH = to;
                }
            }
        }

        private void UpdateH(Node node, Node to) {
            //if (node.lastH.ID != to.ID) {
            node.H = Node.GetTraversalCost(node.Location, to.Location);
            node.lastH = to;
            //}
        }

        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations) {
                int x = location.X;
                int y = location.Y;

                Node node = this[location];
                // Ignore non-walkable nodes
                if (!node.IsWalkable) {
                    continue;
                }

                // Ignore already-closed nodes
                if (node.State == Node.States.Closed) {
                    continue;
                }

                // Ignore players

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == Node.States.Open) {
                    float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                    float gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G) {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                } else {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.State = Node.States.Open;
                    walkableNodes.Add(node);
                }
            }

            return walkableNodes;
        }

        private IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
        {
            return new Point[]
            {
                new Point(fromLocation.X - 1, fromLocation.Y - 1),
                new Point(fromLocation.X - 1, fromLocation.Y),
                new Point(fromLocation.X - 1, fromLocation.Y + 1),
                new Point(fromLocation.X, fromLocation.Y + 1),
                new Point(fromLocation.X + 1, fromLocation.Y + 1),
                new Point(fromLocation.X + 1, fromLocation.Y),
                new Point(fromLocation.X + 1, fromLocation.Y - 1),
                new Point(fromLocation.X,   fromLocation.Y - 1)
            }.Where(p => ContainsKey(p));
        }
    }
}
