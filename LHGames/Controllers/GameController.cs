namespace StarterProject.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using LHGames;
    using LHGames.Controllers;
    using LHGames.ChoiceMaker;

    [Route("/")]
    public class GameController : Controller
    {
        // AIHelper player = new AIHelper();
        
        static MapWrapper mapWrapper = null;
        static ChoiceMaker choiceMaker = new ChoiceMaker();

        [HttpPost]
        public string Index([FromForm]string map)
        {
            GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(map);
            var carte = AIHelper.DeserializeMap(gameInfo.CustomSerializedMap);

            Draw(carte, gameInfo.Player.Position);

            if (mapWrapper == null)
            {
                mapWrapper = new MapWrapper(gameInfo.Player.Position, carte);
            }
            else
            {
                mapWrapper.UpdateMap(carte, gameInfo.Player.Position);
            }
            return choiceMaker.HugeStateMachine(gameInfo);

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
