using Patterns;
using HandStates;

public class HandFSM : FSM
{
    public HandFSM() : base()
    {

    }

    public void Add(HandFSMState state)
    {
        m_states.Add((int)state.ID, state);
    }

    public HandFSMState GetState(HandFSMStateType key)
    {
        return (HandFSMState)GetState((int)key);
    }

    public void SetCurrentState(HandFSMStateType stateKey)
    {
        State state = m_states[(int)stateKey];
        if(state != null)
        {
            SetCurrentState(state);
        }
    }
}

public class HandFSMState : State 
{
    public HandFSMStateType ID { get { return _id; } }

    protected Hand _hand = null;
    protected HandFSMStateType _id;
    public HandFSMState(FSM fsm, Hand hand) : base(fsm) 
    {
        _hand = hand;
    }

    public HandFSMState(Hand hand) : base(fsm: hand.handFSM)
    {
        _hand = hand;
        m_fsm = _hand.handFSM;
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
