using Fusion;

public class Player : NetworkBehaviour
{
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            transform.position += data.direction * Runner.DeltaTime * 5;
        }
    }
}
