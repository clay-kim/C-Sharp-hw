using Assignment6;
using System;
using System.Collections.Generic;

public abstract class Player
{
   public string Name { get; private set; }
   public int Score { get; private set; }
   protected Hand hand;

   public Player(string name)
   {
        if(name == null)
            throw new ArgumentNullException("Error: player with no name.");
        if(name.Length == 0)
            throw new ArgumentException("Error: player with no name.");
      Name = name;
      Score = 0;
      hand = new Hand();
   }

    //------Abstract method----
   public abstract Card ChooseCardFromHand();

   // Other Player methods go here, but do NOT include RemoveCardFromHand().
   //   (You'll want to use ChooseCardFromHand() instead.)
   public void AddCardToHand(Card card)
        {
            // Throw ArgumentNullException for no card
            if (card == null)
                throw new ArgumentNullException("Error: Card is null.");
          
            hand.AddCard(card);
        }

        public void AddPoint()
        {
            Score++;
        }
        


   public override string ToString()
   {
       return $"{Name}'s Hand: {hand}";
   }
}
// -------------------------------------------------------------------------------------------

    public class Hand
    {
        private List<Card> cards;
        private static Random rand = new Random();

        public Hand()
        {
            cards = new List<Card>();                        // Empty hand
        }

        public int Size()
        {
            return cards.Count;
        }

        public List<Card> GetCards()
        {
            var myCards = new List<Card>(cards);    
            return myCards;
        }

        public void AddCard(Card card)
        {
            if (card == null)
                throw new ArgumentNullException("Error: card is null.");
            cards.Add(card);
            cards.Sort();
        }

        public Card RemoveRandomCard()
        {
            if (Size() == 0) return null;

            int position = rand.Next(Size());
            Card card = cards[position];
            cards.RemoveAt(position);
            return card;
        }

        public Card RemoveHighestCard()
        {
            if (Size() == 0) return null;
            
            Card card = cards[Size() - 1];
            cards.RemoveAt(Size() - 1);
            return card;
        }

        public Card RemoveLowestCard()
        {
            if (Size() == 0) return null;

            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public Card RemoveMiddleCard()
        {
            if (Size() == 0 ) return null;

            Card card = cards[Size() / 2];
            cards.RemoveAt(Size() / 2);
            return card;
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

            return s;
        }
    }
    // -------------------------------------------------------------------------------------------
        public class RandomPlayer : Player
        {
            public RandomPlayer(string name) : base(name){}
            public override Card ChooseCardFromHand()
            {
                return hand.RemoveRandomCard();
            }

        }
    // -------------------------------------------------------------------------------------------
        public class HighestPlayer : Player
        {
            public HighestPlayer(string name) : base(name){}

            public override Card ChooseCardFromHand()
            {
                return hand.RemoveHighestCard();
            }

        }
    
    // -------------------------------------------------------------------------------------------

        public class LowestPlayer : Player
        {
            public LowestPlayer (string name) : base(name){}

            public override Card ChooseCardFromHand()
            {
                return hand.RemoveLowestCard();
            }

        }

    // -------------------------------------------------------------------------------------------
        public class MiddlePlayer : Player
        {
            public MiddlePlayer(string name) : base(name){}

            public override Card ChooseCardFromHand()
            {
                return hand.RemoveMiddleCard();
            }

        }
        
   
   
    

