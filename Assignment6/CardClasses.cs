using System;
using System.Collections.Generic;
using System.Linq;

// Uses Automatic .NET Properties

namespace Assignment6
{
    public enum Suit { Diamonds, Clubs, Hearts, Spades};
    public enum Rank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King};

    // -------------------------------------------------------------------------------------------

    public class Card : IComparable
    {
        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }

        public Card(Rank rank = Rank.Ace, Suit suit = Suit.Spades)
        {
            Rank = rank;
            Suit = suit;
        }

        public int CompareTo(object obj)
        {
            Card c = obj as Card;
            if (c == null)
                throw new ArgumentException("Error: Invalid comparison to Card. Object is null or not a Card.");
                    //Console.Write($">>>Comparing {this} to {c}");
            int result = 0;
            if (this.Rank > c.Rank) 
                result = 1;
            else if (this.Rank < c.Rank)
                result = -1;
            else if (this.Suit > c.Suit)
                result = 1;
            else if (this.Suit < c.Suit)
                result = -1;
            else 
                result = 0;

            return result;   
        }

        public override string ToString()
        {
            return $"[{Rank} of {Suit}]";
        }

    }

    // -------------------------------------------------------------------------------------------

    public class Deck
    {
        //private Card[] cards;
        private Stack<Card> cards;
        

        public Deck()
        {
            Array suits = Enum.GetValues(typeof(Suit));
            Array ranks = Enum.GetValues(typeof(Rank));
            // List<Suit>suits = Enum.GetValues(typeof(Suit)).Cast<Suit>().ToList();
            // List<Rank>ranks = Enum.GetValues(typeof(Rank)).Cast<Rank>().ToList();  //test (switch to list)

            if ( suits.Length * ranks.Length != 52)
                throw new ArgumentOutOfRangeException("Error: Number of cards out of range. Deck of cards must be 52");

            cards = new Stack<Card>();
                       
            foreach (Suit suit in suits)
            {
                foreach (Rank rank in ranks)
                {
                    //Card card = new Card(rank, suit);
                    cards.Push(new Card(rank, suit));    //test (switch to list)
                    //cards[i++] = card;
                }
            }
        }

        public int Size()
        {
            //return cards.Length;
            return cards.Count; //test (switch to list)
        }

        public void Shuffle()
        {
            Random rng = new Random();

            int deckSize = Size();
            // Throw ArgumentNullException exception 
            if (deckSize == 0) 
            throw new ArgumentNullException("Error: Cannot shuffle an empty deck");       // Cannot shuffle an empty deck

            // Fisher-Yates Shuffle (modern algorithm)
            //   - http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle

            Card[] cardArray = cards.ToArray();
            for (int i = 0; i < deckSize; i++)
            {
                int j = rng.Next(i, deckSize);
                Card c = cardArray[i];
                cardArray[i] = cardArray[j];
                cardArray[j] = c;
            }
            cards = new Stack<Card>(cardArray); // Array to Stack
        }

        public Card DealCard()
        {
            // Throw ArgumentNullException when null is passed in as an argument
            if (Size() == 0)
                throw new ArgumentNullException("Error: Deck is empty");
            return cards.Pop();
        }

        public override string ToString()
        {
            string s = "[";
            string comma = "";
            foreach (Card c in cards)
            {
                s += comma + c.ToString();
                comma = ", ";
            }
            s += "]";
            s += "\n " + Size() + " cards in deck.\n";

            return s;
        }

    }

}