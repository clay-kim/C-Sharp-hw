using System;

namespace Assignment05
{
    public class PlayerClass
    {
        private string playerName;
        private int score;
        private PlayersHand cardsInHand;

        public PlayerClass(string name)
        {
            this.playerName = name;
            this.score = 0;
            this.cardsInHand = new PlayersHand();
        }
    
        public string getName()
        {
            return playerName;
        }

        public int getScore()
        {
            return score;
        }

        public CardClass GetCard()
        {
            return cardsInHand.getCard();
        }


        public void AddCardtoPlayer(CardClass card)
        {
            cardsInHand.CardsInHand(card);
        }

        public void RemoveCardtoPlayer()
        {
            cardsInHand.CardsOutHand();
        }

         public CardClass getPlayedCard()
        {
            return cardsInHand.CardsOutHand();
          
        }

        public void AddScore()
        {
            score++;
        }


        public override string ToString()
        {
            string s = "";
            s = $"{playerName}'s Hand: \n  {cardsInHand}";
            return s;
        }


    }

    public class PlayersHand
    {
        private CardClass[] cardsInHand;

        public PlayersHand()
        {
            cardsInHand = new CardClass[0];   // 0 card
        }

        public int getSize()
        {
            return cardsInHand.Length;
        }

        public void CardsInHand(CardClass card)
        {
            Array.Resize(ref cardsInHand, getSize() + 1);
            cardsInHand[getSize() - 1] = card;
        }

        public CardClass getCard()
        {
            CardClass card = cardsInHand[getSize() - 1];
            return card;
        }

        public CardClass CardsOutHand()
        {
            CardClass card = cardsInHand[getSize() - 1];
            Array.Resize(ref cardsInHand, getSize() - 1);
            return card;
        }

        public override string ToString()
        {
            string s = "[";
            string comma = "";
            foreach (CardClass card in cardsInHand)  
            {
                s += comma + card;
                comma = ", ";
            }
            s += "]";
        
            return s;
        }
        
    
    }




}