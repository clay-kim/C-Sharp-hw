using System;
using System.Collections.Generic;
using static System.Console;

// Uses Automatic .NET Properties

namespace Assignment6
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Player> players = new List<Player>();

            players.Add(new RandomPlayer("Paul the Random"));
            players.Add(new HighestPlayer("Tom the Highest"));
            players.Add(new LowestPlayer("Pat the Lowest"));
            players.Add(new MiddlePlayer("Susan the Middle"));
            

            WriteLine("\nHighest Rank Wins! Card Game Simulation");
            WriteLine("=================================================");

            Deck theDeck = new Deck();
          
            WriteLine($"\nHere's the new deck of cards:\n{theDeck}");

            theDeck.Shuffle();
            WriteLine($"\nHere's the shuffled deck of cards:\n{theDeck}");

            int numRounds = theDeck.Size() / players.Count;

            // Deal the cards
            int i = 0;
            while (theDeck.Size() > 0)
            {
                players[i].AddCardToHand(theDeck.DealCard());
                i = ++i % players.Count;
            }
            
            // Display each players starting hand
            WriteLine("\nAnd here are our players and their hands:");
            foreach (Player player in players)
                WriteLine($"{player}\n");

            // Play the game
            for (int round=1; round <= numRounds; round++)
            {
                WriteLine($"\nStarting round #{round}...");

                List<Card> cardsPlayed = new List<Card>();
                foreach (Player player in players)
                {
                    Card cardPlayed = player.ChooseCardFromHand();
                    if ( cardPlayed == null)
                        throw new ApplicationException($"{player.Name}'s hand is empty prematurely!");
                        cardsPlayed.Add(cardPlayed);
                        
                    WriteLine($"{player.Name} played the {cardPlayed}");
                }

                Rank maxRank = Rank.Ace;
                Suit maxSuit = Suit.Diamonds;  

                // get the highest card (no tie)
                for (i=0; i < players.Count; i++)
                {
                    if (cardsPlayed[i].Rank > maxRank)
                    {
                        maxRank = cardsPlayed[i].Rank;
                        maxSuit = cardsPlayed[i].Suit;
                    }

                    if (cardsPlayed[i].Rank == maxRank)
                    {
                        if(cardsPlayed[i].Suit > maxSuit)
                            maxSuit = cardsPlayed[i].Suit;
                    }
                }

                //WriteLine($"The maximum rank in this round was {maxRank} of {maxSuit}"); // no need 

                for (i=0; i < players.Count; i++)
                {
                    if (cardsPlayed[i].Rank == maxRank && cardsPlayed[i].Suit == maxSuit)   //only one winner
                    {   
                      
                            players[i].AddPoint();
                            WriteLine($"{players[i].Name} got a point for playing [{maxRank} of {maxSuit}]!");
                        
                    }
                }

                WriteLine($"Round #{round} is complete.");
            }

            WriteLine("\n============== Game Over! =================\n");

            WriteLine("Final Scores:");
            WriteLine("--------------------------");
            foreach (Player player in players)
            {
                WriteLine($"{player.Name} has {player.Score} points");
            }

            int winningScore = 0;
            foreach (Player player in players)
            {
                if (player.Score > winningScore)
                    winningScore = player.Score;
            }

            string ampersand = "";
            string winnerNames = "";
            foreach (Player player in players)
            {
                if (player.Score == winningScore)
                {
                    winnerNames += $"{ampersand}{player.Name}";
                    ampersand = " & ";
                }
            }

            WriteLine();
            if (winnerNames.Contains("&"))
                WriteLine($"It's a tie! With {winningScore} points, the winners are {winnerNames}!");
            else
                WriteLine($"The winner is {winnerNames} with {winningScore} points!");
            WriteLine();
        }

    }

}