using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetCardANN
{
    public class Card
    {
        private string[] colors = { "red", "green", "blue" };
        private string[] shadings = { "empty", "lined", "solid" };
        private string[] shapes = {"squiggle", "diamond", "oval"};
        private int[] numbers = {1, 2, 3};

        public string Color { get; private set; }
        public string Shading { get; private set; }
        public string Shape { get; private set; }
        public int Number { get; private set; }
        public int IntVal { get; private set; }


        public Card(int color, int shading, int shape, int number, int intVal)
        {
            this.Color = colors[color];
            this.Shading = shadings[shading];
            this.Shape = shapes[shape];
            this.Number = number;
            this.IntVal = intVal;
        }

        public override string ToString()
        {
            return this.Color + "_" + this.Shape + "_" + this.Number + "_" + this.Shading;
        }

        public override bool Equals(object? obj)
        {
            if ((obj as Card) == null) return false;
            else return this.ToString().Equals((obj as Card).ToString());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
