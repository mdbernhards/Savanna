using Newtonsoft.Json;
using System;

namespace Savanna
{
    /// <summary>
    /// Class that manages all animal actions like moving, attacking, seeing, spawning and more.
    /// </summary>
    public class AnimalManager
    {
        private Animal animal;


        /// <summary>
        /// Starts the moving process, chooses what type of moving will the animal do: normal, running away, attacking.
        /// </summary>
        public void AllAnimalsMove(Field field) 
        {
            animal = new Animal();

            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    if(field.SavannaField[line,character].Type != 'E')
                    {
                        var objectCopy = JsonConvert.SerializeObject(field.SavannaField[line, character]);
                        animal = JsonConvert.DeserializeObject<Animal>(objectCopy);

                        MoveToSide(line, character, field);
                    }
                }
            }
        }

        /// <summary>
        /// Normal moving way, where animal if it can moves randomly to one side
        /// </summary>
        public void MoveToSide(int line, int character, Field field)
        {
            Random randomInt = new Random();

            int side = randomInt.Next(4);
            int placeInColumn = line;
            int placeInRow = character;

            if (side == 0)
            {
                placeInColumn--;
            }
            else if (side == 1)
            {
                placeInColumn++;
            }
            else if (side == 2)
            {
                placeInRow--;
            }
            else if (side == 3)
            {
                placeInRow++;
            }

            if (placeInColumn >= 0 && placeInColumn < field.Height && placeInRow >= 0 && placeInRow < field.Width) 
            { 
                if (field.SavannaField[placeInColumn, placeInRow].Type == 'E')
                {
                    field.SavannaField[placeInColumn, placeInRow] = animal;
                    field.SavannaField[line, character].Type = 'E';
                }
            }
        }

        /// <summary>
        /// Spawns animal in random place in savanna
        /// </summary>
        public void SpawnAnimal(Field field)
        {
            Random randomInt = new Random();

            do
            {
                int height = randomInt.Next(field.SavannaField.GetLength(0));
                int width = randomInt.Next(field.SavannaField.GetLength(1));

                if (field.SavannaField[height,width].Type == 'E')
                {
                    field.SavannaField[height, width] = animal;
                    break;
                }

            } while (true);
        }

        /// <summary>
        /// Checks if there is a need to spawn animal, if there is calls SpawnAnimal method and gives it the type of animal that is needed to be spawned
        /// </summary>
        public void CheckForAnimalSpawn(Field field)
        {
            ConsoleKey key = default;

            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
            }

            if (key == ConsoleKey.A)
            {
                animal = new Animal('A', false, 5, 3);
                SpawnAnimal(field);
            }

            if (key == ConsoleKey.L)
            {
                animal = new Animal('L', true, 3, 5);
                SpawnAnimal(field);
            }
        }

        /// <summary>
        /// Checks if animal can see any animal to attack/run away from
        /// </summary>
        public void CheckVision()
        {

        }
    }
}