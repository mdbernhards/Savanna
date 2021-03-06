﻿namespace Savanna
{
    /// <summary>
    /// Main program class, starts the program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the program, by creating a gameField object
        /// </summary>
        public static void Main(string[] args)
        {
            new FieldManager(new AnimalManager(), new UI(), new Field(40, 100));
        }
    }
}