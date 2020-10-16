using Newtonsoft.Json;
using System;

namespace Savanna
{
    public class AnimalManager
    {
        private Animal animal;

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

        public void CheckVision()
        {

        }
    }
}