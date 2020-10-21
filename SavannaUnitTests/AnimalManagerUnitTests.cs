using Newtonsoft.Json;
using Savanna;
using Moq;
using Xunit;
using System;

namespace SavannaUnitTests
{
    /// <summary>
    /// Unit tests for the Animal Manager Class
    /// </summary>
    public class AnimalManagerUnitTests
    {
        /// <summary>
        /// Tests if MoveAllAnimals method moves one animal correctly
        /// </summary>
        [Fact]
        public void MoveAllAnimalsUnitTest()
        {
            //Setup
            int line = 10;
            int character = 30;

            int expectedLine = 10;
            int expectedCharacter = 29;

            //Setting up field
            Field field = new Field(20, 40);
            field.SavannaField[line, character] = new Antelope();

            //Setting up testField
            var animalSerializedCopy = JsonConvert.SerializeObject(field.SavannaField[line, character]);
            Animal animalCopy = JsonConvert.DeserializeObject<Antelope>(animalSerializedCopy);

            Field testField = new Field(20, 40);
            testField.SavannaField[expectedLine, expectedCharacter] = animalCopy;

            //Setting up randomInt mock
            var mockRandomGenerator = new Mock<Random>();
            mockRandomGenerator.Setup(random => random.Next(4)).Returns(2);

            AnimalManager animalManager = new AnimalManager(mockRandomGenerator.Object);

            //Act
            animalManager.MoveAllAnimals(field);

            //Test
            Assert.Equal(field.SavannaField[expectedLine, expectedCharacter].Type, testField.SavannaField[expectedLine, expectedCharacter].Type);
            Assert.Equal(field.SavannaField[expectedLine, expectedCharacter].ID, testField.SavannaField[expectedLine, expectedCharacter].ID);
            Assert.Equal(field.SavannaField[expectedLine, expectedCharacter].Health, testField.SavannaField[expectedLine, expectedCharacter].Health);
        }
    }
}