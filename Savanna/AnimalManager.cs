﻿using Newtonsoft.Json;
using Savanna.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Savanna
{
    /// <summary>
    /// Class that manages all animal actions like moving, attacking, seeing, spawning and more.
    /// </summary>
    public class AnimalManager : IAnimalManager
    {
        private readonly Random RandomInt;

        /// <summary>
        /// Class that manages all animal actions like moving, attacking, seeing, spawning and more.
        /// </summary>
        public AnimalManager()
        {
            RandomInt = new Random();
        }

        /// <summary>
        /// Class that manages all animal actions like moving, attacking, seeing, spawning and more, used for unit tests
        /// </summary>
        /// <param name="randomInt">Random number generator used for mocking</param>
        public AnimalManager(Random randomInt)
        {
            RandomInt = randomInt;
        }

        /// <summary>
        /// Starts the moving process, chooses what type of moving will the animal do: normal, running away, attacking.
        /// </summary>
        /// <param name="field">Field that the animals move on</param>
        public void MoveAllAnimals(IField field)
        {
            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    if (field.SavannaField[line, character] != null)
                    {
                        if (!field.SavannaField[line, character].HasMoved)
                        {
                            CheckForSpecialMove(line, character, field);
                            RandomMove(line, character, field);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Normal moving way, where animal if it can moves randomly to one side
        /// </summary>
        /// <param name="line">Line where the animal is at</param>
        /// <param name="character">Character in line where the animal is at</param>
        /// <param name="field">Field that the animal moves on</param>
        private void RandomMove(int line, int character, IField field)
        {
            int side = RandomInt.Next(4);
            int newLine = line;
            int newCharacter = character;

            if (side == 0)
            {
                newLine--;
            }
            else if (side == 1)
            {
                newLine++;
            }
            else if (side == 2)
            {
                newCharacter--;
            }
            else if (side == 3)
            {
                newCharacter++;
            }

            if (newLine >= 0 && newLine < field.Height && newCharacter >= 0 && newCharacter < field.Width && field.SavannaField[line, character] != null)
            {
                if (field.SavannaField[newLine, newCharacter] == null && !field.SavannaField[line, character].HasMoved)
                {
                    field.SavannaField[newLine, newCharacter] = CreateAnimalCopy(field.SavannaField[line, character]);
                    field.SavannaField[newLine, newCharacter].HasMoved = true;

                    field.SavannaField[line, character] = null;
                }
            }
        }

        /// <summary>
        /// Spawns animal in random place in savanna
        /// </summary>
        /// <param name="field">Field where the animal will spawn on</param>
        /// <param name="animalName">Animal named used for creating the animal</param>
        private void SpawnAnimal(IField field, string animalName)
        {
            do
            {
                string animalTypeString = animalName + "s." + animalName + ", " + animalName;

                Type type = Type.GetType(animalTypeString);
                object o = Activator.CreateInstance(type);
                Animal animal = (Animal)o;

                int height = RandomInt.Next(field.SavannaField.GetLength(0));
                int width = RandomInt.Next(field.SavannaField.GetLength(1));

                if (field.SavannaField[height, width] == null)
                {
                    field.SavannaField[height, width] = animal;
                    break;
                }

            } while (true);
        }

        /// <summary>
        /// Checks if there is a need to spawn animal, if there is calls SpawnAnimal method and gives it the type of animal that is needed to be spawned
        /// </summary>
        /// <param name="field">Field that is given to SpawnAnimal method if animal spawning is needed</param>
        public void CheckForAnimalSpawn(IField field)
        {
            ConsoleKey key = default;

            GetFileInfo getFiles = new GetFileInfo();
            FileInfo[] Files = getFiles.GetDllFileInfo();

            do
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }

                foreach (FileInfo filePath in Files)
                {
                    string animalName = filePath.Name;
                    animalName = animalName.Remove(animalName.Length - 4);

                    if (key.ToString() == filePath.Name[0].ToString())
                    {
                        SpawnAnimal(field, animalName);
                    }
                }
            } while (Console.KeyAvailable);
        }

        /// <summary>
        /// Checks if AttackMove, RunAwayMove or SpecialAction happens
        /// </summary>
        /// <param name="line">Line where the animal is at</param>
        /// <param name="character">Character in line where the animal is at</param>
        /// <param name="field">Field that the animal moves on</param>
        private void CheckForSpecialMove(int line, int character, IField field)
        {
            int vision = field.SavannaField[line, character].VisionRange;

            for (int row = -vision; row < vision; row++)
            {
                for (int column = -vision; column < vision + 1; column++)
                {
                    int heightCheck = line + row;
                    int widthCheck = character + column;

                    if (heightCheck > -1 && heightCheck < field.Height && widthCheck > -1 && widthCheck < field.Width && field.SavannaField[heightCheck, widthCheck] != null && field.SavannaField[line, character] != null)
                    {
                        if (field.SavannaField[line, character].SpecialActionCooldown == 0)
                        {
                            field.SavannaField[line, character].SpecialAction(field, line, character, heightCheck, widthCheck);
                        }
                        else if (!field.SavannaField[heightCheck, widthCheck].CanAttack && field.SavannaField[line, character].CanAttack)
                        {
                            field.SavannaField[line, character].SpecialActionCooldown--;

                            AttackMove(field, line, character, heightCheck, widthCheck);
                            return;
                        }
                        else if (field.SavannaField[heightCheck, widthCheck].CanAttack && !field.SavannaField[line, character].CanAttack)
                        {
                            field.SavannaField[line, character].SpecialActionCooldown--;

                            RunAwayMove(field, line, character, heightCheck, widthCheck);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The animal is Moved closer to another animal it is attacking
        /// </summary>
        /// <param name="field">Object contains animal grid where array where the attack is calculated</param>
        /// <param name="attackerLine">Line where the attacking animal is at</param>
        /// <param name="attackerCharacter">Character in line where the attacking animal is at</param>
        /// <param name="animalSeenLine">Line where the attacked animal is at</param>
        /// <param name="animalSeenCharacter">Character in line where the attacked animal is at</param>
        private void AttackMove(IField field, int attackerLine, int attackerCharacter, int animalSeenLine, int animalSeenCharacter) 
        {
            int OriginalAttackerHeight = attackerLine;
            int OriginalAttackerWidth = attackerCharacter;

            if (attackerLine > animalSeenLine)
            {
                if (attackerCharacter > animalSeenCharacter)
                {
                    if(RandomInt.Next(2) == 0)
                    {
                        attackerLine--;
                    }
                    else
                    {
                        attackerCharacter--;
                    }
                }
                else if (attackerCharacter < animalSeenCharacter)
                {
                    if (RandomInt.Next(2) == 0)
                    {
                        attackerLine--;
                    }
                    else
                    {
                        attackerCharacter++;
                    }
                }
                else
                {
                    attackerLine--;
                }
            }
            else if (attackerLine < animalSeenLine)
            {
                if (attackerCharacter > animalSeenCharacter)
                {
                    if (RandomInt.Next(2) == 0)
                    {
                        attackerLine++;
                    }
                    else
                    {
                        attackerCharacter--;
                    }
                }
                else if (attackerCharacter < animalSeenCharacter)
                {
                    if (RandomInt.Next(2) == 0)
                    {
                        attackerLine++;
                    }
                    else
                    {
                        attackerCharacter++;
                    }
                }
                else
                {
                    attackerLine++;
                }
            }
            else
            {
                if (attackerCharacter > animalSeenCharacter)
                {
                    attackerCharacter--;
                }
                else
                {
                    attackerCharacter++;
                }
            }

            if (field.SavannaField[attackerLine,attackerCharacter] == null)
            {
                field.SavannaField[attackerLine, attackerCharacter] = CreateAnimalCopy(field.SavannaField[OriginalAttackerHeight, OriginalAttackerWidth]);
                field.SavannaField[attackerLine, attackerCharacter].HasMoved = true;

                field.SavannaField[OriginalAttackerHeight, OriginalAttackerWidth] = null;
            }
        }

        /// <summary>
        /// The animal is Moved farther away from another animal it is running away from
        /// </summary>
        /// <param name="field">Object contains animal grid where array where the attack is calculated</param>
        /// <param name="runnerLine">Line where the animal running away is at</param>
        /// <param name="runnerCharacter">Character in line where the animal running away is at</param>
        /// <param name="animalSeenLine">Line where the animal run from is at</param>
        /// <param name="animalSeenCharacter">Character in line where the animal run from is at</param>
        private void RunAwayMove(IField field, int runnerLine, int runnerCharacter, int animalSeenLine, int animalSeenCharacter)
        {
            int OriginalRunnerHeight = runnerLine;
            int OriginalRunnerWidth = runnerCharacter;

            if (runnerLine > animalSeenLine)
            {
                if (runnerCharacter > animalSeenCharacter)
                {
                    if (RandomInt.Next(2) == 0)
                    {
                        runnerLine++;
                    }
                    else
                    {
                        runnerCharacter++;
                    }
                }
                else if (runnerCharacter < animalSeenCharacter)
                {
                    if (RandomInt.Next(2) == 0)
                    {
                        runnerLine++;
                    }
                    else
                    {
                        runnerCharacter--;
                    }
                }
                else
                {
                    runnerLine++;
                }
            }
            else if (runnerLine < animalSeenLine)
            {
                if (runnerCharacter > animalSeenCharacter)
                {
                    if (RandomInt.Next(2) == 0)
                    {
                        runnerLine--;
                    }
                    else
                    {
                        runnerCharacter++;
                    }
                }
                else if (runnerCharacter < animalSeenCharacter)
                {
                    if (RandomInt.Next(2) == 0)
                    {
                        runnerLine--;
                    }
                    else
                    {
                        runnerCharacter--;
                    }
                }
                else
                {
                    runnerLine--;
                }
            }
            else
            {
                if (runnerCharacter > animalSeenCharacter)
                {
                    runnerCharacter++;
                }
                else
                {
                    runnerCharacter--;
                }
            }

            if (runnerLine > -1 && runnerLine < field.Height && runnerCharacter > -1 && runnerCharacter < field.Width)
            {
                if (field.SavannaField[runnerLine, runnerCharacter] == null)
                {
                    field.SavannaField[runnerLine, runnerCharacter] = CreateAnimalCopy(field.SavannaField[OriginalRunnerHeight, OriginalRunnerWidth]);
                    field.SavannaField[runnerLine, runnerCharacter].HasMoved = true;

                    field.SavannaField[OriginalRunnerHeight, OriginalRunnerWidth] = null;
                }
            }
        }

        /// <summary>
        /// Resets Animal array field.SavannaField property HasMoved to false for all array members
        /// </summary>
        /// <param name="field">Animal field where the property will be reset</param>
        public void AnimalReset(IField field)
        {
            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    if (field.SavannaField[line, character] != null)
                    {
                        field.SavannaField[line, character].HasMoved = false;
                    }
                }
            }
        }

        /// <summary>
        /// Checks each animal health, if it's 0 or below sets the animal type to 'E' or Empty
        /// </summary>
        /// <param name="field">Animal field where the animal heath is checked and dead animals are set</param>
        public void SetIfAnimalDead(IField field)
        {
            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    if (field.SavannaField[line, character] != null)
                    {
                        field.SavannaField[line, character].Health -= 0.5;
                        field.SavannaField[line, character].Health -= CheckForOverPopulation(line, character, field);

                        if (field.SavannaField[line, character].Health <= 0)
                        {
                            field.SavannaField[line, character] = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns how much health will be removed from the animal, if there are animals all around it
        /// </summary>
        /// <param name="line">Line where the animal is at</param>
        /// <param name="character">Character in Line where the animal is at</param>
        /// <param name="field">Field object that includes the animal array that has the animals on it</param>
        private double CheckForOverPopulation(int line, int character, IField field)
        {
            double healthTakenAway = 0;

            if (line > 0 && line + 1 < field.Height && character > 0 && character + 1 < field.Width)
            {
                if (field.SavannaField[line + 1, character] != null && field.SavannaField[line - 1, character] != null && field.SavannaField[line, character + 1] != null && field.SavannaField[line, character - 1] != null)
                {
                    healthTakenAway = 4;

                    if (field.SavannaField[line + 1, character + 1] != null)
                    {
                        healthTakenAway += 1.75;
                    }
                    else if (field.SavannaField[line + 1, character - 1] != null)
                    {
                        healthTakenAway += 1.75;
                    }
                    else if (field.SavannaField[line - 1, character + 1] != null)
                    {
                        healthTakenAway += 1.75;
                    }
                    else if (field.SavannaField[line - 1, character - 1] != null)
                    {
                        healthTakenAway += 1.75;
                    }
                }
            }
            return healthTakenAway;
        }

        /// <summary>
        /// Check if animal is eaten or it eats someone, if animal is eaten it's dead and the eater gets extra health
        /// </summary>
        /// <param name="line">Line where the animal is at</param>
        /// <param name="character">Character in Line where the animal is at</param>
        /// <param name="field">Animal field where the animals are on</param>
        private void EatAnimalIfCan(int line, int character, IField field)
        {
            int eatRange = 1;

            for (int row = -eatRange; row < eatRange; row++)
            {
                for (int column = -eatRange; column < eatRange; column++)
                {
                    int heightCheck = line + row;
                    int widthCheck = character + column;

                    if (heightCheck > -1 && heightCheck < field.Height && widthCheck > -1 && widthCheck < field.Width && field.SavannaField[line, character] != null && field.SavannaField[heightCheck, widthCheck] != null)
                    {
                        if (!field.SavannaField[heightCheck, widthCheck].CanAttack && field.SavannaField[line, character].CanAttack)
                        {
                            field.SavannaField[heightCheck, widthCheck] = null;
                            field.SavannaField[line, character].Health += 4.5;
                            field.SavannaField[line, character].HasMoved = true;
                            return;
                        }
                        else if (field.SavannaField[heightCheck, widthCheck].CanAttack && !field.SavannaField[line, character].CanAttack)
                        {
                            field.SavannaField[line, character] = null;
                            field.SavannaField[heightCheck, widthCheck].Health += 4.5;
                            field.SavannaField[heightCheck, widthCheck].HasMoved = true;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if animal needs to be born. If it does calls method that spawns animal nearby
        /// </summary>
        /// <param name="firstLine">Line where the first animal is at</param>
        /// <param name="firstCharacter">Character in Line where the first animal is at</param>
        /// <param name="secondLine">Line where the second animal found near is at</param>
        /// <param name="secondCharacter">Character in Line where the second animal found near is at</param>
        /// <param name="field">Field object that includes the animal array that has the animals on it</param>
        private void CheckIfNewAnimalNeedsToSpawn(int firstLine, int firstCharacter, int secondLine, int secondCharacter, IField field)
        {
            if (!field.SavannaField[firstLine, firstCharacter].PartnerIds.ContainsKey(field.SavannaField[secondLine, secondCharacter].ID))
            {
                field.SavannaField[firstLine, firstCharacter].PartnerIds.Add(field.SavannaField[secondLine, secondCharacter].ID, 0);
            }

            field.SavannaField[firstLine, firstCharacter].PartnerIds[field.SavannaField[secondLine, secondCharacter].ID]++;

            if (field.SavannaField[firstLine, firstCharacter].PartnerIds[field.SavannaField[secondLine, secondCharacter].ID] == 3)
            {
                SpawnAnimalNearby(firstLine, firstCharacter, field);

                field.SavannaField[firstLine, firstCharacter].PartnerIds.Clear();
                field.SavannaField[secondLine, secondCharacter].PartnerIds.Clear();
            }
        }

        /// <summary>
        /// Spawns animal nearby the given animal location
        /// </summary>
        /// <param name="line">Line where the animal is going to be spawned close to</param>
        /// <param name="character">Character in Line where the animal is going to be spawned close to</param>
        /// <param name="field">Field object that includes the animal array that has the animals on it</param>
        private void SpawnAnimalNearby(int line, int character, IField field)
        {
            bool animalSpawned = false;
            int placeInColumn = line;
            int placeInRow = character;

            do
            {
                int side = RandomInt.Next(5);

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
                else
                {
                    break;
                }

                if (placeInColumn >= 0 && placeInColumn < field.Height && placeInRow >= 0 && placeInRow < field.Width && field.SavannaField[line, character] != null)
                {
                    field.SavannaField[placeInColumn, placeInRow] = (Animal)Activator.CreateInstance(field.SavannaField[line, character].GetType());
                    animalSpawned = true;
                }
            } while (!animalSpawned);
        }

        /// <summary>
        /// Searches for animals in animal array that is in field object. If finds an animal calls methods that check if an animal will be eaten or a new one born
        /// </summary>
        /// <param name="field">Field object that includes the animal array that is the Savanna game field</param>
        public void SearchForAnimals(IField field)
        {
            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    if (field.SavannaField[line, character] != null)
                    {
                        EatAnimalIfCan(line, character, field);

                        if (field.SavannaField[line, character] != null)
                        {
                            Dictionary<int, int> dictionaryCopy = CreateDictionaryCopy(field.SavannaField[line, character].PartnerIds);
                            SearchForAnimalsThatAreClose(line, character, field);
                            DeleteAnimalsFromDictionary(line, character, field, dictionaryCopy);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Is given animal posision, checks around animal if it's near the same kind of animal.If finds animal calls functions 
        /// </summary>
        /// <param name="line">Line where the animal is at</param>
        /// <param name="character">Character in Line where the animal is at</param>
        /// <param name="field">Field object that includes the animal array that has the animals on it</param>
        private void SearchForAnimalsThatAreClose(int line, int character, IField field)
        {
            int partnerRange = 1;

            for (int row = -partnerRange; row < partnerRange; row++)
            {
                for (int column = -partnerRange; column < partnerRange; column++)
                {
                    int heightCheck = line + row;
                    int widthCheck = character + column;

                    if (heightCheck > -1 && heightCheck < field.Height && widthCheck > -1 && widthCheck < field.Width && field.SavannaField[line, character] != null && field.SavannaField[heightCheck, widthCheck] != null)
                    {
                        CheckIfAnimalsAreTheSameType(line, character, field, heightCheck, widthCheck);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the two animals are the same type, if they are calls method that checks if new animal should spawn
        /// </summary>
        /// <param name="firstLine">Line where the first animal is at</param>
        /// <param name="firstCharacter">Character in Line where the first animal is at</param>
        /// <param name="field">Field object that includes the animal array that has the animals on it</param>
        /// <param name="secondLine">Line where the second animal is at</param>
        /// <param name="secondCharacter">Character in Line where the second animal is at</param>
        private void CheckIfAnimalsAreTheSameType(int firstLine, int firstCharacter, IField field, int secondLine, int secondCharacter)
        {
            if (field.SavannaField[secondLine, secondCharacter].Type == field.SavannaField[firstLine, firstCharacter].Type)
            {
                if (firstLine != secondLine || firstCharacter != secondCharacter)
                {
                    CheckIfNewAnimalNeedsToSpawn(firstLine, firstCharacter, secondLine, secondCharacter, field);
                }
            }
        }

        /// <summary>
        /// Creates a copy of an Animal object and returns it
        /// </summary>
        /// <param name="animal">Animal that will be copied</param>
        private dynamic CreateAnimalCopy(Animal animal)
        {
            var animalCopy = JsonConvert.SerializeObject(animal);
            object newAnimal = JsonConvert.DeserializeObject(animalCopy, animal.GetType());
            
            return newAnimal;
        }

        /// <summary>
        /// Creates a copy of a Dictionary and returns it
        /// </summary>
        /// <param name="dictionary">Dictionary that will be copied</param>
        private Dictionary<int, int> CreateDictionaryCopy(Dictionary<int, int> dictionary)
        {
            var DictionaryCopy = JsonConvert.SerializeObject(dictionary);
            var newDictionary = JsonConvert.DeserializeObject<Dictionary<int, int>>(DictionaryCopy);

            return newDictionary;
        }

        /// <summary>
        /// Checks animal partnerIDs dictionary with an older copy if animals have been close together, if not they get removed from the list 
        /// </summary>
        /// <param name="line">Line where the animal is at</param>
        /// <param name="character">Character in Line where the animal is at</param>
        /// <param name="field">Field object that includes the animal array that has the animals on it</param>
        /// <param name="dictionary">Older copy of the dictionary that was made before the last changes</param>
        private void DeleteAnimalsFromDictionary(int line, int character, IField field, Dictionary<int, int> dictionary)
        {
            foreach (var item in dictionary)
            {
                if (field.SavannaField[line, character].PartnerIds.ContainsKey(item.Key))
                {
                    if (field.SavannaField[line, character].PartnerIds[item.Key] == dictionary[item.Key])
                    {
                        field.SavannaField[line, character].PartnerIds.Remove(item.Key);
                    }
                }
            }
        }
    }
}