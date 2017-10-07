

using StarterProject.Web.Api;

namespace LHGames.ChoiceMaker
{
    public class FleeCalculator
    {
        private static double focusMultiplier;
        private static int pastPlayerHealth = 5;

        public double calculateWeight(Player player, int distanceToEnemy)
        {
            int healthLost = pastPlayerHealth - player.Health;

            pastPlayerHealth = player.Health;

            double weight;

            if (healthLost >= player.Health)
            {
                weight = 1;
            }
            else
            {
                if (player.Health == 1)
                {
                    weight = ((1.0 / distanceToEnemy) + ((double)healthLost / player.Health)) / 2;
                }
                else
                {
                    weight = ((1.0 / distanceToEnemy) + ((double)healthLost / player.Health) + (1 - ((double)player.Health / player.MaxHealth))) / 3;
                }
            }                    

            return weight;
        }
    }
}
