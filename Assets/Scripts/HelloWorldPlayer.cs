using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    /// <summary>
    /// A simple NetworkBehaviour that moves the object to a random position on the plane
    /// </summary>
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                // Only the owner of the object can move it

                Move();
            }
        }

        public void Move()
        {
            SubmitPositionRequestRpc();
        }

        [Rpc(SendTo.Server)]
        private void SubmitPositionRequestRpc(RpcParams rpcParams = default)
        {
            // Server will call this method on the client
            var randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            Position.Value = randomPosition;
        }

        private static Vector3 GetRandomPositionOnPlane()
        {
            // Random position on a plane
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        private void Update()
        {
            // move the object to the position received from the server
            transform.position = Position.Value;
        }
    }
}