using System;
using static System.Console;

namespace Assignment05
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Higest Rank Wins! Card Game Simulation");
            WriteLine("=================================================");
            WriteLine();

            DeckClass cards = new DeckClass();
            int deckSize = cards.getSize();

            WriteLine("Here's the new deck of cards:");
            WriteLine(cards.ToString());
            WriteLine();

            WriteLine("Here's the shuffled deck of cards:");
            cards.Shuffle();
            WriteLine(cards.ToString());
            WriteLine();

            PlayerClass[] players = new PlayerClass[4];
           
            players[0] = new PlayerClass("Tom");
            players[1] = new PlayerClass("Sarah");
            players[2] = new PlayerClass("Robert");
            players[3] = new PlayerClass("Clay");

            // Deal cards
            int i = 0;
            while (cards.getSize() > 0)
            {
                for( i = 0; i < players.Length; i++)
                {
                    players[i].AddCardtoPlayer(cards.DealCard());
                } 
                i = 0;            
            }
           
            WriteLine("And here are our players and their hands:");
    
            foreach (PlayerClass p in players)
                WriteLine(p.ToString());
                WriteLine();

            WriteLine("============= The Game Begins! =============");
            WriteLine();

            CardClass[] playedCard = new CardClass[players.Length];
            
            WriteLine(players[0].ToString());
            WriteLine($" here is the played card: {playedCard[0]}");
            
            WriteLine(deckSize);
            //play the game
            int numOfRound = deckSize / players.Length;
            WriteLine(numOfRound);


            for(int round = 1; round <= numOfRound; round++)
            {
                WriteLine($"Starting round #{round}...");

                CardClass[] playedCards = new CardClass[players.Length];

                for ( i = 0; i < players.Length; i++)
                {
                    playedCards[i] = players[i].getPlayedCard();
                    WriteLine($"{players[i].getName()} played the {playedCards[i]}");
                }   

                Rank maxRank = Rank.Ace;
                for (i=0; i < players.Length; i++)
                {
                    if (playedCards[i].getRank() > maxRank)
                        maxRank = playedCards[i].getRank();
                }
                WriteLine($"The maximum rank in this round was [{maxRank}]");

                for ( i = 0; i < players.Length; i++)
                {
                    if (playedCards[i].getRank() == maxRank)
                    {
                        players[i].AddScore();
                        WriteLine($"{players[i].getName()} got a point!");
                    }
                }
                WriteLine($"Round #{round} is complete.");
                WriteLine();

            } 

            WriteLine("============ Game Over ============");
            WriteLine();
            WriteLine("Final Scores:");
            WriteLine("------------------------------------");

            int finalWinnerScore = 0;
            for (i = 0; i < players.Length; i++)
            {
                WriteLine($"{players[i].getName()} has {players[i].getScore()} points");
                if (players[i].getScore() > finalWinnerScore)
                {
                    finalWinnerScore = players[i].getScore();
                }
            }
            WriteLine();

            for (i = 0; i < players.Length; i++)
            {
                if(players[i].getScore() == finalWinnerScore)
                WriteLine($"The winner is {players[i].getName()} with {finalWinnerScore} points!");
            }
        }
    }
}
