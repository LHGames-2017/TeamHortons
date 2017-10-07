namespace StarterProject.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using LHGames;

    [Route("/")]
    public class GameController : Controller
    {
        AIHelper player = new AIHelper();

        static int nbTurns = 0;
        static MapWrapper mapWrapper = null;

        [HttpPost]
        public string Index([FromForm]string map)
        {
            GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(map);
            var carte = AIHelper.DeserializeMap(gameInfo.CustomSerializedMap);
            
            Draw(carte, gameInfo.Player.Position);

            if (mapWrapper == null) {
                mapWrapper = new MapWrapper(gameInfo.Player.Position, carte);
            } else {
                mapWrapper.UpdateMap(carte, gameInfo.Player.Position);
            }

            //var path = mapWrapper.Map.FindPath(new Point(15, 17), new Point(15, 21));
            //Console.WriteLine("Count: " + path.Count);

            string action = AIHelper.CreateMoveAction(gameInfo.Player.Position);

            string[] actions =
            {
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, -1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(1, 0)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(-1, 0))
            };

            action = actions[nbTurns];

            nbTurns = (nbTurns + 1) % 4;

            return action;
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
