using Newtonsoft.Json;
using Savanna;
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
        int runnerLine;
        int runnerCharacter;
        int animalSeenLine;
        int animalSeenCharacter;

        /// <summary>
        /// Sets up needed variables, objects and mocks for Antelope class unit tests
        /// </summary>
        private void SetUp()
        {
            runnerLine = 10;
            runnerCharacter = 20;

            animalSeenLine = 20;
            animalSeenCharacter = 20;

            field = new Field(40, 100);

            var fieldSerializedCopy = JsonConvert.SerializeObject(field);
            fieldCopy = JsonConvert.DeserializeObject<Field>(fieldSerializedCopy);
        }

        /// <summary>
        /// Tests if Antelope Special action is working correctly
        /// </summary>
        [Fact]
        public void SpecialActionUnitTest()
        {
            //Setup
            SetUp();

            //Act
            field.SavannaField[runnerLine, runnerCharacter].SpecialAction(field, runnerLine, runnerCharacter, animalSeenLine, animalSeenCharacter);


            //Test
            Assert.Equal(field, fieldCopy);
        }
    }
}