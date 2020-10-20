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
            VisionRange = 10;
            Health = 25;

            ID = randomInt.Next(100000, 999999);
            HasMoved = false;
            Alive = true;
        }

        /// <summary>
        /// Antelope special action
        /// </summary>
        public override void SpecialAction()
        {

        }
    }
}