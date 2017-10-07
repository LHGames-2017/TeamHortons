namespace StarterProject.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using LHGames;
    using LHGames.Controllers;

    [Route("/")]
    public class GameController : Controller
    {
        AIHelper player = new AIHelper();

        static int nbTurns = 0;
        static MapWrapper mapWrapper = null;
        static Queue<Node> path = new Queue<Node>();
        static States state = States.Dunno;

        [HttpPost]
        public string Index([FromForm]string map)
        {
            GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(map);
            var carte = AIHelper.DeserializeMap(gameInfo.CustomSerializedMap);
            
            if (mapWrapper == null) {
                mapWrapper = new MapWrapper(gameInfo.Player.Position, carte);
            } else {
                mapWrapper.UpdateMap(carte, gameInfo.Player.Position);
            }

            if (gameInfo.Player.CarryingCapacity < gameInfo.Player.CarriedResources && path.Count == 0) {
                path = new Queue<Node>(mapWrapper.GetPathToNearestType(MapWrapper.TargetType.Ressource, gameInfo.Player.Position));

                if (path.Count <= 1) {
                    state = States.Mine;
                } else {
                    state = States.WalkToMine;
                }
            } else if (gameInfo.Player.CarryingCapacity >= gameInfo.Player.CarriedResources && path.Count == 0) {
                path = new Queue<Node>(mapWrapper.Map.FindPath(gameInfo.Player.Position, gameInfo.Player.HouseLocation));

                if (path.Count <= 1) {
                    state = States.Wait;
                } else {
                    state = States.WalkToHome;
                }
            }

            switch (state) {
                case States.Mine:
                    return AIHelper.CreateCollectAction(new Point(0, 0));
                case States.Wait:
                    return AIHelper.CreateMoveAction(gameInfo.Player.HouseLocation);
                case States.WalkToMine:
                case States.WalkToHome:
                    return AIHelper.CreateMoveAction(path.Dequeue().Location);
                default:
                    return AIHelper.CreateMoveAction(gameInfo.Player.Position);
            }

        }

        public enum States {
            Dunno,

            WalkToMine,
            Mine,
            WalkToHome,
            Wait
        }

        public void Draw(Tile[,] tiles, Point player)
        {
            for (int x = 0, width = tiles.GetLength(0); x < width; x++) {
                string line = "";
                for (int y = 0, height = tiles.GetLength(1); y < height; y++) {
                    if (tiles[x, y].X == player.X && tiles[x, y].Y == player.Y) {
                        line += "O";
                    } else {
                        switch (tiles[x, y].C) {
                            case 0:
                                line += ".";
                                break;
                            case 1:
                                line += "|";
                                break;
                            case 2:
                                line += "H";
                                break;
                            case 3:
                                line += "~";
                                break;
                            case 4:
                                line += "*";
                                break;
                            case 5:
                                line += "$";
                                break;
                        }
                    }
                }
                Console.WriteLine(line);
            }
        }
    }
}
