

using StarterProject.Web.Api;

namespace LHGames.ChoiceMaker
{
    public class FleeCalculator
    {
        private double focusMultiplier;
        private int pastPlayerHealth;

        public FleeCalculator()
        {
            focusMultiplier = 0;
            pastPlayerHealth = 0;
        }

        public double calculateWeight(ref Player player, int distanceToEnemy)
        {
            int healthLost = pastPlayerHealth - player.Health;

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
