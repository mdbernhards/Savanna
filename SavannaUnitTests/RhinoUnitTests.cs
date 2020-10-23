using System;
using Xunit;
using Savanna;
using Antelopes;
using Lions;
using Rhinos;

namespace SavannaUnitTests
{
    /// <summary>
    /// Unit tests for the Rhino Class
    /// </summary>
    public class RhinoUnitTests
    {
        /// <summary>
        /// Tests if Rhinos Special action is working correctly, charging an aggresive animal and pushing it back 
        /// </summary>
        /// <param name="rhinoLine">Line where the rhino doing the specialAction is</param>
        /// <param name="rhinoCharacter">Character in line where the rhino doing the specialAction is</param>
        /// <param name="LionLine">Line where the second animal (Lion) is</param>
        /// <param name="LionCharacter">Character in line where the second animal (Lion) is</param>
        /// <param name="expectedLionLine">Line where the second animal (Lion) is expected to be</param>
        /// <param name="expectedLionCharacter">Character in line where the second animal (Lion) is expected to be</param>
        [Theory]
        [InlineData(15, 20, 20, 20, 25, 20)]
        [InlineData(23, 20, 20, 20, 17, 20)]
        [InlineData(20, 14, 20, 20, 20, 26)]
        [InlineData(20, 29, 20, 20, 20, 11)]
        [InlineData(20, 30, 10, 7, 10, 7)]
        public void SpecialAction_FourLionsOnSidesOfRhino_AllLionsChargedAndPushedBack(int rhinoLine, int rhinoCharacter, int LionLine, int LionCharacter, int expectedLionLine, int expectedLionCharacter)
        {
            int expectedRhinoLine = LionLine;
            int expectedRhinoCharacter = LionCharacter;
            // Arrange
            Field field = new Field(40, 100);
            field.SavannaField[rhinoLine, rhinoCharacter] = new Rhino();
            field.SavannaField[LionLine, LionCharacter] = new Lion();

            Field fieldCopy = new Field(40, 100);

            fieldCopy.SavannaField[expectedRhinoLine, expectedRhinoCharacter] = new Rhino();
            fieldCopy.SavannaField[expectedRhinoLine, expectedRhinoCharacter].ID = field.SavannaField[rhinoLine, rhinoCharacter].ID;

            fieldCopy.SavannaField[expectedLionLine, expectedLionCharacter] = new Lion();
            fieldCopy.SavannaField[expectedLionLine, expectedLionCharacter].ID = field.SavannaField[LionLine, LionCharacter].ID;

            // Act
            field.SavannaField[rhinoLine, rhinoCharacter].SpecialAction(field, rhinoLine, rhinoCharacter, LionLine, LionCharacter);

            // Assert
            Assert.Equal(field.SavannaField[expectedRhinoLine, expectedRhinoCharacter].Type, fieldCopy.SavannaField[expectedRhinoLine, expectedRhinoCharacter].Type);
            Assert.Equal(field.SavannaField[expectedRhinoLine, expectedRhinoCharacter].ID, fieldCopy.SavannaField[expectedRhinoLine, expectedRhinoCharacter].ID);
            Assert.Equal(field.SavannaField[expectedRhinoLine, expectedRhinoCharacter].Health, fieldCopy.SavannaField[expectedRhinoLine, expectedRhinoCharacter].Health);

            Assert.Equal(field.SavannaField[expectedLionLine, expectedLionCharacter].Type, fieldCopy.SavannaField[expectedLionLine, expectedLionCharacter].Type);
            Assert.Equal(field.SavannaField[expectedLionLine, expectedLionCharacter].ID, fieldCopy.SavannaField[expectedLionLine, expectedLionCharacter].ID);
            Assert.Equal(field.SavannaField[expectedLionLine, expectedLionCharacter].Health, fieldCopy.SavannaField[expectedLionLine, expectedLionCharacter].Health);
        }
    }
}
