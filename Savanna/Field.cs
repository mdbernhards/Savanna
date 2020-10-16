namespace Savanna
{
    public class Field
    {
        public Animal[,] SavannaField { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Field(int height, int width)
        {
            Height = height;
            Width = width;
            SavannaField = new Animal[Height, Width];
        }
    }
}