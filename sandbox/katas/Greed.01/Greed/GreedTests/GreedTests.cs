
namespace GreedTests;
using Greed;
public class UnitTest1
{
    [Fact]
    public void ScoreCountShouldReturn1150ForFourOnesAndFive()
    {
        //Arrange
        Dices dice = new Dices();
        int[] diceValues = { 1, 1, 1, 5, 1 };

        //Act
        int score = dice.ScoreCount(diceValues);

        //Assert
        Assert.Equal(1150, score);

    }

[Fact]
    public void ScoreCountShouldReturn0ForTwoTwosThreeFourSix()
    {
        //Arrange
        Dices dice = new Dices();
        int[] diceValues = { 2,3,4,6,2 };

        //Act
        int score = dice.ScoreCount(diceValues);

        //Assert
        Assert.Equal(0, score);

    }

    [Fact]
    public void ScoreCountShouldReturn350ForTripleThreesFourFive()
    {
        //Arrange
        Dices dice = new Dices();
        int[] diceValues = {3,4,5,3,3};

        //Act
        int score = dice.ScoreCount(diceValues);

        //Assert
        Assert.Equal(350, score);

    }

}
