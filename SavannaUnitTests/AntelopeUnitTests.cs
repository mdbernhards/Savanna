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
        private Field field;
        private Field fieldCopy;

        private int runnerLine;
        private int runnerCharacter;
        private int expectedRunnerLine;
        private int expectedRunnerCharacter;
        private int animalSeenLine;
        private int animalSeenCharacter;


        /// <summary>
        /// Sets up needed variables, objects and mocks for Antelope class unit tests
        /// </summary>
        private void SetUp()
        {
            //Antelope position
            runnerLine = 15;
            runnerCharacter = 20;

            //Antelope position after SpecialAction
            expectedRunnerLine = 10;
            expectedRunnerCharacter = 20;

            //Lion position
            animalSeenLine = 20;
            animalSeenCharacter = 20;

            //Setting up field
            field = new Field(40, 100);
            field.SavannaField[runnerLine, runnerCharacter] = new Antelope();
            field.SavannaField[animalSeenLine, animalSeenCharacter] = new Lion();

            //Setting up fieldCopy
            fieldCopy = new Field(40, 100);
            Array.Copy(field.SavannaField, fieldCopy.SavannaField, field.SavannaField.Length);

            fieldCopy.SavannaField[runnerLine, runnerCharacter] = null;
            fieldCopy.SavannaField[expectedRunnerLine, expectedRunnerCharacter] = new Antelope();
            fieldCopy.SavannaField[expectedRunnerLine, expectedRunnerCharacter].ID = field.SavannaField[runnerLine, runnerCharacter].ID;
        }

        /// <summary>
        /// Tests if Antelope Special action is working correctly
        /// </summary>
        [Fact]
        public void SpecialActionUnitTest()
        {
            // Arrange
            SetUp();

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