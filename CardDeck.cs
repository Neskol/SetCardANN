using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetCardANN
{
    public class CardDeck
    {
        public List<Card> Cards {  get; private set; }
        public List<Card[]> SetBundle { get; private set; }
        public Dictionary<double[], double[]> SetInputOutput { get; private set; }
        public Dictionary<double[], double[]> InputOutputSet { get; private set; }
        public Dictionary<Card[], double[]> CardInputOutputSet { get; private set; }

        public CardDeck()
        {
            this.Cards = new List<Card>();
            this.SetBundle = new List<Card[]>();
            this.SetInputOutput = new Dictionary<double[], double[]>();
            this.InputOutputSet = new Dictionary<double[], double[]>();
            this.CardInputOutputSet = new Dictionary<Card[], double[]>();
            this.InitializeCards();
            this.ConstructSetList();
        }

        public void InitializeCards()
        {
            this.Cards = new List<Card>();
            this.SetBundle = new List<Card[]>();
            int val = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            this.Cards.Add(new Card(i, j, k, l, val));
                            val++;
                        }
                    }
                }
            }

            // foreach (Card card in this.Cards) {Console.WriteLine(card.ToString()); }
        }

        public bool CheckSet(Card card1, Card card2, Card card3)
        {
            bool result =
    (card1.Color == card2.Color) &&
     (card2.Color == card3.Color) ||
    (card1.Color != card2.Color &&
      card2.Color != card3.Color &&
      card1.Color != card3.Color);
            result = result &&
              ((card1.Shape == card2.Shape && card2.Shape == card3.Shape) ||
              (card1.Shape != card2.Shape &&
                card2.Shape != card3.Shape &&
                card1.Shape != card3.Shape));
            result =
              result &&
              ((card1.Shading == card2.Shading && card2.Shading == card3.Shading) ||
                (card1.Shading != card2.Shading &&
                  card2.Shading != card3.Shading &&
                  card1.Shading != card3.Shading));
            result =
              result &&
              ((card1.Number == card2.Number && card2.Number == card3.Number) ||
                (card1.Number != card2.Number &&
                  card2.Number != card3.Number &&
                  card1.Number != card3.Number));
            return result;
        }

        public void ConstructSetList()
        {
            foreach (Card c1 in this.Cards)
            {
                foreach (Card c2 in this.Cards)
                {
                    foreach (Card c3 in this.Cards)
                    {
                        List<double> doubleArrayInKey = new List<double>();
                        doubleArrayInKey.AddRange(c1.DoubleValSet);
                        doubleArrayInKey.AddRange(c2.DoubleValSet);
                        doubleArrayInKey.AddRange(c3.DoubleValSet);
                        double[] inKey = doubleArrayInKey.ToArray();
                        //double[] inKey = { c1.IntVal, c2.IntVal, c3.IntVal };
                        if (this.IsDistinctCards(c1,c2,c3)&&this.CheckSet(c1, c2, c3))
                        {
                            Card[] newSet = { c1, c2, c3 };
                            bool pushNewSet = true;
                            foreach (Card[] set in this.SetBundle)
                            {
                                pushNewSet = pushNewSet && !(set.Contains(c1)&& set.Contains(c2)&& set.Contains(c3));
                            }
                            if (pushNewSet)
                            {
                                this.SetBundle.Add(newSet);
                                double[] outKey = { 1 };
                                this.SetInputOutput.Add(inKey, outKey);
                                this.InputOutputSet.Add(inKey,outKey);
                                this.CardInputOutputSet.Add(newSet, outKey);
                                // Console.WriteLine(this.SetToString(newSet));
                            }
                            else
                            {
                                double[] outKey = { 0 };
                                Card[] nonSet = {c1,c2,c3 };
                                this.InputOutputSet.Add(inKey, outKey);
                                this.CardInputOutputSet.Add(nonSet, outKey);
                            }
                        }
                        else
                        {
                            double[] outKey = { 0 };
                            Card[] nonSet = { c1, c2, c3 };
                            this.InputOutputSet.Add(inKey, outKey);
                            this.CardInputOutputSet.Add(nonSet, outKey);
                        }
                    }
                }
            }
        }

        public bool IsDistinctCards(Card card1, Card card2,Card card3)
        {
            return (!card1.Equals(card2) && !card2.Equals(card3) && !card3.Equals(card1));
        }
        
        public string SetToString(Card[] cards)
        {
            return "{"+cards[0].ToString()+","+ cards[1].ToString()+"," +cards[2].ToString()+"}";
        }

        public string SetToString(double[] intVal)
        {
            return "{" + this.Cards[(int)intVal[0]].ToString() + "," + this.Cards[(int)intVal[1]].ToString() + "," + this.Cards[(int)intVal[2]].ToString() + "}";
        }
    }
}