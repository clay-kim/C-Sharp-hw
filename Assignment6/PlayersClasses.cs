// using System;
// using System.Collections.Generic;

// // Uses Automatic .NET Properties

// namespace Assignment6
// {
//     public class Player
//     {
//         public Player(string name, int score) 
//         {
//             this.Name = name;
//                 this.Score = score;
               
//         }
//         public string Name { get; private set; }
//         public int Score { get; private set; }
//         private Hand hand;          // No getters/setters for hand, so use a private member variable

//         public Player(string name)
//         {
//             Name = name;
//             Score = 0;
//             hand = new Hand();
//         }

//         public void AddCardToHand(Card card)
//         {
//             // Throw ArgumentNullException for no card
//             if (card == null)
//                 throw new ArgumentNullException("Error: Empty Card");
                
//             hand.AddCard(card);
//         }

//         public Card RemoveCardFromHand()
//         {
//             return hand.RemoveRandomCard();
//         }

//         public void AddPoint()
//         {
//             Score++;
//         }

//         public override string ToString()
//         {
//             return $"{Name}'s Hand:\n {hand}";
//         }
//     }

//     // -------------------------------------------------------------------------------------------

//     public class Hand
//     {
//         //private Card[] cards;
//         private List<Card> cards = new List<Card>();

//         public Hand()
//         {
//            // cards = new Card[0];                        // Empty hand
//            cards = new List<Card>();    //test (switch to list)
//         }

//         public int Size()
//         {
//             //return cards.Length;
//             return cards.Count; //test (switch to list)
//         }

//         public Card[] GetCards()
//         {
//             Card[] myCards = new Card[Size()];          // Return a copy
//             Array.Copy(cards, myCards, Size());         //   so that our cards cannot be changed
//             return myCards;
//         }

//         public void AddCard(Card card)
//         {
//             Array.Resize(ref cards, Size() + 1);
//             cards[Size()-1] = card;
            
//         }

//         public Card RemoveRandomCard()
//         {
//             if (Size() == 0) return null;

//             Card card = cards[Size() - 1];
//             Array.Resize(ref cards, Size() - 1);
//             return card;
//         }

//         public override string ToString()
//         {
//             string s = "[";
//             string comma = "";
//             foreach (Card c in cards)
//             {
//                 s += comma + c.ToString();
//                 comma = ", ";
//             }
//             s += "]";

//             return s;
//         }
//     }

// }