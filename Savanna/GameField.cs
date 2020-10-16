using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Savanna
{
    public class GameField
    {
        public string[,] Field { get; set; }

        private static Timer UpdateTimer;

        private const string Empty = "empty";
        private const string Lion = "lion";
        private const string Antelope = "antelope";

        private int Height;
        private int Width;

        public GameField()
        {
            Field = new string[40, 100];

            Height = Field.GetLength(0);
            Width = Field.GetLength(1);
        }

        public void SetUpField()
        {
            for (int line = 0; line < Height; line++)
            {
                for (int character = 0; character < Width; character++)
                {
                    Field[line, character] = Empty;
                }
            }

            SetFieldUpdateTimer();
        }

        public void SetFieldUpdateTimer()
        {
            UpdateTimer = new Timer(1000);
            UpdateTimer.Elapsed += FieldUpdate;
            UpdateTimer.AutoReset = true;
            UpdateTimer.Enabled = true;

            //Infinite loop so the program doesn't stop
            while (true) { }

        }

        public void FieldUpdate(Object source, ElapsedEventArgs e)
        {

        }

    }
}
