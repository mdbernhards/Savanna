using System;
using System.Collections.Generic;
using System.Text;

namespace Savanna
{
    public class Animal
    {
        public char Type { get; set; }
        public bool CanAttack { get; set; }
        public int VisionRange { get; set; }
        public int WalkRange { get; set; }


        public Animal()
        {

        }

        public Animal(char type, bool canAttack, int visionRange, int walkRange)
        {
            Type = type;
            CanAttack = canAttack;
            VisionRange = visionRange;
            WalkRange = walkRange;
        }
    }
}