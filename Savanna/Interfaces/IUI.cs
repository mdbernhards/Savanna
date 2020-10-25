namespace Savanna.Interfaces
{
    /// <summary>
    /// Interface of class that displays all the needed information to the user
    /// </summary>
    public interface IUI
    {
        /// <summary>
        /// When called draws the grid with given information
        /// </summary>
        /// <param name="field">The field that is going to be drawn</param>
        public void DrawField(Field field);
    }
}