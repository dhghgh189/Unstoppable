using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Item
{
    public override void Use()
    {
        theApp.Game.Score += Value;
    }
}
