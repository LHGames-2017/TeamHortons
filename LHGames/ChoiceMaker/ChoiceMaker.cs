

using LHGames.Controllers;
using StarterProject.Web.Api;
using System;
using System.Collections.Generic;

namespace LHGames.ChoiceMaker
{
    public class ChoiceMaker
    {

        static Queue<Node> path = new Queue<Node>();
        static States state = States.Dunno;
        static States lastState = States.Dunno;
        private MapWrapper mapWrapper { get => MapWrapper.Instance; }

        public string HugeStateMachine(GameInfo gameInfo)
        {
            if(gameInfo.Player.Position.X == gameInfo.Player.HouseLocation.X && gameInfo.Player.Position.Y == gameInfo.Player.HouseLocation.Y)
            {

            }

            if (state == States.BreakWall && path.Peek().Type != TileType.W)
            {
                SwitchState(lastState);
            }

            if (gameInfo.Player.CarryingCapacity > gameInfo.Player.CarriedResources)
            {
                path = new Queue<Node>(mapWrapper.GetPathToNearestType(MapWrapper.TargetType.Ressource, gameInfo.Player.Position));

                if (path.Count <= 1)
                {
                    SwitchState(States.Mine);
                }
                else
                {
                    SwitchState(States.WalkToMine);
                }
            }
            else if (gameInfo.Player.CarryingCapacity <= gameInfo.Player.CarriedResources)
            {
                path = new Queue<Node>(mapWrapper.Map.FindPath(gameInfo.Player.Position, gameInfo.Player.HouseLocation));

                if (path.Count <= 1)
                {
                    SwitchState(States.Wait);
                }
                else
                {
                    SwitchState(States.WalkToHome);
                }
            }

            Console.WriteLine(gameInfo.Player.CarriedResources + "/" + gameInfo.Player.CarryingCapacity + " -- " + Enum.GetName(state.GetType(), state));

            switch (state)
            {
                case States.BreakWall:
                    return AIHelper.CreateAttackAction(path.Peek().Location);
                case States.Mine:
                    return AIHelper.CreateCollectAction(path.Dequeue().Location);
                case States.Wait:
                    return AIHelper.CreateMoveAction(gameInfo.Player.HouseLocation);
                case States.WalkToMine:
                case States.WalkToHome:
                    Node next = path.Peek();
                    if (next.Type == TileType.W)
                    {
                        SwitchState(States.BreakWall);
                        return AIHelper.CreateAttackAction(path.Peek().Location);
                    }
                    return AIHelper.CreateMoveAction(path.Dequeue().Location);
                default:
                    return AIHelper.CreateMoveAction(gameInfo.Player.Position);
            }

        }

        public enum States
        {
            Dunno,

            WalkToMine,
            Mine,
            WalkToHome,
            Wait,
            BreakWall
        }

        private void SwitchState(States newState)
        {
            lastState = state;
            state = newState;
        }
    }
}
