using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Robot {

    private Card.Suit _type;
    private List<Card> _hand;
    private int _robotLoc;
    
    public Robot(Card.Suit type)
    {
        _type = type;
        _robotLoc = 0;
    }
}
