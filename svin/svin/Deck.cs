using System;
using System.Collections.Generic;
using System.Linq;

namespace svin
{
    public class Deck
    {
        private readonly List<Card> _cards;
        private readonly Random _rnd = new Random();

        public Deck()
        {
            _cards = new List<Card>();

            // создаём стандартную колоду 52 карты (4 масти × 13 рангов)
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
        {
            // Fisher–Yates
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int j = _rnd.Next(i + 1);
                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }

        public Card Draw()
        {
            if (_cards.Count == 0)
                return null;

            var c = _cards[0];
            _cards.RemoveAt(0);
            return c;
        }

        public int Count => _cards.Count;
    }
}
