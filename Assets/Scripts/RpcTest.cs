using Unity.Netcode;
using UnityEngine;

public class RpcTest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        //Only send an RPC to the server from the client that owns the NetworkObject of this NetworkBehaviour instance
        if (!IsServer && IsOwner)
        {
            //Send an RPC to the server from the client that owns the NetworkObject of this NetworkBehaviour instance
            ServerOnlyRpc(0, NetworkObjectId);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void ClientAndHostRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Client Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");

        //Only send an RPC to the owner of the NetworkObject
        if (IsOwner)
        {
            //Send an RPC to the server from the client that owns the NetworkObject of this NetworkBehaviour instance
            ServerOnlyRpc(value + 1, sourceNetworkObjectId);
        }
    }

    [Rpc(SendTo.Server)]
    private void ServerOnlyRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Server Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");

        //Send an RPC to all clients and the host
        ClientAndHostRpc(value, sourceNetworkObjectId);
    }
}