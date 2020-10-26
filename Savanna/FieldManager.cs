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
        private readonly IAnimalManager _animalManager;
        private readonly IUI _ui;
        private readonly IField _field;
        private Timer UpdateTimer;

        /// <summary>
        /// Class that manages evrything about the Savanna field, like: field SetUp and timed field update
        /// </summary>
        public FieldManager(IAnimalManager animalManager, IUI ui, IField field)
        {
            _animalManager = animalManager;
            _ui = ui;
            _field = field;

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
            _animalManager.CheckForAnimalSpawn(_field);
            _ui.DrawField(_field);

            _animalManager.SearchForAnimals(_field);
            _animalManager.MoveAllAnimals(_field);
            _animalManager.SetIfAnimalDead(_field);
            _animalManager.AnimalReset(_field);
        }
    }
}