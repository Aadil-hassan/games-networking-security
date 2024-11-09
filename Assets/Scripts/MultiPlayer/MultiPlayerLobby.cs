using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;           // Corrected namespace to Photon.Pun for multiplayer networking
using Photon.Realtime;      // Needed for handling Photon connection callbacks
using UnityEngine.UI;

public class MultiPlayerLobby : MonoBehaviourPunCallbacks
{
    // Panel references for different UI views
    public Transform LoginPanel;
    public Transform SelectionPanel;
    public Transform CreateRoomPanel;
    public Transform InsideRoomPanel;
    public Transform ListRoomsPanel;
    public GameObject textPrefab;
    public Transform insideRoomPlayerList;

    // Input field for room name
    public InputField roomNameInput;
    public InputField playerNameInput;

    private string playerName;

    private void Start()
    {
        playerName = string.Format("Player {0}", Random.Range(1, 1000000));
        if (playerNameInput != null)
        {
            playerNameInput.text = playerName;
        }
    }

    // Called when the login button is clicked
    public void LoginButtonClicked()
    {
        if (playerNameInput != null && !string.IsNullOrEmpty(playerNameInput.text))
        {
            playerName = playerNameInput.text;
        }

        PhotonNetwork.LocalPlayer.NickName = playerName;
        PhotonNetwork.ConnectUsingSettings(); // Connects to Photon servers using the predefined settings
    }

    // Method to create a new room
    public void CreateARoom()
    {
        if (roomNameInput != null && !string.IsNullOrEmpty(roomNameInput.text))
        {
            // Define room options with max players and visibility settings
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            roomOptions.IsVisible = true;

            // Create room using the name entered in the input field
            PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);
        }
        else
        {
            Debug.LogWarning("Room name input field is empty or not assigned.");
        }
    }

    // Method to activate a specified panel while hiding others
    public void ActivatePanel(string panelName)
    {
        // Deactivate all panels initially
        LoginPanel.gameObject.SetActive(false);
        SelectionPanel.gameObject.SetActive(false);
        CreateRoomPanel.gameObject.SetActive(false);
        InsideRoomPanel.gameObject.SetActive(false);
        ListRoomsPanel.gameObject.SetActive(false);

        // Activate only the specified panel
        if (panelName == LoginPanel.gameObject.name)
            LoginPanel.gameObject.SetActive(true);
        else if (panelName == SelectionPanel.gameObject.name)
            SelectionPanel.gameObject.SetActive(true);
        else if (panelName == CreateRoomPanel.gameObject.name)
            CreateRoomPanel.gameObject.SetActive(true);
        else if (panelName == InsideRoomPanel.gameObject.name)
            InsideRoomPanel.gameObject.SetActive(true);
        else if (panelName == ListRoomsPanel.gameObject.name)
            ListRoomsPanel.gameObject.SetActive(true);
    }

    // Called automatically when connected to the master server
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the master server!");
        ActivatePanel(SelectionPanel.gameObject.name); // Ensure the panel name matches
    }

    // Optional: Called when successfully joined a lobby
    public override void OnJoinedLobby()
    {
        Debug.Log("Successfully joined the lobby!");
    }

    // Called if connection fails, for debugging purposes
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from the master server!");
        ActivatePanel(LoginPanel.gameObject.name); // Use consistent naming
    }

    // Method to disconnect manually from the Photon network
    public void DisconnectButtonClicked()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room has been created!");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room: " + message);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room has been joined!");
        ActivatePanel(InsideRoomPanel.gameObject.name); // Activate InsideRoom panel

        // Clear the existing player list UI
        foreach (Transform child in insideRoomPlayerList)
        {
            Destroy(child.gameObject);
        }

        // Populate the player list UI with current players in the room
        foreach (var player in PhotonNetwork.PlayerList)
        {
            var playerListEntry = Instantiate(textPrefab, insideRoomPlayerList);
            playerListEntry.GetComponent<Text>().text = player.NickName;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left the room!");
        ActivatePanel(CreateRoomPanel.gameObject.name); // Activate CreateRoom panel
    }
}






