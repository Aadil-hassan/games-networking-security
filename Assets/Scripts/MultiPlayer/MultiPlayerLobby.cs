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

    // Input field for room name
    public InputField roomNameInput;

    // Method to create a new room
    public void CreateARoom()
    {
        // Define room options with max players and visibility settings
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;

        // Create room using the name entered in the input field
        PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);
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

    // Method called when the login button is clicked, initiating the connection
    public void LoginButtonClicked()
    {
        PhotonNetwork.ConnectUsingSettings();  // Connects to Photon servers using the predefined settings
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

    public ovveride void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room!");
    }

    public ovveride void OnJoinedRoom()
    {
        Debug.Log("Room has been joined!");
        ActivatePanel("InsideRoom");
    }

    public void CreateARoom()
    {
        RoommOptions roomOptions = new RoommOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Room has been joined!");
        ActivatePanel("CreateRomm");

    }

}



