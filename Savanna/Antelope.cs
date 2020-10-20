using Newtonsoft.Json;
using System;

namespace Savanna
{
    /// <summary>
    /// Class that stores information about an Antelope
    /// </summary>
    public class Antelope : Animal
    {
        /// <summary>
        /// Class that stores information about an Antelope, creates an Antelope
        /// </summary>
        public Antelope()
        {
            Random randomInt = new Random();

            Type = 'A';
            CanAttack = false;
            VisionRange = 7;
            Health = 25;

            ID = randomInt.Next(100000, 999999);
            HasMoved = false;
            Alive = true;
        }

        /// <summary>
        /// Antelope special action, runs away the same amount of cells the animalSeen is away
        /// </summary>
        /// <param name="field">Object contains animal grid where array where the attack is calculated</param>
        /// <param name="runnerLine">Line where the animal running away is at</param>
        /// <param name="runnerCharacter">Character in line where the animal running away is at</param>
        /// <param name="animalSeenLine">Line where the animal run from is at</param>
        /// <param name="animalSeenCharacter">Character in line where the animal run from is at</param>
        public override void SpecialAction(Field field, int runnerLine, int runnerCharacter, int animalSeenLine, int animalSeenCharacter)
        {
            if (field.SavannaField[animalSeenLine, animalSeenCharacter].CanAttack == false)
            {
                SpecialActionCooldown += 7;

                Random randomInt = new Random();

                int OriginalAttackerHeight = runnerLine;
                int OriginalAttackerWidth = runnerCharacter;

                if (runnerLine == animalSeenLine)
                {
                    runnerCharacter -= (animalSeenCharacter - runnerCharacter);
                }
                else if (runnerCharacter == animalSeenCharacter)
                {
                    runnerLine -= (animalSeenLine - runnerLine);
                }
                else
                {
                    if (randomInt.Next(2) == 0)
                    {
                        runnerLine -= (animalSeenLine - runnerLine);
                    }
                    else
                    {
                        runnerCharacter -= (animalSeenCharacter - runnerCharacter);
                    }
                }
                if (runnerLine > -1 && runnerLine < field.Height && runnerCharacter > -1 && runnerCharacter < field.Width)
                {
                    if (field.SavannaField[runnerLine, runnerCharacter] == null)
                    {
                        var animalCopy = JsonConvert.SerializeObject(field.SavannaField[OriginalAttackerHeight, OriginalAttackerWidth]);
                        var newAnimal = JsonConvert.DeserializeObject<Antelope>(animalCopy);

                        field.SavannaField[runnerLine, runnerCharacter] = newAnimal;
                        field.SavannaField[runnerLine, runnerCharacter].HasMoved = true;
                        field.SavannaField[OriginalAttackerHeight, OriginalAttackerWidth] = null;
                    }
                }
            }
        }
    }
}