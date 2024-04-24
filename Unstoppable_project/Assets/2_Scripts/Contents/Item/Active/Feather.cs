using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : Item
{
    public override void Use()
    {
        _owner.AddJump((int)Value);
    }
}
