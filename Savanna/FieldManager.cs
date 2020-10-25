using Savanna.Interfaces;
using System;
using System.Timers;

namespace Savanna
{
    /// <summary>
    /// Class that manages evrything about the Savanna field, like: field SetUp and timed field update
    /// </summary>
    public class FieldManager : IFieldManager
    {
        private readonly AnimalManager animalManager;
        private readonly Field field;
        private readonly UI ui;
        private Timer UpdateTimer;

        /// <summary>
        /// Class that manages evrything about the Savanna field, like: field SetUp and timed field update
        /// </summary>
        public FieldManager()
        {
            animalManager = new AnimalManager();
            field = new Field(40, 100);
            ui = new UI();

            SetFieldUpdateTimer();
        }

        /// <summary>
        /// Sets up a timer that fires repeatedly
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

            animalManager.SearchForAnimals(field);
            animalManager.MoveAllAnimals(field);
            animalManager.SetIfAnimalDead(field);
            animalManager.AnimalReset(field);
        }
    }
}