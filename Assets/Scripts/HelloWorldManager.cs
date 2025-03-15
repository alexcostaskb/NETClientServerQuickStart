using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    /// <summary>
    /// A simple NetworkBehaviour that moves the object to a random position on the plane
    /// </summary>
    public class HelloWorldManager : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private NetworkManager m_NetworkManager;

        private void Awake()
        {
            // Get the NetworkManager instance
            m_NetworkManager = GetComponent<NetworkManager>();
        }

        private void OnGUI()
        {
            // Create a GUI area
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));

            if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
            {
                // the NetworkManager is not a client or server

                // Display the start buttons
                DisplayStartButtons();
            }
            else
            {
                // the NetworkManager is a server

                // Display the status labels
                DisplayStatusLabels();

                // display the submit new position button
                DisplaySubmitNewPositionButton();
            }

            // End the GUI area
            GUILayout.EndArea();
        }

        private void DisplayStartButtons()
        {
            // Display the start buttons

            if (GUILayout.Button("Host"))
            {
                m_NetworkManager.StartHost();
            }

            if (GUILayout.Button("Client"))
            {
                m_NetworkManager.StartClient();
            }

            if (GUILayout.Button("Server"))
            {
                m_NetworkManager.StartServer();
            }
        }

        private void DisplayStatusLabels()
        {
            // Display the transport and mode
            var mode = m_NetworkManager.IsHost ? "Host" : m_NetworkManager.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " + m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        private void DisplaySubmitNewPositionButton()
        {
            // Display the submit new position button
            if (GUILayout.Button(m_NetworkManager.IsServer ? "Move" : "Request Position Change"))
            {
                if (m_NetworkManager.IsServer && !m_NetworkManager.IsClient)
                {
                    // If the NetworkManager is a server and not a client

                    foreach (ulong uid in m_NetworkManager.ConnectedClientsIds)
                    {
                        // move the player object of each client connected to the server to a random position on the plane
                        m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().Move();
                    }
                }
                else
                {
                    // If the NetworkManager is a client or a host

                    // Get the local player object
                    var playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();
                    var player = playerObject.GetComponent<HelloWorldPlayer>();

                    // Move the player
                    player.Move();
                }
            }
        }
    }
}