

using StarterProject.Web.Api;
using System;

namespace LHGames.ChoiceMaker
{
    public class CombatCalculator
    {
        private static double focusMultiplier;
        private static int pastPlayerHealth = 5;

        public static bool isCurrent;

        public double calculateWeight(Player player, int distanceToObjective, int distanceToHouse, int attack, int defense, 
            int distanceToEnemy, int ennemyHealth)
        {
            int healthLost = pastPlayerHealth - player.Health;

            pastPlayerHealth = player.Health;

            int availableCapacity = player.CarryingCapacity - player.CarriedResources;

            int nbTurns = (int)Math.Ceiling(Decimal.Divide(ennemyHealth, attack));

            int length = distanceToObjective * 2 + distanceToHouse;

            double weight = ((attack / 11) + (defense / 11) + ((double)player.Health / player.MaxHealth) + (1 / nbTurns) + (1 / length ^ 2)*2 
                + (availableCapacity / player.CarryingCapacity) + (1 / ennemyHealth) + ((attack - 5) / 6)*2) / 10;

            if (!isCurrent)
            {
                double agressionMultiplier = ((1 / distanceToObjective) + (1 / distanceToEnemy) - (healthLost / player.Health)) / 2;

                weight = weight * agressionMultiplier;
            }

            return weight;
        }
    }
}
