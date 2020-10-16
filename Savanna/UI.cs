using System;
using System.Text;

namespace Savanna
{
    /// <summary>
    /// Class that displays all the needed information to the user
    /// </summary>
    public class UI
    {
        /// <summary>
        /// When called draws the grid with given information
        /// </summary>
        public void DrawField(Field field)
        {
            Console.Clear();
            StringBuilder fieldString = new StringBuilder();

            fieldString.AppendLine(" Spawn Antelope by pressing A");
            fieldString.AppendLine(" Spawn Lion by pressing L");
            fieldString.AppendLine();
            fieldString.AppendLine(" + -------------------------------------------------------------------------------------------------- +");

            for (int line = 0; line < field.Height; line++)
            {
                fieldString.Append(" |");

                for (int character = 0; character < field.Width; character++)
                {
                    if (field.SavannaField[line, character].Type == 'E')
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