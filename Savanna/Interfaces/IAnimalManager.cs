namespace Savanna.Interfaces
{
    interface IAnimalManager
    {
        /// <summary>
        /// Starts the moving process, chooses what type of moving will the animal do: normal, running away, attacking.
        /// </summary>
        /// <param name="field">Field that the animals move on</param>
        public void MoveAllAnimals(Field field);

        /// <summary>
        /// Checks if there is a need to spawn animal, if there is calls SpawnAnimal method and gives it the type of animal that is needed to be spawned
        /// </summary>
        /// <param name="field">Field that is given to SpawnAnimal method if animal spawning is needed</param>
        public void CheckForAnimalSpawn(Field field);

        /// <summary>
        /// Resets Animal array field.SavannaField property HasMoved to false for all array members
        /// </summary>
        /// <param name="field">Animal field where the property will be reset</param>
        public void AnimalReset(Field field);

        /// <summary>
        /// Checks each animal health, if it's 0 or below sets the animal type to 'E' or Empty
        /// </summary>
        /// <param name="field">Animal field where the animal heath is checked and dead animals are set</param>
        public void SetIfAnimalDead(Field field);

        /// <summary>
        /// Searches for animals in animal array that is in field object. If finds an animal calls methods that check if an animal will be eaten or a new one born
        /// </summary>
        /// <param name="field">Field object that includes the animal array that is the Savanna game field</param>
        public void SearchForAnimals(Field field);
    }
}