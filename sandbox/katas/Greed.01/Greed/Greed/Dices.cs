using System;

namespace Greed
{
    public class Dices
    {
        Random random = new Random();
        int[] dice = new int[5];

        // Metoda pro generování náhodných hodů kostkami
        public void RandomDice()
        {
            for (int i = 0; i < dice.Length; i++)
            {
                dice[i] = random.Next(1, 7); // Hod kostkou (1-6)
                Console.Write(dice[i] + " "); // Výpis hodnot
            }
            Console.WriteLine(); // Pro nový řádek
        }

        // Metoda pro výpočet skóre
        public int ScoreCount()
        {
            int[] counts = new int[7]; // Počítadlo pro každé číslo kostky
            int score = 0;

            // Procházení kostek a počítání výskytů jednotlivých čísel
            for (int i = 0; i < dice.Length; i++)
            {
                counts[dice[i]]++;
            }

            // Pravidlo pro jedničky
            if (counts[1] >= 3)
            {
                score += 1000; // Trojice jedniček
            }
            score += (counts[1] % 3) * 100; // Zbývající jednotlivé jedničky

            // Pravidlo pro pětky
            if (counts[5] >= 3)
            {
                score += 500; // Trojice pětek
            }
            score += (counts[5] % 3) * 50; // Zbývající jednotlivé pětky

            // Pravidlo pro ostatní čísla (2-6)
            for (int i = 2; i <= 6; i++)
            {
                if (counts[i] >= 3)
                {
                    score += i * 100; // Trojice (2-6)
                }
            }

            return score;
        }
    }
}
