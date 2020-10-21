using Xunit;
using Savanna;
using System;

namespace SavannaUnitTests
{
    /// <summary>
    /// Unit tests for the Lion Class
    /// </summary>
    public class LionUnitTests
    {
        private Field field;
        private Field fieldCopy;

        private int attackerLine;
        private int attackerCharacter;
        private int expectedAttackerLine;
        private int expectedAttackerCharacter;
        private int animalSeenLine;
        private int animalSeenCharacter;

        /// <summary>
        /// Sets up needed variables, objects and mocks for Lion class unit tests
        /// </summary>
        private void SetUp()
        {
            //Lion position
            attackerLine = 10;
            attackerCharacter = 20;

            //Lion position after SpecialAction
            expectedAttackerLine = 19;
            expectedAttackerCharacter = 20;

            //Antelope position
            animalSeenLine = 20;
            animalSeenCharacter = 20;

            //Setting up field
            field = new Field(40, 100);
            field.SavannaField[attackerLine, attackerCharacter] = new Lion();
            field.SavannaField[animalSeenLine, animalSeenCharacter] = new Antelope();

            //Setting up fieldCopy
            fieldCopy = new Field(40, 100);
            Array.Copy(field.SavannaField, fieldCopy.SavannaField, field.SavannaField.Length);

            fieldCopy.SavannaField[attackerLine, attackerCharacter] = null;
            fieldCopy.SavannaField[expectedAttackerLine, expectedAttackerCharacter] = new Lion();
            fieldCopy.SavannaField[expectedAttackerLine, expectedAttackerCharacter].ID = field.SavannaField[attackerLine, attackerCharacter].ID;
        }

        /// <summary>
        /// Tests if Lion Special action is working correctly
        /// </summary>
        [Fact]
        public void SpecialActionUnitTest()
        {
            // Arrange
            SetUp();

            // Act
            field.SavannaField[attackerLine, attackerCharacter].SpecialAction(field, attackerLine, attackerCharacter, animalSeenLine, animalSeenCharacter);

            // Assert
            Assert.Equal(field.SavannaField[expectedAttackerLine, expectedAttackerCharacter].Type, fieldCopy.SavannaField[expectedAttackerLine, expectedAttackerCharacter].Type);
            Assert.Equal(field.SavannaField[expectedAttackerLine, expectedAttackerCharacter].ID, fieldCopy.SavannaField[expectedAttackerLine, expectedAttackerCharacter].ID);
            Assert.Equal(field.SavannaField[expectedAttackerLine, expectedAttackerCharacter].Health, fieldCopy.SavannaField[expectedAttackerLine, expectedAttackerCharacter].Health);
            Assert.Equal(field.SavannaField[animalSeenLine, animalSeenCharacter], fieldCopy.SavannaField[animalSeenLine, animalSeenCharacter]);
        }
    }
}