public class EmptyHandState : HandFSMState
{
    public EmptyHandState(Hand hand): base(hand)
    {
        _id = HandStates.HandFSMStateType.EMPTY;
    }

     public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // Add state logic

        // Check exit object type conformance

        // Perform exits from type checks
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
