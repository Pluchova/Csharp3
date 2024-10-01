using System;

namespace Greed
{
    public class Dices
    {
        const int NumberOfDice = 5;
        const int NumberOfSideOnDice = 6;
        Random random = new Random();
        int[] dice = new int[NumberOfDice]; //vytvoření prázdného pole velikosti 5

        public void RandomDice()
        {
            for (int i = 0; i < NumberOfDice; i++)
            {
                dice[i] = random.Next(1, NumberOfSideOnDice + 1); // hod kostkou (1-6)
                Console.Write(dice[i] + " ");
            }
        }

        public int ScoreCount()
        {
            return ScoreCount(dice);
        }

        public int ScoreCount(int[] dice )
        {
            int[] counts = new int[NumberOfSideOnDice + 1];
            int score = 0;

            for (int i = 0; i < NumberOfDice; i++) //počítání jednotlivých čísel
            {
                counts[dice[i]]++;
            }


            if (counts[1] >= 3)
            {
                score += 1000; // trojice jedniček = skore 1000
            }
            score += (counts[1] % 3) * 100; // za každou další 1 přidání 100 ke skóre


            if (counts[5] >= 3) //trojice pětek = skore 500
            {
                score += 500;
            }
            score += (counts[5] % 3) * 50; // za každou další 5 přidání 50 ke skore


            for (int i = 2; i <= 6; i++)
            {
                if (counts[i] >= 3)
                {
                    score += i * 100; //další trojce od 2-6
                }
            }

            return score;
        }
    }
}
