using System;
using System.Collections.Generic;
using System.Linq;
using BlackJac.Models;

namespace BlackJac
{
    class Program
    {
        private static readonly string[] Suits = { "Diamonds", "Hearts", "Spades", "Clubs" };
        private static readonly List<Card> LoadedDeckList = new List<Card>();
        static void Main(string[] args)
        {
            PopulateDeck();
            Console.WriteLine($"{LoadedDeckList.Count}");
            ConsoleKey response;
            
            bool roundComplete = false;
            int playerTotal = 0;
            int dealerTotal = 0;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--- SERVING PLAYER ---");
            do
            {
                do
                {
                    Console.Write("Do you wanna receive a card [Y/N] ");
                    response = Console.ReadKey(false).Key;  
                    Console.WriteLine();
                    if (response == ConsoleKey.Y)
                    {
                        var card = ServeCard();
                        playerTotal += card.Value;
                        
                        Console.WriteLine($"Served card:: {card.Name} of {card.Suit}");
                        Console.WriteLine($"PLAYER TOTAL:: {playerTotal}");
                        LoadedDeckList.Remove(card);
                        
                        if (playerTotal > 21)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"!! PLAYER HAS LOST !!");
                            roundComplete = true;
                        }

                    }
                    if (response == ConsoleKey.N)
                    {
                        if (playerTotal != 0)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("--- SERVING DEALER ---");
                            while (dealerTotal < 17)
                            {
                                var newCard = ServeCard();
                                dealerTotal += newCard.Value;
                                Console.WriteLine($"Served card:: {newCard.Name} of {newCard.Suit}");
                                Console.WriteLine($"DEALER TOTAL:: {dealerTotal}");
                                LoadedDeckList.Remove(newCard);
                                if (dealerTotal > 21)
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"!! DEALER HAS LOST !!");
                                }
                            }
                            Console.WriteLine();
                        
                            if (dealerTotal <= 21 && playerTotal <= 21)
                            {
                                var winnerMessage = (playerTotal > dealerTotal) ? "!! PLAYER WINS !!" : "!! DEALER WINS !!";
                                if (playerTotal == dealerTotal)
                                {
                                    winnerMessage = "!! ITS A DRAW !!";
                                }
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(winnerMessage);
                            }

                            roundComplete = true;
                        }
                    }
                } while (response != ConsoleKey.Y && response != ConsoleKey.N);
                
            } while (!roundComplete);

        }

        private static void PopulateDeck()
        {
            var cards = new Dictionary<string, int>()
            {
                { "J", 10 }, { "K", 10 }, { "Q", 10 },
                { "Ace", 1 }, { "2", 2 }, { "3", 3 },
                { "4", 4 }, { "5", 5 }, { "6", 6 },
                { "7", 7 }, { "8", 8 }, { "9", 9 }, { "10", 10 }, 
            };
            foreach (var s in Suits)
            {
                for (var x = 0; x < cards.Count; x++)
                {
                    LoadedDeckList.Add(new Card()
                    {
                        Id = Guid.NewGuid(),
                        Name = cards.ElementAt(x).Key,
                        Value = cards.ElementAt(x).Value,
                        Suit = s
                    });
                }
            }
        }

        private static Card ServeCard()
        {
            var random = new Random();
            var ranCard = random.Next(1, LoadedDeckList.Count);
            return LoadedDeckList[ranCard];
        }
    }
}