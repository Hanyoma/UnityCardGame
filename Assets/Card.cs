using System;

public class Card {

    // Private members
    private Robot _robot;
    private string _name;

    public enum Robot
    {
        TheOriginal=0, DiscoFever, Null
    }

    // Accessors
    public Robot robot
    {
        get { return _robot; }
    }
    public string name
    {
        get { return _name; }
    }

    // Constructors
    public enum Suit
    {
        Heart = 0, Diamond, Club, Spade
    }

    public Card()
    {
        _robot = Robot.Null;
        _name = "";
    }

    public Card(Robot r, string name)
    {
        _robot = r;
        _name = name;
    }

    public Card(Card _rhs)
    {
        _robot = _rhs.robot;
        _name = _rhs.name;
    }

    public override string ToString()
    {
        return "Card: " + _robot + " " + _name;
    }
}
