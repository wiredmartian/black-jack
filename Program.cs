using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJac
{
    class Program
    {
        private static readonly string[] Suits = { "Diamonds", "Hearts", "Spades", "Clubs" };
        private static readonly List<Card> LoadedDeckList = new List<Card>();
        static void Main(string[] args)
        {
            /*
             * load the deck, the complete set should be 52 cards
             */
            PopulateDeck();

            bool gameComplete = false; // game state
            int playerTotal = 0;
            int dealerTotal = 0;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--- SERVING PLAYER ---");
            /*
             * keep processing inputs while the game is still in session
             * while (!gameComplete);
             */
            do
            {
                /*
                 * process only [N/Y] key responses
                 * while (response != ConsoleKey.Y && response != ConsoleKey.N);
                 */
                ConsoleKey response;
                do
                {
                    Console.Write("Do you wanna receive a card [Y/N] ");
                    response = Console.ReadKey(false).Key;  
                    Console.WriteLine();
                    switch (response)
                    {
                        /*
                         * the player gets served first until he/she replies [N]
                         */
                        case ConsoleKey.Y:
                        {
                            var card = ServeCard();
                            playerTotal += card.Value;
                        
                            Console.WriteLine($"Served card:: {card.Name} of {card.Suit}");
                            Console.WriteLine($"PLAYER TOTAL:: {playerTotal}");
                            LoadedDeckList.Remove(card);
                        
                            // terminate the game is the total is over 21
                            if (playerTotal > 21)
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"!! PLAYER HAS LOST !!");
                                gameComplete = true;
                            }

                            break;
                        }
                        // this condition is in case the player replies [N] on first iteration
                        case ConsoleKey.N when playerTotal == 0:
                            continue;
                        case ConsoleKey.N:
                        {
                            /*
                             * the player replied [N]
                             * lets serve the dealer
                             */
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("--- SERVING DEALER ---");
                            // continue to serve the dealer while his/her total is under 17
                            while (dealerTotal < 17)
                            {
                                var newCard = ServeCard();
                                dealerTotal += newCard.Value;
                                Console.WriteLine($"Served card:: {newCard.Name} of {newCard.Suit}");
                                Console.WriteLine($"DEALER TOTAL:: {dealerTotal}");
                                LoadedDeckList.Remove(newCard);
                            
                                // terminate the game is the total is over 21
                                if (dealerTotal > 21)
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"!! DEALER HAS LOST !!");
                                }
                            }
                            Console.WriteLine();
                            /*
                         * neither the player nor dealer went over 21
                         * find the winner
                         */
                            if (dealerTotal <= 21 && playerTotal <= 21)
                            {
                                var winnerMessage = (playerTotal > dealerTotal) ? "!! PLAYER WINS !!" : "!! DEALER WINS !!";
                                // there is a possible draw
                                if (playerTotal == dealerTotal)
                                {
                                    winnerMessage = "!! ITS A DRAW !!";
                                }
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(winnerMessage);
                            }

                            gameComplete = true;
                            break;
                        }
                    }
                } while (response != ConsoleKey.Y && response != ConsoleKey.N);
            } while (!gameComplete);

        }

        private static void PopulateDeck()
        {
            var cards = new Dictionary<string, int>()
            {
                { "J", 10 }, { "K", 10 }, { "Q", 10 }, { "A", 1 }, { "2", 2 }, { "3", 3 },
                { "4", 4 }, { "5", 5 }, { "6", 6 }, { "7", 7 }, { "8", 8 }, { "9", 9 }, { "10", 10 }, 
            };
            foreach (var s in Suits)
            {
                for (var x = 0; x < cards.Count; x++)
                {
                    LoadedDeckList.Add(new Card()
                    {
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
public class Card
{
    public string Name { get; set; }
    public int Value { get; set; }
    public string Suit { get; set; }
}