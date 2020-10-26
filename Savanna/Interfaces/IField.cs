namespace Savanna.Interfaces
{
    /// <summary>
    /// Interface for class that stores information about the Savanna field, like what animals are in it and its Height and Width
    /// </summary>
    public interface IField
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

    }
}