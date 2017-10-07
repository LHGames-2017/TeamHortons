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
        MapWrapper mapWrapper = null;

        int nbTurns = 0;

        [HttpPost]
        public string Index([FromForm]string map)
        {
            GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(map);
            var carte = AIHelper.DeserializeMap(gameInfo.CustomSerializedMap);
            
            //if (mapWrapper == null) {
            //    mapWrapper = new MapWrapper(gameInfo.Player.Position, carte);
            //} else {
            //    mapWrapper.UpdateMap(carte, gameInfo.Player.Position);
            //}

            //var path = mapWrapper.Map.FindPath(new Point(15, 17), new Point(15, 21));
            //Console.WriteLine("Count: " + path.Count);

            string action = AIHelper.CreateMoveAction(gameInfo.Player.Position);

            string[] actions =
            {
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, -1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, -1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, -1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, -1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, -1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, -1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, -1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, -1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(1, 0)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(1, 0)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateCollectAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(0, 1)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(-1, 0)),
                AIHelper.CreateMoveAction(gameInfo.Player.Position + new Point(-1, 0)),
            };

            action = actions[nbTurns];

            nbTurns = (nbTurns + 1) % 30;

            return action;
        }
    }
}
