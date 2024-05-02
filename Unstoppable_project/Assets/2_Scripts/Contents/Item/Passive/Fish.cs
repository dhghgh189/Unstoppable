using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Item
{
    public override void Use()
    {
        theApp.Game.Score += (int)Value;
        Vector3 pos = theApp.Game.Player.transform.position;
        theApp.Game.GenerateScoreEffect(pos, (int)Value);
    }
}
