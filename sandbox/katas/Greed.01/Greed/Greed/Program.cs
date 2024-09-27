// See https://aka.ms/new-console-template for more information
using Greed;

Dices dices = new Dices(); //vytvoreni tridy
dices.RandomDice(); // nahodny hod kostkami
int score = dices.ScoreCount(); // vypocet skore
Console.WriteLine("Celkové skóre je: " + score);
