using Savanna.Interfaces;
using System;
using System.IO;
using System.Text;

namespace Savanna
{
    /// <summary>
    /// Class that displays all the needed information to the user
    /// </summary>
    public class UI : IUI
    {
        /// <summary>
        /// When called draws the grid with given information
        /// </summary>
        /// <param name="field">The field that is going to be drawn</param>
        public void DrawField(IField field)
        {
            Console.Clear();
            StringBuilder fieldString = new StringBuilder();

            GetFileInfo getFiles = new GetFileInfo();
            FileInfo[] Files = getFiles.GetDllFileInfo();

            string spawnText = " Spawn ";

            foreach (FileInfo filePath in Files)
            {
                string animalName = filePath.Name;
                animalName = animalName.Remove(animalName.Length - 4);

                spawnText = spawnText + animalName + ", ";
            }

            spawnText = spawnText.Remove(spawnText.Length - 2);
            spawnText += " by pressing the first letter in its name";

            fieldString.AppendLine(spawnText);
            fieldString.AppendLine();
            fieldString.AppendLine(" + -------------------------------------------------------------------------------------------------- +");

            for (int line = 0; line < field.Height; line++)
            {
                fieldString.Append(" |");

                for (int character = 0; character < field.Width; character++)
                {
                    if (field.SavannaField[line, character] == null)
                    {
                        fieldString.Append(" ");
                    }
                    else
                    {
                        fieldString.Append(field.SavannaField[line, character].Type);
                    }
                }

                fieldString.AppendLine("|");
            }

            fieldString.AppendLine(" + -------------------------------------------------------------------------------------------------- +");
            Console.WriteLine(fieldString);
        }
    }
}