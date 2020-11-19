using System;

namespace BlackJac.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Issued { get; set; }
        public string Suit { get; set; }
    }
}