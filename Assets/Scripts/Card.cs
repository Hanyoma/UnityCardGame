using System;

public class Card
{

    // Private members
    private Robot _robot;
    private string _name;

    private int _backstep;
    private int _move;
    private int _blink;
    private bool _melee;
    private bool _ranged;
    private bool _evadeMelee;
    private bool _evadeRanged;
    private bool _perfectMelee;
    private int _rangedCloseDist;
    private int _rangedFarDist;

    public enum Robot
    {
        TheOriginal = 0, DiscoFever, Null
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
    public int move
    {
        get { return _move; }
    }
    public int backstep
    {
        get { return _backstep; }
    }
    public int blink
    {
        get { return _blink; }
    }
    public int rangedCloseDist
    {
        get { return _rangedCloseDist; }
    }
    public int rangedFarDist
    {
        get { return _rangedFarDist; }
    }
    public bool melee
    {
        get { return _melee; }
    }
    public bool ranged
    {
        get { return _ranged; }
    }

    public bool perfectMelee
    {
        get { return _perfectMelee; }
    }
    public bool evadeMelee
    {
        get { return _evadeMelee; }
    }
    public bool evadeRanged
    {
        get { return _evadeRanged; }
    }


    //Constructors
    public Card()
    {
        _robot = Robot.Null;
        _name = "";
    }

    public Card(Robot r, string name)
    {
        _robot = r;
        _name = name;

        _backstep = 0;
        _move = 0;
        _blink = 0;
        _melee = false;
        _ranged = false;
        _evadeMelee = false;
        _evadeRanged = false;
        _perfectMelee = false;
        _rangedCloseDist = 0;
        _rangedFarDist = 0;

        if (r == Robot.TheOriginal)
        {
            if (name.Equals("BLINK"))
            {
                _move = 0;
                _blink = 3;
            }
            else if (name.Equals("BUCKLE DOWN"))
            {
                _move = 0;
                _perfectMelee = true;
                _melee = true;
            }
            else if (name.Equals("COUNTER"))
            {
                _move = 1;
                _melee = true;
                _evadeMelee = true;
            }
            else if (name.Equals("DODGE"))
            {
                _move = 1;
                _evadeMelee = true;
                _evadeRanged = true;
            }
            else if (name.Equals("DUCK"))
            {
                _move = 2;
                _evadeRanged = true;
            }
            else if (name.Equals("FLAMETHROWER"))
            {
                _backstep = -1;
                _move = 0;
                _ranged = true;
                _rangedCloseDist = 1;
                _rangedFarDist = 2;
            }
            else if (name.Equals("LASER"))
            {
                _move = 0;
                _ranged = true;
                _rangedCloseDist = 1;
                _rangedFarDist = 3;
            }
            else if (name.Equals("LUNGE"))
            {
                _move = 2;
                _melee = true;
            }
            else if (name.Equals("NAIL GUN"))
            {
                _move = 2;
                _ranged = true;
                _rangedCloseDist = 0;
                _rangedFarDist = 1;
            }
            else if (name.Equals("SPRINT"))
            {
                _move = 3;
            }
        }
        else if (r == Robot.DiscoFever)
        {
            if (name.Equals("DISCO BLAST"))
            {
                _move = 0;
                _evadeMelee = true;
                _ranged = true;
                _rangedCloseDist = -1;
                _rangedFarDist = 1;
            }
            else if (name.Equals("ELECTRIC SHUFFLE"))
            {
                _move = -2;
                _blink = 4;
            }
            else if (name.Equals("JUMP"))
            {
                _move = 0;
                _melee = true;
                _blink = 2;
            }
            else if (name.Equals("MOONWALK"))
            {
                _move = 5;
                _blink = -2;
            }
            else if (name.Equals("OVERDRIVE"))
            {
                _move = 4;
                // Next turn your move is 0
            }
            else if (name.Equals("SALSA SMACKDOWN"))
            {
                _move = 3;
                _melee = true;
                _blink = -2;
            }
            else if (name.Equals("SIDE STEP"))
            {
                _move = 2;
                _evadeMelee = true;
            }
            else if (name.Equals("STUN STEP"))
            {
                _move = 1;
                //Your opponent moves 0
            }
            else if (name.Equals("TRIPLE STEP PART ONE"))
            {
                _move = 1;
                _ranged = true;
                _rangedCloseDist = 1;
                _rangedFarDist = 1;
                _blink = 1;
            }
            else if (name.Equals("TRIPLE STEP PART TWO"))
            {
                _move = 1;
                _melee = true;
                _blink = 1;
            }
        }
    }

    public Card(Card _rhs) : this(_rhs.robot, _rhs.name)
    {

    }

    public override string ToString()
    {
        return "Card: " + _robot + " " + _name;
    }
}