public class Program
{
    public static void Main(string[] args)
    {
        SecretWords secretWords = new SecretWords();
        string randomSecretWord = secretWords.GetRandomWord();

        Hangman game = new Hangman(randomSecretWord);

        while (true)
        {
            while (game.IsInProgress)
            {
                Console.Clear();
                Console.WriteLine("Hra Hangman začíná:");
                Console.WriteLine($"Hádané slovo je {game.GetMaskedWord()}");
                Console.WriteLine($"Použitá písmena špatně: {game.GetIncorrectGuess()}");
                Console.WriteLine($"Zbývající pokusy: {game.GuessLeft()}");

                Console.WriteLine("Zadej písmeno:");
                char guess = Console.ReadKey().KeyChar;
                Console.WriteLine();

                string result = game.Guess(guess);
                Console.WriteLine(result);

                // Only prompt for Enter if the game is still in progress
                if (game.IsInProgress)
                {
                    Console.WriteLine("Pokračujte stiskem tlačítka Enter");
                    Console.ReadLine();
                }
            }

            if (game.GuessLeft() == 0 && !game.IsInProgress)
            {
                Console.WriteLine($"Prohráli jste! Tajné slovo bylo: {randomSecretWord}");
            }

            Console.WriteLine("Chcete hrát znovu? (A/N)");
            char choice = Console.ReadKey().KeyChar;

            if (char.ToUpper(choice) == 'A')
            {
                randomSecretWord = secretWords.GetRandomWord();
                game.ResetGame(randomSecretWord);
            }
            else
            {
                break;
            }
        }
    }
}
