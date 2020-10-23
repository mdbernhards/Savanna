using System;
using Xunit;
using Savanna;
using Antelopes;
using Lions;

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
        /// <param name="lionLine">Line where the lion doing the specialAction is</param>
        /// <param name="lionCharacter">Character in line where the lion doing the specialAction is</param>
        /// <param name="expectedLionLine">Line where the lion doing the specialAction is expected to be</param>
        /// <param name="expectedLionCharacter">Character in line where the lion doing the specialAction is expected to be</param>
        /// <param name="animalSeenLine">Line where the second animal (Antelope) is</param>
        /// <param name="animalSeenCharacter">Character in line where the second animal (Antelope) is</param>
        [Theory]
        [InlineData(10, 20, 19, 20, 20, 20)]
        [InlineData(30, 20, 21, 20, 20, 20)]
        [InlineData(20, 10, 20, 19, 20, 20)]
        [InlineData(20, 30, 20, 21, 20, 20)]
        public void SpecialAction_FourLionsOnSidesOfAntelope_AllLionsJumpNextToAntelopes(int lionLine, int lionCharacter, int expectedLionLine, int expectedLionCharacter, int animalSeenLine, int animalSeenCharacter)
        {
            // Arrange
            Field field = new Field(40, 100);
            field.SavannaField[lionLine, lionCharacter] = new Lion();
            field.SavannaField[animalSeenLine, animalSeenCharacter] = new Antelope();

            Field fieldCopy = new Field(40, 100);
            Array.Copy(field.SavannaField, fieldCopy.SavannaField, field.SavannaField.Length);

            fieldCopy.SavannaField[lionLine, lionCharacter] = null;
            fieldCopy.SavannaField[expectedLionLine, expectedLionCharacter] = new Lion
            {
                ID = field.SavannaField[lionLine, lionCharacter].ID
            };

            // Act
            field.SavannaField[lionLine, lionCharacter].SpecialAction(field, lionLine, lionCharacter, animalSeenLine, animalSeenCharacter);

            // Assert
            Assert.Equal(field.SavannaField[expectedLionLine, expectedLionCharacter].Type, fieldCopy.SavannaField[expectedLionLine, expectedLionCharacter].Type);
            Assert.Equal(field.SavannaField[expectedLionLine, expectedLionCharacter].ID, fieldCopy.SavannaField[expectedLionLine, expectedLionCharacter].ID);
            Assert.Equal(field.SavannaField[expectedLionLine, expectedLionCharacter].Health, fieldCopy.SavannaField[expectedLionLine, expectedLionCharacter].Health);
            Assert.Equal(field.SavannaField[animalSeenLine, animalSeenCharacter], fieldCopy.SavannaField[animalSeenLine, animalSeenCharacter]);
        }
    }
}