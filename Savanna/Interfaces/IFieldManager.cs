using System;
using System.Timers;

namespace Savanna.Interfaces
{
    /// <summary>
    /// Interface for class that manages evrything about the Savanna field, like: field SetUp and timed field update
    /// </summary>
    public interface IFieldManager
    {
        /// <summary>
        /// Sets up a timer that fires repeatedly
        /// </summary>
        public void SetFieldUpdateTimer();

        /// <summary>
        /// Updates the field, if any changes happen like animals added, animals move. Calls the method that draws everything
        /// </summary>
        /// <param name="e">Used by timer</param>
        /// <param name="source">Used by timer</param>
        public void FieldUpdate(Object source, ElapsedEventArgs e);
    }
}