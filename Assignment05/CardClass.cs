using System;

namespace Assignment05
{
    public enum Suit {  Clubs, Diamonds, Hearts, Spades };
    
    public enum Rank {  Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };
    public class CardClass
    {
        private Suit suits;
        private Rank ranks;

        public Rank getRank()
        {
            return ranks;
        }

        public Suit GetSuit()
        {
            return suits;
        }
        public CardClass(Rank rank, Suit suit)
        {
            this.ranks = rank;
            this.suits = suit;
        }

        public override string ToString()
        {
            string str = "";
            str = $"[{ranks} of {suits}]";
            return str;
        }
    }

    //============ Deck Class ===========================

    public class DeckClass
    {
        private CardClass[] cards;

        public DeckClass()
        {
            Array ranks = Enum.GetValues(typeof(Rank));  //  4
            Array suits = Enum.GetValues(typeof(Suit));  // 13

            cards = new CardClass[ranks.Length * suits.Length];
            int index = 0;
            foreach(Suit s in suits)
            {
                foreach(Rank r in ranks)
                {
                    CardClass card = new CardClass(r, s);
                    cards[index] = card;
                    index++;
                }
            }
        }

        public int getSize()
        {
            return cards.Length;
        }

        // Fisher-Yates Shuffle (modern algorithm)
        public void Shuffle()
        {
            Random rng = new Random();

            int deckSize = getSize();
            if (deckSize == 0) return;          
            for (int i = 0; i < deckSize; i++)
            {
                int j = rng.Next(i, deckSize);
                CardClass c = cards[i];
                cards[i] = cards[j];
                cards[j] = c;
            }
        }

        public CardClass DealCard()
        {
            if (getSize() == 0) return null;

            CardClass card = cards[getSize() - 1];
            Array.Resize(ref cards, getSize() - 1);

            return card;
        }

        public override string ToString()
        {
            string s = "[";
            string comma = "";
            foreach(CardClass card in cards)
            {
                s += comma + card.ToString();
                comma = ", ";
            }
            s += "]";

            string info = $"\n{getSize()} cards in deck.";

            return s + info;
        }

    }
    
}