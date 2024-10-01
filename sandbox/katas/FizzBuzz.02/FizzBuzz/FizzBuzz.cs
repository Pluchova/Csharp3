using System;
public class FizzBuzz
{
    public void CountTo(int lastNumber)
    {
        for (int currentNumber = 1; currentNumber <= lastNumber; currentNumber++)
        {
            if (currentNumber % 3 == 0 && currentNumber % 5 == 0)
            {
                Console.WriteLine("FizzBuzz");
            }
            else if (currentNumber % 3 == 0)
            {
                Console.WriteLine("Fizz");
            }
            else if (currentNumber % 5 == 0)
            {
                Console.WriteLine("Buzz");
            }
            else
            {
                Console.WriteLine(currentNumber);
            }
        }
    }
}
