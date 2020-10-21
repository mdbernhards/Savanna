﻿using Newtonsoft.Json;
using System;

namespace Savanna
{
    /// <summary>
    /// Class that stores information about a Lion
    /// </summary>
    public class Lion : Animal
    {
        /// <summary>
        /// Class that stores information about a Lion, creates a Lion
        /// </summary>
        public Lion()
        {
            Random randomInt = new Random();

            Type = 'L';
            CanAttack = true;
            VisionRange = 7;
            Health = 12;

            ID = randomInt.Next(100000, 999999);
            HasMoved = false;
            Alive = true;
        }

        /// <summary>
        /// Lion special action, Jumps on the same Y or X axis that the animal beeing attacked is
        /// </summary>
        /// <param name="field">Object contains animal grid where array where the attack is calculated</param>
        /// <param name="attackerLine">Line where the attacking animal is at</param>
        /// <param name="attackerCharacter">Character in line where the attacking animal is at</param>
        /// <param name="animalSeenLine">Line where the attacked animal is at</param>
        /// <param name="animalSeenCharacter">Character in line where the attacked animal is at</param>
        public override void SpecialAction(Field field, int attackerLine, int attackerCharacter, int animalSeenLine, int animalSeenCharacter)
        {
            if (field.SavannaField[animalSeenLine, animalSeenCharacter].CanAttack == false)
            {
                SpecialActionCooldown += 7;

                Random randomInt = new Random();

                int OriginalAttackerHeight = attackerLine;
                int OriginalAttackerWidth = attackerCharacter;

                if (attackerLine == animalSeenLine)
                {
                    if (attackerCharacter > animalSeenCharacter)
                    {
                        attackerCharacter = animalSeenCharacter + 1;
                    }
                    else
                    {
                        attackerCharacter = animalSeenCharacter - 1;
                    }
                }
                else if (attackerCharacter == animalSeenCharacter)
                {
                    if (attackerLine > animalSeenLine)
                    {
                        attackerLine = animalSeenLine + 1;
                    }
                    else
                    {
                        attackerLine = animalSeenLine - 1;
                    }
                }
                else
                {
                    if (randomInt.Next(2) == 0)
                    {
                        attackerCharacter = animalSeenCharacter;
                    }
                    else
                    {
                        attackerLine = animalSeenLine;
                    }
                }
                if (attackerLine > -1 && attackerLine < field.Height && attackerCharacter > -1 && attackerCharacter < field.Width)
                {
                    if (field.SavannaField[attackerLine, attackerCharacter] == null)
                    {
                        var animalCopy = JsonConvert.SerializeObject(field.SavannaField[OriginalAttackerHeight, OriginalAttackerWidth]);
                        var newAnimal = JsonConvert.DeserializeObject<Lion>(animalCopy);

                        field.SavannaField[attackerLine, attackerCharacter] = newAnimal;
                        field.SavannaField[attackerLine, attackerCharacter].HasMoved = true;

                        field.SavannaField[OriginalAttackerHeight, OriginalAttackerWidth] = null;
                    }
                }
            }
        }
    }
}