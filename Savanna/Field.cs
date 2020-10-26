using Savanna.Interfaces;

namespace Savanna
{
    /// <summary>
    /// Stores information about the Savanna field, like what animals are in it and its Height and Width
    /// </summary>
    public class Field : IField
    {
        /// <summary>
        /// Stores information about the animals and the location of them
        /// </summary>
        public Animal[,] SavannaField { get; set; }

        /// <summary>
        /// How High is the Savanna field
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// How Wide is the Savanna field
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Stores information about the Savanna field, like what animals are in it and its Height and Width
        /// </summary>
        /// <param name="height">Height of Animal array SavannaField beeing created</param>
        /// <param name="width">Width of Animal array SavannaField beeing created</param>
        public Field(int height, int width)
        {
            Height = height;
            Width = width;
            SavannaField = new Animal[Height, Width];
        }
    }
}