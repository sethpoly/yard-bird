using Patterns;
using PlayerStates;

public class PlayerFSM : FSM
{
    public PlayerFSM() : base()
    {

    }

    public void Add(PlayerFSMState state)
    {
        m_states.Add((int)state.ID, state);
    }

    public PlayerFSMState GetState(PlayerFSMStateType key)
    {
        return (PlayerFSMState)GetState((int)key);
    }

    public void SetCurrentState(PlayerFSMStateType stateKey)
    {
        State state = m_states[(int)stateKey];
        if(state != null)
        {
            SetCurrentState(state);
        }
    }
}

public class PlayerFSMState : State 
{
    public PlayerFSMStateType ID { get { return _id; } }

    protected Player _player = null;
    protected PlayerFSMStateType _id;
    public PlayerFSMState(FSM fsm, Player player) : base(fsm) 
    {
        _player = player;
    }

    public PlayerFSMState(Player player) : base(fsm: player.playerFSM)
    {
        _player = player;
        m_fsm = _player.playerFSM;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
