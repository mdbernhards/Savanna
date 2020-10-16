﻿using Newtonsoft.Json;
using System;

namespace Savanna
{
    public class AnimalManager
    {
        private Animal animal;


        public void Move(Field field) 
        {
            Random randomInt = new Random();
            animal = new Animal();

            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    if(field.SavannaField[line,character].Type != 'E')
                    {
                        var objectCopy = JsonConvert.SerializeObject(field.SavannaField[line, character]);
                        animal = JsonConvert.DeserializeObject<Animal>(objectCopy);


                        int side = randomInt.Next(4);

                        if(side == 0 &&  line - 1 >= 0)
                        {
                            if (field.SavannaField[line - 1, character].Type == 'E')
                            {
                                field.SavannaField[line - 1, character] = animal;
                                field.SavannaField[line, character].Type = 'E';
                            }
                        }
                        else if (side == 1 && line + 1 < field.Height)
                        {
                            if (field.SavannaField[line + 1, character].Type == 'E')
                            {
                                field.SavannaField[line + 1, character] = animal;
                                field.SavannaField[line, character].Type = 'E';
                            }
                        }
                        else if (side == 2 && character - 1 >= 0)
                        {
                            if (field.SavannaField[line, character - 1].Type == 'E')
                            {
                                field.SavannaField[line, character - 1] = animal;
                                field.SavannaField[line, character].Type = 'E';
                            }
                        }
                        else if (side == 3 && character + 1 < field.Width)
                        {
                            if (field.SavannaField[line, character + 1].Type == 'E')
                            {
                                field.SavannaField[line, character + 1] = animal;
                                field.SavannaField[line, character].Type = 'E';
                            }
                        }
                    }
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
    }
}