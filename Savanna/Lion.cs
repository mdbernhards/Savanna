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
            VisionRange = 18;
            Health = 10;

            ID = randomInt.Next(100000, 999999);
            HasMoved = false;
            Alive = true;
        }

        /// <summary>
        /// Lion special action
        /// </summary>
        public override void SpecialAction()
        {

        }
    }
}