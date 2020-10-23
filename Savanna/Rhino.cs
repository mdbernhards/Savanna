using Newtonsoft.Json;
using System;

namespace Savanna
{
    /// <summary>
    /// Class that stores information about a Rhino
    /// </summary>
    public class Rhino : Animal
    {
        /// <summary>
        /// Class that stores information about an Rhino, creates an Rhino
        /// </summary>
        public Rhino()
        {
            Random randomInt = new Random();

            Type = 'R';
            CanAttack = false;
            VisionRange = 12;
            Health = 30;

            ID = randomInt.Next(100000, 999999);
            HasMoved = false;
        }

        /// <summary>
        /// Rhino special action, runs up to animal that can attack and pushes it away
        /// </summary>
        /// <param name="field">Object contains animal grid where array where the attack is calculated</param>
        /// <param name="runnerLine">Line where the Rhino is at</param>
        /// <param name="runnerCharacter">Character in line where the Rhino is at</param>
        /// <param name="animalSeenLine">Line where the aggressive animal is at</param>
        /// <param name="animalSeenCharacter">Character in line where the aggressive animal is at</param>
        public override void SpecialAction(Field field, int runnerLine, int runnerCharacter, int animalSeenLine, int animalSeenCharacter)
        {
            if (field.SavannaField[animalSeenLine, animalSeenCharacter].CanAttack == true)
            {
                SpecialActionCooldown += 5;

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
                        var newAnimal = JsonConvert.DeserializeObject<Rhino>(animalCopy);

                        field.SavannaField[runnerLine, runnerCharacter] = newAnimal;
                        field.SavannaField[runnerLine, runnerCharacter].HasMoved = true;
                        field.SavannaField[OriginalAttackerHeight, OriginalAttackerWidth] = null;
                    }
                }
            }
        }
    }
}
