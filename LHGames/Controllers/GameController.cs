namespace StarterProject.Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using LHGames.Controllers;

    [Route("/")]
    public class GameController : Controller
    {
        AIHelper player = new AIHelper();
        AStar aStar;

        [HttpPost]
        public string Index([FromForm]string map)
        {
            GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(map);
            var carte = AIHelper.DeserializeMap(gameInfo.CustomSerializedMap);
            
            // INSERT AI CODE HERE.

            string action = AIHelper.CreateMoveAction(gameInfo.Player.Position);
            return action;
        }
    }
}
