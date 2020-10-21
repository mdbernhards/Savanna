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
            // Arrange
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

            // Act
            animalManager.MoveAllAnimals(field);

            // Assert
            Assert.Equal(field.SavannaField[expectedLine, expectedCharacter].Type, testField.SavannaField[expectedLine, expectedCharacter].Type);
            Assert.Equal(field.SavannaField[expectedLine, expectedCharacter].ID, testField.SavannaField[expectedLine, expectedCharacter].ID);
            Assert.Equal(field.SavannaField[expectedLine, expectedCharacter].Health, testField.SavannaField[expectedLine, expectedCharacter].Health);
        }

        /// <summary>
        /// Tests if Animals .HasMoved property gets reset to false
        /// </summary>
        [Fact]
        public void AnimalResetUnitTest()
        {
            // Arrange
            int line1 = 10;
            int character1 = 5;

            int line2 = 0;
            int character2 = 0;

            int line3 = 24;
            int character3 = 39;

            AnimalManager animalManager = new AnimalManager();

            Field field = new Field(30, 50);
            field.SavannaField[line1, character1] = new Lion();
            field.SavannaField[line2, character2] = new Antelope();
            field.SavannaField[line3, character3] = new Lion();


            field.SavannaField[line1, character1].HasMoved = true;
            field.SavannaField[line2, character2].HasMoved = true;
            field.SavannaField[line3, character3].HasMoved = true;

            // Act
            animalManager.AnimalReset(field);

            // Assert
            Assert.False(field.SavannaField[line1, character1].HasMoved);
            Assert.False(field.SavannaField[line2, character2].HasMoved);
            Assert.False(field.SavannaField[line2, character2].HasMoved);
        }

        /// <summary>
        /// Tests if Method SetIfAnimalDead can correctly set animal status
        /// </summary>
        [Fact]
        public void SetIfAnimalDeadUnitTest()
        {
            // Arrange
            int line1 = 10;
            int character1 = 5;

            int line2 = 0;
            int character2 = 0;

            int line3 = 24;
            int character3 = 39;

            int line4 = 20;
            int character4 = 10;

            AnimalManager animalManager = new AnimalManager();

            Field field = new Field(30, 50);
            field.SavannaField[line1, character1] = new Lion();
            field.SavannaField[line2, character2] = new Antelope();
            field.SavannaField[line3, character3] = new Lion();
            field.SavannaField[line4, character4] = new Antelope();


            field.SavannaField[line1, character1].Health = 0.5;
            field.SavannaField[line2, character2].Health = 0.55;
            field.SavannaField[line3, character3].Health = 1000;
            field.SavannaField[line4, character4].Health = 0;

            // Act
            animalManager.SetIfAnimalDead(field); //Removes 0.5 health and deletes animal if its health is 0 or bellow

            // Assert
            Assert.Null(field.SavannaField[line1, character1]);
            Assert.NotNull(field.SavannaField[line2, character2]);
            Assert.NotNull(field.SavannaField[line3, character3]);
            Assert.Null(field.SavannaField[line4, character4]);
        }

        /// <summary>
        /// Tests if SearchForAnimals Method can find animals and call the correct method EatAnimalIfCan that deletes the Antelope
        /// </summary>
        [Theory]
        [InlineData(10, 5, 10, 6)]
        [InlineData(10, 5, 10, 4)]
        [InlineData(10, 5, 11, 5)]
        [InlineData(10, 5, 9, 5)]
        [InlineData(10, 5, 11, 6)]
        [InlineData(10, 5, 11, 4)]
        [InlineData(10, 5, 9, 4)]
        [InlineData(10, 5, 9, 6)]
        public void SearchForAnimalsUnitTest(int line1, int character1, int line2, int character2)
        {
            // Arrange
            AnimalManager animalManager = new AnimalManager();

            Field field = new Field(30, 50);
            field.SavannaField[line1, character1] = new Lion();
            field.SavannaField[line2, character2] = new Antelope();

            // Act
            animalManager.SearchForAnimals(field);

            // Assert
            Assert.NotNull(field.SavannaField[line1, character1]);
            Assert.Null(field.SavannaField[line2, character2]);
        }
    }
}