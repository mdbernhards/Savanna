using System;
using System.Timers;

namespace Savanna
{
    /// <summary>
    /// Class that manages evrything about the Savanna field, like: field SetUp and timed field update
    /// </summary>
    public class FieldManager
    {
        private AnimalManager animalManager;
        private Field field;
        private readonly UI ui;
        private static Timer UpdateTimer;

        /// <summary>
        /// Class that manages evrything about the Savanna field, like: field SetUp and timed field update
        /// </summary>
        public FieldManager()
        {
            animalManager = new AnimalManager();
            field = new Field(40, 100);
            ui = new UI();
        }

        /// <summary>
        /// Sets up field to be used by game, starts the updateTimer
        /// </summary>
        public void SetUpField()
        {
            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    field.SavannaField[line, character] = new Animal();
                }
            }

            SetFieldUpdateTimer();
        }

        /// <summary>
        /// Sets up a timer that fires every half a second
        /// </summary>
        public void SetFieldUpdateTimer()
        {
            UpdateTimer = new Timer(800);
            UpdateTimer.Elapsed += FieldUpdate;
            UpdateTimer.AutoReset = true;
            UpdateTimer.Enabled = true;

            //Infinite loop so the program doesn't stop
            while (true) { }
        }

        /// <summary>
        /// Updates the field, if any changes happen like animals added, animals move. Calls the method that draws everything
        /// </summary>
        /// <param name="e">Used by timer</param>
        /// <param name="source">Used by timer</param>
        public void FieldUpdate(Object source, ElapsedEventArgs e)
        {
            animalManager.CheckForAnimalSpawn(field);
            ui.DrawField(field);
            animalManager.AllAnimalsMove(field);
            animalManager.AnimalReset(field);
        }
    }
}