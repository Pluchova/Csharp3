using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class SecretWords
    {
        private List<string> wordList;

        public SecretWords()
        {
            wordList = new List<string>
            {
              "POCITAC",
              "UMYVADLO",
              "SLON"

            };

        }

 public void AddNewWord(string word)
        {
           wordList.Add(word.ToUpper());
        }

        public string GetRandomWord()
        {
            Random random = new Random();
            int index = random.Next(wordList.Count);
            return wordList[index];
        }
}


