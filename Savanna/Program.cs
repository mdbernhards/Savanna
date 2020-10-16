namespace Savanna
{
    /// <summary>
    /// Main program class, starts the program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Starts the program, by creating a gameField object and calling SetUpField method
        /// </summary>
        static void Main(string[] args)
        {
            FieldManager gameField = new FieldManager();
            gameField.SetUpField();
        }
    }
}