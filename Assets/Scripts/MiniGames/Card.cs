using System;
using System.Collections.Generic;

public enum Rank
{
    Ace,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King
}

public enum Suit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

public class Card
{
    public Rank Rank { get; set; }
    public Suit Suit { get; set; }
    public int PointValue
    {
        get
        {
            if (Rank == Rank.Ace)
            {
                return 1;
            }
            else if (Rank == Rank.Jack || Rank == Rank.Queen || Rank == Rank.King)
            {
                return 10;
            }
            else
            {
                return (int)Rank;
            }
        }
    }

    public Card(Rank rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public override string ToString()
    {
        return Rank + " of " + Suit;
    }
}

public class Deck
{
    private List<Card> cards;
    private int nextCardIndex;

    public Deck()
    {
        cards = new List<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(rank, suit));
            }
        }

        nextCardIndex = 0;
    }

    public void Shuffle()
    {
        nextCardIndex = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, cards.Count);
            Card temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public Card Deal()
    {
        Card card = cards[nextCardIndex];
        nextCardIndex++;

        if (nextCardIndex >= cards.Count)
        {
            Shuffle();
        }

        return card;
    }
}
