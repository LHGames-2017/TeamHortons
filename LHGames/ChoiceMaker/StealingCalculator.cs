

using StarterProject.Web.Api;
using System;

namespace LHGames.ChoiceMaker
{
    public class StealingCalculator
    {
        private double focusMultiplier;

        public double calculateWeight(ref Player player, int distanceToObjective, int distanceToHouse, int stealingAmount)
        {
            int availableCapacity = player.CarryingCapacity - player.CarriedResources;

            int nbTurns = (int)Math.Ceiling(Decimal.Divide(availableCapacity, stealingAmount));

            int length = distanceToObjective * 2 + distanceToHouse;

            double weight = ((1 / nbTurns) + (1 / length ^ 2) + (availableCapacity / player.CarryingCapacity)) / 3;

            return weight;
        }
    }
}
