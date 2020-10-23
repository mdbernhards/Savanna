using System;
using Xunit;
using Savanna;
using Antelopes;
using Lions;

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
        /// <param name="antelopesLine">Line where the antelope doing the specialAction is</param>
        /// <param name="antelopesCharacter">Character in line where the antelope doing the specialAction is</param>
        /// <param name="expectedAntelopesLine">Line where the antelope doing the specialAction is expected to be</param>
        /// <param name="expectedAntelopesCharacter">Character in line where the antelope doing the specialAction is expected to be</param>
        /// <param name="animalSeenLine">Line where the second animal (Lion) is</param>
        /// <param name="animalSeenCharacter">Character in line where the second animal (Lion) is</param>
        [Theory]
        [InlineData(15, 20, 10, 20, 20, 20)]
        [InlineData(25, 20, 30, 20, 20, 20)]
        [InlineData(20, 15, 20, 10, 20, 20)]
        [InlineData(20, 25, 20, 30, 20, 20)]
        public void SpecialAction_AntelopeFiveCellsAwayFromLionOnEachSide_AllJumpAway(int antelopesLine, int antelopesCharacter, int expectedAntelopesLine, int expectedAntelopesCharacter, int animalSeenLine, int animalSeenCharacter)
        {
            // Arrange
            Field field = new Field(40, 100);
            field.SavannaField[antelopesLine, antelopesCharacter] = new Antelope();
            field.SavannaField[animalSeenLine, animalSeenCharacter] = new Lion();

            Field fieldCopy = new Field(40, 100);
            Array.Copy(field.SavannaField, fieldCopy.SavannaField, field.SavannaField.Length);

            fieldCopy.SavannaField[antelopesLine, antelopesCharacter] = null;
            fieldCopy.SavannaField[expectedAntelopesLine, expectedAntelopesCharacter] = new Antelope
            {
                ID = field.SavannaField[antelopesLine, antelopesCharacter].ID
            };

            // Act
            field.SavannaField[antelopesLine, antelopesCharacter].SpecialAction(field, antelopesLine, antelopesCharacter, animalSeenLine, animalSeenCharacter);

            // Assert
            Assert.Equal(field.SavannaField[expectedAntelopesLine, expectedAntelopesCharacter].Type, fieldCopy.SavannaField[expectedAntelopesLine, expectedAntelopesCharacter].Type);
            Assert.Equal(field.SavannaField[expectedAntelopesLine, expectedAntelopesCharacter].ID, fieldCopy.SavannaField[expectedAntelopesLine, expectedAntelopesCharacter].ID);
            Assert.Equal(field.SavannaField[expectedAntelopesLine, expectedAntelopesCharacter].Health, fieldCopy.SavannaField[expectedAntelopesLine, expectedAntelopesCharacter].Health);
            Assert.Equal(field.SavannaField[animalSeenLine, animalSeenCharacter], fieldCopy.SavannaField[animalSeenLine, animalSeenCharacter]);
        }
    }
}