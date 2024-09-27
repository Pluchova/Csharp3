// See https://aka.ms/new-console-template for more information
using Greed;

Dices dices = new Dices();
dices.RandomDice(); // Vygenerování hodu kostkami
int score = dices.ScoreCount(); // Výpočet skóre
Console.WriteLine("Celkové skóre je: " + score);
