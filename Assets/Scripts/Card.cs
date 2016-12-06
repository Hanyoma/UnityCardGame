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

        switch(r)
        {
            case Robot.TheOriginal:
                switch(name)
                {
                    case "BLINK":
                        _move = 0;
                        _blink = 3;
                        break;
                    case "BUCKLE DOWN":
                        _move = 0;
                        _perfectMelee = true;
                        _melee = true;
                        break;
                    case "COUNTER":
                        _move = 1;
                        _melee = true;
                        _evadeMelee = true;
                        break;
                    case "DODGE":
                        _move = 1;
                        _evadeMelee = true;
                        _evadeRanged = true;
                        break;
                    case "DUCK":
                        _move = 2;
                        _evadeRanged = true;
                        break;
                    case "FLAMETHROWER":
                        _backstep = -1;
                        _move = 0;
                        _ranged = true;
                        _rangedCloseDist = 1;
                        _rangedFarDist = 2;
                        break;
                    case "LASER":
                        _move = 0;
                        _ranged = true;
                        _rangedCloseDist = 1;
                        _rangedFarDist = 3;
                        break;
                    case "LUNGE":
                        _move = 2;
                        _melee = true;
                        break;
                    case "NAIL GUN":
                        _move = 2;
                        _ranged = true;
                        _rangedCloseDist = 0;
                        _rangedFarDist = 1;
                        break;
                    case "SPRINT":
                        _move = 3;
                        break;
                }
                break;

            case Robot.DiscoFever:
                switch(name)
                {
                    case "DISCO BLAST":
                        _move = 0;
                        _evadeMelee = true;
                        _ranged = true;
                        _rangedCloseDist = -1;
                        _rangedFarDist = 1;
                        break;
                    case "ELECTRIC SHUFFLE":
                        _move = -2;
                        _blink = 4;
                        break;
                    case "JUMP":
                        _move = 0;
                        _melee = true;
                        _blink = 2;
                        break;
                    case "MOONWALK":
                        _move = 5;
                        _blink = -2;
                        break;
                    case "OVERDRIVE":
                        _move = 4;
                        // Next turn your move is 0
                        break;
                    case "SALSA SMACKDOWN":
                        _move = 3;
                        _melee = true;
                        _blink = -2;
                        break;
                    case "SIDE STEP":
                        _move = 2;
                        _evadeMelee = true;
                        break;
                    case "STUN STEP":
                        _move = 1;
                        //Your opponent moves 0
                        break;
                    case "TRIPLE STEP PART ONE":
                        _move = 1;
                        _ranged = true;
                        _rangedCloseDist = 1;
                        _rangedFarDist = 1;
                        _blink = 1;
                        break;
                    case "TRIPLE STEP PART TWO":
                        _move = 1;
                        _melee = true;
                        _blink = 1;
                        break;

                }
                break;
            }
    }
    
    public override string ToString()
    {
        return "Card: " + _robot + " " + _name;
    }
}