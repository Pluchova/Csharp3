public class Hangman
{
    private string secretWord;
    private List<char> correctGuess;
    private List<char> incorrectGuess;
    private int maxIncorrectGuess;

    public bool IsInProgress { get; private set; }

    public Hangman(string word)
    {
        InitializeGame(word);
    }

    private void InitializeGame(string word)
    {
        secretWord = word.ToUpper();
        maxIncorrectGuess = secretWord.Length * 2;
        correctGuess = new List<char>();
        incorrectGuess = new List<char>();
        IsInProgress = true;
    }

    public string GetMaskedWord()
    {
        char[] maskedWord = new char[secretWord.Length];

        for (int i = 0; i < secretWord.Length; i++)
        {
            maskedWord[i] = correctGuess.Contains(secretWord[i]) ? secretWord[i] : '_';
        }

        return new string(maskedWord);
    }

    public string Guess(char letter)
    {
        letter = Char.ToUpper(letter);

        if (!char.IsLetter(letter))
        {
            return "Neplatný vstup, vlož písmeno!";
        }

        if (correctGuess.Contains(letter) || incorrectGuess.Contains(letter))
        {
            return "Toto písmeno jste již hádali";
        }

        if (secretWord.Contains(letter))
        {
            correctGuess.Add(letter);

            string currentMaskedWord = GetMaskedWord();

            if (IsWordGuessed())
            {
                IsInProgress = false;
                return $"Správný tip! {currentMaskedWord}\nGratuluji, vyhráli jste!";
            }

            return $"Správný tip! {currentMaskedWord}";
        }
        else
        {
            incorrectGuess.Add(letter);
            if (incorrectGuess.Count >= maxIncorrectGuess)
            {
                IsInProgress = false;
                return $"Vyčerpali jste počet pokusů! Prohráli jste. Tajné slovo bylo: {secretWord}";
            }
            return "Špatný tip!";
        }
    }

    private bool IsWordGuessed()
    {
        foreach (char c in secretWord)
        {
            if (!correctGuess.Contains(c))
            {
                return false;
            }
        }
        return true;
    }

    public int GuessLeft()
    {
        return maxIncorrectGuess - incorrectGuess.Count;
    }

    public string GetIncorrectGuess()
    {
        return string.Join(", ", incorrectGuess);
    }

    public void ResetGame(string newWord)
    {
        InitializeGame(newWord);
    }
}

