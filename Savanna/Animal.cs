using System.Collections.Generic;

namespace Savanna
{
    /// <summary>
    /// Class that stores information about an animal
    /// </summary>
    public class Animal
    {
        /// <summary>
        /// Type of animal, first letter of the animals name
        /// </summary>
        public char Type { get; set; } = 'E';

        /// <summary>
        /// If the animal can attack and eat other animals
        /// </summary>
        public bool CanAttack { get; set; }

        /// <summary>
        /// How far can the animal see
        /// </summary>
        public int VisionRange { get; set; }

        /// <summary>
        /// True if animal has moved this turn, false if not. Resets to false every turn
        /// </summary>
        public bool HasMoved { get; set; } = true;

        /// <summary>
        /// Animal health, if 0 or below animal is dead
        /// </summary>
        public double Health { get; set; }

        /// <summary>
        /// ID to identify select animal
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Dictionary of saved IDs of partners and how many times they have been next to eachother
        /// </summary>
        public Dictionary<int, int> PartnerIds { get; set; } = new Dictionary<int, int>();

        /// <summary>
        /// True if animal alive, false if dead
        /// </summary>
        public bool Alive { get; set; } = false;

        /// <summary>
        /// Cooldown for the special action, so that it can be done only every fifth time attacking or defending
        /// </summary>
        public int SpecialActionCooldown { get; set; } = 5;

        /// <summary>
        /// Virtual method for special action made for beeing overriden
        /// </summary>
        public virtual void SpecialAction(Field field, int firstLine, int firstCharacter, int secondLine, int secondCharacter)
        {

        }
    }
}