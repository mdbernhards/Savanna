namespace Savanna
{
    /// <summary>
    /// Main program class, starts the program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the program, by creating a gameField object and calling SetUpField method
        /// </summary>
        public static void Main(string[] args)
        {
            FieldManager gameField = new FieldManager();
            gameField.SetUpField();
        }
    }
}