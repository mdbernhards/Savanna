using Xunit;
using Savanna;
using System;
using Lions;
using Antelopes;

namespace SavannaUnitTests
{
    /// <summary>
    /// Unit tests for the Lion Class
    /// </summary>
    public class LionUnitTests
    {
        /// <summary>
        /// Tests if Lion Special action is working correctly, jumping next to animal they are attacking
        /// </summary>
        [Theory]
        [InlineData(10, 20, 19, 20, 20, 20)]
        [InlineData(30, 20, 21, 20, 20, 20)]
        [InlineData(20, 10, 20, 19, 20, 20)]
        [InlineData(20, 30, 20, 21, 20, 20)]
        public void SpecialAction_FourLionsOnSidesOfAntelope_AllLionsJumpNextToAntelopes(int attackerLine, int attackerCharacter, int expectedAttackerLine, int expectedAttackerCharacter, int animalSeenLine, int animalSeenCharacter)
        {
            // Arrange
            Field field = new Field(40, 100);
            field.SavannaField[attackerLine, attackerCharacter] = new Lion();
            field.SavannaField[animalSeenLine, animalSeenCharacter] = new Antelope();

            Field fieldCopy = new Field(40, 100);
            Array.Copy(field.SavannaField, fieldCopy.SavannaField, field.SavannaField.Length);

            fieldCopy.SavannaField[attackerLine, attackerCharacter] = null;
            fieldCopy.SavannaField[expectedAttackerLine, expectedAttackerCharacter] = new Lion
            {
                ID = field.SavannaField[attackerLine, attackerCharacter].ID
            };

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