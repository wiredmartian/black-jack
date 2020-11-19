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

            foreach (var card in LoadedDeckList)
            {
                Console.WriteLine($"{card.Suit} - {card.Name} {card.Value}");
            }
        }

        private static void PopulateDeck()
        {
            var cards = new Dictionary<string, int>()
            {
                { "Jack", 10 }, { "King", 10 }, { "Queen", 10 },
                { "Ace", 1 }, { "2", 2 }, { "3", 3 },
                { "4", 4 }, { "5", 5 }, { "6", 6 },
                { "7", 7 }, { "8", 8 }, { "9", 9 }, { "10", 10 }, 
            };
            foreach (var t in Suits)
            {
                for (var x = 0; x < cards.Count; x++)
                {
                    LoadedDeckList.Add(new Card()
                    {
                        Id = Guid.NewGuid(),
                        Name = cards.ElementAt(x).Key,
                        Value = cards.ElementAt(x).Value,
                        Issued = false,
                        Suit = t
                    });
                }
            }
        }
    }
}