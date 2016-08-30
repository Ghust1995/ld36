using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class IdleState : IPlayerState
{
    public IPlayerState HandleInput(Player p)
    {
        if (Input.GetMouseButtonDown(1))
            return new MovingMonocularState(MovingMonocularType.ToEye);
        return null;
    }

    public void Enter(Player p)
    {
        return;
    }

    public void Update(Player p)
    {
        return;
    }
}
