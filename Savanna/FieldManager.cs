using System;
using System.Timers;

namespace Savanna
{
    public class FieldManager
    {
        private Field field;
        private UI ui;
        private AnimalManager animalManager;

        private static Timer UpdateTimer;

        private const char Empty = 'E';
        private const char Lion = 'L';
        private const char Antelope = 'A';

        public FieldManager()
        {
            animalManager = new AnimalManager();
            ui = new UI();
            field = new Field(40, 100);
        }

        public void SetUpField()
        {
            for (int line = 0; line < field.Height; line++)
            {
                for (int character = 0; character < field.Width; character++)
                {
                    field.SavannaField[line, character] = new Animal();
                    field.SavannaField[line, character].Type = Empty;
                }
            }

            SetFieldUpdateTimer();
        }

        public void SetFieldUpdateTimer()
        {
            UpdateTimer = new Timer(500);
            UpdateTimer.Elapsed += FieldUpdate;
            UpdateTimer.AutoReset = true;
            UpdateTimer.Enabled = true;

            //Infinite loop so the program doesn't stop
            while (true) { }

        }

        public void FieldUpdate(Object source, ElapsedEventArgs e)
        {
            animalManager.CheckForAnimalSpawn(field);

            ui.DrawField(field);

            animalManager.Move(field);
        }

    }
}
