using Fusion;
using UnityEngine;

public class NetworkLevelView : NetworkBehaviour
{
    public override void Spawned()
    {
        if(Runner.IsServer)
            transform.rotation = Quaternion.Euler(0, 180f, 0);
    }
}
