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
        public char Type { get; set; }

        /// <summary>
        /// If the animal can attack and eat other animals
        /// </summary>
        public bool CanAttack { get; set; }

        /// <summary>
        /// How far can the animal see
        /// </summary>
        public int VisionRange { get; set; }

        /// <summary>
        /// How far can the animal walk in one turn, prob remove
        /// </summary>
        public int WalkRange { get; set; }

        /// <summary>
        /// Class that stores information about an animal, creates empty animal
        /// </summary>
        public Animal()
        {
            Type = 'E';
        }

        /// <summary>
        /// Class that stores information about an animal, creates animal
        /// </summary>
        public Animal(char type, bool canAttack, int visionRange, int walkRange)
        {
            Type = type;
            CanAttack = canAttack;
            VisionRange = visionRange;
            WalkRange = walkRange;
        }
    }
}