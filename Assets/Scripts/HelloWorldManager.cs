using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
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

            GUILayout.EndArea();
        }

        private void DisplayStartButtons()
        {
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
            var mode = m_NetworkManager.IsHost ? "Host" : m_NetworkManager.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " + m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        private void DisplaySubmitNewPositionButton()
        {
            if (GUILayout.Button(m_NetworkManager.IsServer ? "Move" : "Request Position Change"))
            {
                if (m_NetworkManager.IsServer && !m_NetworkManager.IsClient)
                {
                    foreach (ulong uid in m_NetworkManager.ConnectedClientsIds)
                    {
                        m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().Move();
                    }
                }
                else
                {
                    var playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();
                    var player = playerObject.GetComponent<HelloWorldPlayer>();
                    player.Move();
                }
            }
        }
    }
}