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
        /// True if animal has moved this turn, false if not. Resets to false every turn
        /// </summary>
        public bool HasMoved { get; set; }

        /// <summary>
        /// Animal health, if 0 or below animal is dead
        /// </summary>
        public double Health { get; set; }

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
        /// <param name="type">Animal type being created</param>
        /// <param name="canAttack">Bool statement, if true can attack</param>
        /// <param name="visionRange">How far can the animal see</param>
        /// <param name="health"> animal starting health</param>
        public Animal(char type, bool canAttack, int visionRange, double health)
        {
            Type = type;
            CanAttack = canAttack;
            VisionRange = visionRange;
            Health = health;

            HasMoved = false;
        }
    }
}