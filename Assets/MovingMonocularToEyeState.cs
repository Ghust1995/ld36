
using UnityEngine;

public enum MovingMonocularType
{
    FromEye,
    ToEye,
}

public class MovingMonocularState : IPlayerState
{
    private readonly MovingMonocularType _type;
    private float _percentageComplete;


    public MovingMonocularState(MovingMonocularType type)
    {
        _type = type;
    }

    private float _time = 0;
    public IPlayerState HandleInput(Player p)
    {
        if (_percentageComplete >= 1 )
        {
            switch (_type)
            {
                case MovingMonocularType.FromEye:
                    return new IdleState();
                case MovingMonocularType.ToEye:
                    return new LookingAtMonocularState();
            }
            
        }
        return null;
    }

    public void Enter(Player p)
    {
       
    }

    
    public void Update(Player p)
    {
        _time += Time.deltaTime;
        _percentageComplete = _time / p.MovineScopeToEyeAnimationTime;
        var t = _percentageComplete;
        if (_type == MovingMonocularType.FromEye)
        {
            t = 1 - t;
        }
        p.ScopeObject.transform.position = Vector3.Slerp(p.ScopeToEyeStart.position, p.ScopeToEyeEnd.position, t);
        p.ScopeObject.transform.rotation = Quaternion.Slerp(p.ScopeToEyeStart.rotation, p.ScopeToEyeEnd.rotation, t);
    }
}
