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
        /// <param name="field">Field that the animals move on</param>
        public void TryToMoveAllAnimals(Field field)
        {
            animal = new Animal();

            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    if (field.SavannaField[line, character].Type != 'E' && !field.SavannaField[line, character].HasMoved)
                    {
                        var objectCopy = JsonConvert.SerializeObject(field.SavannaField[line, character]);
                        animal = JsonConvert.DeserializeObject<Animal>(objectCopy);

                        EatAnimalIfCan(line, character, field);
                        CheckIfSameAnimalSpeciesAreClose(line, character, field);
                        CheckVision(line, character, field);

                        if (!field.SavannaField[line, character].HasMoved)
                        {
                            MoveToSide(line, character, field);
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
                    field.SavannaField[placeInColumn, placeInRow].HasMoved = true;

                    field.SavannaField[line, character] = new Animal();
                }
            }
        }

        /// <summary>
        /// Spawns animal in random place in savanna
        /// </summary>
        /// <param name="field">Field where the animal will spawn on</param>
        public void SpawnAnimal(Field field)
        {
            Random randomInt = new Random();

            do
            {
                int height = randomInt.Next(field.SavannaField.GetLength(0));
                int width = randomInt.Next(field.SavannaField.GetLength(1));

                if (field.SavannaField[height, width].Type == 'E')
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
        public void CheckForAnimalSpawn(Field field)
        {
            ConsoleKey key = default;

            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
            }

            if (key == ConsoleKey.A)
            {
                animal = new Animal('A', false, 5, 100);
                SpawnAnimal(field);
            }

            if (key == ConsoleKey.L)
            {
                animal = new Animal('L', true, 10, 100);
                SpawnAnimal(field);
            }
        }

        /// <summary>
        /// Checks if animal can see any animal to attack/run away from
        /// </summary>
        /// <param name="line">Line where the animal is at</param>
        /// <param name="character">Character in line where the animal is at</param>
        /// <param name="field">Field that the animal moves on</param>
        public void CheckVision(int line, int character, Field field)
        {
            int vision = field.SavannaField[line, character].VisionRange;

            for (int row = -vision; row < vision; row++)
            {
                for (int column = -vision; column < vision; column++)
                {
                    int heightCheck = line + row;
                    int widthCheck = character + column;

                    if (heightCheck > -1 && heightCheck < field.Height && widthCheck > -1 && widthCheck < field.Width)
                    {
                        if (field.SavannaField[heightCheck, widthCheck].Type == 'A' && field.SavannaField[line, character].Type == 'L')
                        {
                            AttackMove(field, line, character, heightCheck, widthCheck);
                            return;
                        }
                        else if (field.SavannaField[heightCheck, widthCheck].Type == 'L' && field.SavannaField[line, character].Type == 'A')
                        {
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
        /// <param name="AttackerLine">Line where the attacking animal is at</param>
        /// <param name="AttackerCharacter">Character in line where the attacking animal is at</param>
        /// <param name="AnimalSeenLine">Line where the attacked animal is at</param>
        /// <param name="AnimalSeenCharacter">Character in line where the attacked animal is at</param>
        public void AttackMove(Field field, int AttackerLine, int AttackerCharacter, int AnimalSeenLine, int AnimalSeenCharacter) 
        {
            Random randomInt = new Random();

            int OriginalAttackerHeight = AttackerLine;
            int OriginalAttackerWidth = AttackerCharacter;

            if (AttackerLine > AnimalSeenLine)
            {
                if(AttackerCharacter > AnimalSeenCharacter)
                {
                    if(randomInt.Next(2) == 0)
                    {
                        AttackerLine--;
                    }
                    else
                    {
                        AttackerCharacter--;
                    }
                }
                else if (AttackerCharacter < AnimalSeenCharacter)
                {
                    if (randomInt.Next(2) == 0)
                    {
                        AttackerLine--;
                    }
                    else
                    {
                        AttackerCharacter++;
                    }
                }
                else
                {
                    AttackerLine--;
                }
            }
            else if (AttackerLine < AnimalSeenLine)
            {
                if (AttackerCharacter > AnimalSeenCharacter)
                {
                    if (randomInt.Next(2) == 0)
                    {
                        AttackerLine++;
                    }
                    else
                    {
                        AttackerCharacter--;
                    }
                }
                else if (AttackerCharacter < AnimalSeenCharacter)
                {
                    if (randomInt.Next(2) == 0)
                    {
                        AttackerLine++;
                    }
                    else
                    {
                        AttackerCharacter++;
                    }
                }
                else
                {
                    AttackerLine++;
                }
            }
            else
            {
                if (AttackerCharacter > AnimalSeenCharacter)
                {
                    AttackerCharacter--;
                }
                else
                {
                    AttackerCharacter++;
                }
            }

            if(field.SavannaField[AttackerLine,AttackerCharacter].Type == 'E')
            {
                field.SavannaField[OriginalAttackerHeight, OriginalAttackerWidth] = new Animal();

                field.SavannaField[AttackerLine, AttackerCharacter] = animal;
                field.SavannaField[AttackerLine, AttackerCharacter].HasMoved = true;
            }
        }

        /// <summary>
        /// The animal is Moved farther away from another animal it is running away from
        /// </summary>
        /// <param name="field">Object contains animal grid where array where the attack is calculated</param>
        /// <param name="RunnerLine">Line where the animal running away is at</param>
        /// <param name="RunnerCharacter">Character in line where the animal running away is at</param>
        /// <param name="AnimalSeenLine">Line where the animal run from is at</param>
        /// <param name="AnimalSeenCharacter">Character in line where the animal run from is at</param>
        public void RunAwayMove(Field field, int RunnerLine, int RunnerCharacter, int AnimalSeenLine, int AnimalSeenCharacter)
        {
            Random randomInt = new Random();  

            int OriginalRunnerHeight = RunnerLine;
            int OriginalRunnerWidth = RunnerCharacter;

            if (RunnerLine > AnimalSeenLine)
            {
                if (RunnerCharacter > AnimalSeenCharacter)
                {
                    if (randomInt.Next(2) == 0)
                    {
                        RunnerLine++;
                    }
                    else
                    {
                        RunnerCharacter++;
                    }
                }
                else if (RunnerCharacter < AnimalSeenCharacter)
                {
                    if (randomInt.Next(2) == 0)
                    {
                        RunnerLine++;
                    }
                    else
                    {
                        RunnerCharacter--;
                    }
                }
                else
                {
                    RunnerLine++;
                }
            }
            else if (RunnerLine < AnimalSeenLine)
            {
                if (RunnerCharacter > AnimalSeenCharacter)
                {
                    if (randomInt.Next(2) == 0)
                    {
                        RunnerLine--;
                    }
                    else
                    {
                        RunnerCharacter++;
                    }
                }
                else if (RunnerCharacter < AnimalSeenCharacter)
                {
                    if (randomInt.Next(2) == 0)
                    {
                        RunnerLine--;
                    }
                    else
                    {
                        RunnerCharacter--;
                    }
                }
                else
                {
                    RunnerLine--;
                }
            }
            else
            {
                if (RunnerCharacter > AnimalSeenCharacter)
                {
                    RunnerCharacter++;
                }
                else
                {
                    RunnerCharacter--;
                }
            }

            if (RunnerLine > -1 && RunnerLine < field.Height && RunnerCharacter > -1 && RunnerCharacter < field.Width)
            {
                if (field.SavannaField[RunnerLine, RunnerCharacter].Type == 'E')
                {
                    field.SavannaField[OriginalRunnerHeight, OriginalRunnerWidth] = new Animal();

                    field.SavannaField[RunnerLine, RunnerCharacter] = animal;
                    field.SavannaField[RunnerLine, RunnerCharacter].HasMoved = true;
                }
            }
        }

        /// <summary>
        /// Resets Animal array field.SavannaField property HasMoved to false for all array members
        /// </summary>
        /// <param name="field">Animal field where the property will be reset</param>
        public void AnimalReset(Field field)
        {
            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    field.SavannaField[line, character].HasMoved = false;
                }
            }
        }

        /// <summary>
        /// Checks each animal health, if it's 0 or below sets the animal type to 'E' or Empty
        /// </summary>
        /// <param name="field">Animal field where the animal heath is checked and dead animals are set</param>
        public void SetIfAnimalDead(Field field)
        {
            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    field.SavannaField[line, character].Health -= 0.5;

                    if(field.SavannaField[line, character].Health <= 0)
                    {
                        field.SavannaField[line, character] = new Animal();
                    }
                }
            }
        }

        /// <summary>
        /// Check if animal is eaten or it eats someone, if animal is eaten it's dead and the eater gets extra health
        /// </summary>
        /// <param name="line">Line where the animal is at</param>
        /// <param name="character">Character in Line where the animal is at</param>
        /// <param name="field">Animal field where the animals are on</param>
        public void EatAnimalIfCan(int line, int character, Field field)
        {
            int eatRange = 1;

            for (int row = -eatRange; row < eatRange; row++)
            {
                for (int column = -eatRange; column < eatRange; column++)
                {
                    int heightCheck = line + row;
                    int widthCheck = character + column;

                    if (heightCheck > -1 && heightCheck < field.Height && widthCheck > -1 && widthCheck < field.Width)
                    {
                        if (field.SavannaField[heightCheck, widthCheck].Type == 'A' && field.SavannaField[line, character].Type == 'L')
                        {
                            field.SavannaField[heightCheck, widthCheck] = new Animal();
                            field.SavannaField[line, character].Health += 7;
                            field.SavannaField[line, character].HasMoved = true;
                            return;
                        }
                        else if (field.SavannaField[heightCheck, widthCheck].Type == 'L' && field.SavannaField[line, character].Type == 'A')
                        {
                            field.SavannaField[line, character] = new Animal();
                            field.SavannaField[heightCheck, widthCheck].Health += 7;
                            field.SavannaField[heightCheck, widthCheck].HasMoved = true;
                            return;
                        }
                    }
                }
            }
        }

        public void CheckIfSameAnimalSpeciesAreClose(int line, int character, Field field)
        {
            int partnerRange = 2;

            for (int row = -partnerRange; row < partnerRange; row++)
            {
                for (int column = -partnerRange; column < partnerRange; column++)
                {
                    int heightCheck = line + row;
                    int widthCheck = character + column;

                    if (heightCheck > -1 && heightCheck < field.Height && widthCheck > -1 && widthCheck < field.Width)
                    {
                        if (field.SavannaField[heightCheck, widthCheck].Type == field.SavannaField[line, character].Type)
                        {
                            CheckIfNewAnimalNeedsToSpawn(line, character, row, column, field);
                        }
                    }
                }
            }
        }

        public void CheckIfNewAnimalNeedsToSpawn(int originalLine, int originalCharacter, int foundLine, int foundCharacter, Field field)
        {
            string[,] localPartnerIds = new string[25, 2];
            Array.Copy(field.SavannaField[originalLine, originalCharacter].PartnerIDs, localPartnerIds, field.SavannaField[originalLine, originalCharacter].PartnerIDs.Length);

            for (int id = 0; id < 25; id++)
            {
                if( field.SavannaField[originalLine, originalCharacter].PartnerIDs[id, 0] == field.SavannaField[foundLine, foundCharacter].ID)
                {
                    field.SavannaField[originalLine, originalCharacter].PartnerIDs[id, 1]++;

                    if (field.SavannaField[originalLine, originalCharacter].PartnerIDs[id, 1] == 3)
                    {
                        SpawnAnimalNearby(originalLine, originalCharacter, field);
                    }
                }
            }
        }

        public void SpawnAnimalNearby(int line, int character, Field field)
        {

        }
    }
}