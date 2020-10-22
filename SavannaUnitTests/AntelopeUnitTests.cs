using Savanna;
using System;
using Xunit;

namespace SavannaUnitTests
{
    /// <summary>
    /// Unit tests for the Antelope Class
    /// </summary>
    public class AntelopeUnitTests
    {
        /// <summary>
        /// Tests if Antelope Special action is working correctly
        /// </summary>
        [Theory]
        [InlineData(15, 20, 10, 20, 20, 20)]
        [InlineData(25, 20, 30, 20, 20, 20)]
        [InlineData(20, 15, 20, 10, 20, 20)]
        [InlineData(20, 25, 20, 30, 20, 20)]
        public void SpecialAction_AntelopeFiveCellsAwayFromLionOnEachSide_AllJumpAway(int runnerLine, int runnerCharacter, int expectedRunnerLine, int expectedRunnerCharacter, int animalSeenLine, int animalSeenCharacter)
        {
            // Arrange
            Field field = new Field(40, 100);
            field.SavannaField[runnerLine, runnerCharacter] = new Antelope();
            field.SavannaField[animalSeenLine, animalSeenCharacter] = new Lion();

            Field fieldCopy = new Field(40, 100);
            Array.Copy(field.SavannaField, fieldCopy.SavannaField, field.SavannaField.Length);

            fieldCopy.SavannaField[runnerLine, runnerCharacter] = null;
            fieldCopy.SavannaField[expectedRunnerLine, expectedRunnerCharacter] = new Antelope();
            fieldCopy.SavannaField[expectedRunnerLine, expectedRunnerCharacter].ID = field.SavannaField[runnerLine, runnerCharacter].ID;

            // Act
            field.SavannaField[runnerLine, runnerCharacter].SpecialAction(field, runnerLine, runnerCharacter, animalSeenLine, animalSeenCharacter);

            // Assert
            Assert.Equal(field.SavannaField[expectedRunnerLine, expectedRunnerCharacter].Type, fieldCopy.SavannaField[expectedRunnerLine, expectedRunnerCharacter].Type);
            Assert.Equal(field.SavannaField[expectedRunnerLine, expectedRunnerCharacter].ID, fieldCopy.SavannaField[expectedRunnerLine, expectedRunnerCharacter].ID);
            Assert.Equal(field.SavannaField[expectedRunnerLine, expectedRunnerCharacter].Health, fieldCopy.SavannaField[expectedRunnerLine, expectedRunnerCharacter].Health);
            Assert.Equal(field.SavannaField[animalSeenLine, animalSeenCharacter], fieldCopy.SavannaField[animalSeenLine, animalSeenCharacter]);
        }
    }
}