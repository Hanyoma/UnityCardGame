using System;

public class Card {

    // Private members
    private Suit _suit;
    private int _value;

    // Accessors
    public Suit suit
    {
        get { return _suit; }
    }
    public int value
    {
        get { return _value; }
    }

    // Constructors
    public enum Suit
    {
        Heart = 0, Diamond, Club, Spade
    }

    public Card(Suit s, int val)
    {
        _suit = s;
        _value = val;
    }

    public Card(Card _rhs)
    {
        _suit = _rhs.suit;
        _value = _rhs.value;
    }

    public override string ToString()
    {
        return "Card: " + _suit + " " + _value;
    }
}
