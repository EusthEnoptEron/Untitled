﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour {

    public Camera Camera;
    public GameObject[] Cars;
    int SelectedCar = 0;
    public InputField NameInputField;
    public string EnteredName;
    public string GameName;
    string TypeName = "BFH.GameDev.SplashDash";
    private const string GAME_NAME = "Splash Dash";
    string ServerName;
    private HostData[] hostList;
    public GameObject CreateServerCanvas;
    public GameObject OldCanvas;
    public InputField serverNameInput;
   


    public string Name;
    private bool displayGui;
	// Use this for initialization
	void Start () {

        MasterServer.RequestHostList(TypeName);
        GamePresets.car = Cars[SelectedCar];
	
	}
	
	// Update is called once per frame
	void Update () {
        Camera.transform.position = new Vector3(Mathf.Lerp(Camera.transform.position.x, Cars[SelectedCar].transform.position.x, Time.deltaTime*4), Camera.transform.position.y, Camera.transform.position.z);
	}

    public void OnNextCar()
    {
        if (SelectedCar != 0)
        {
            SelectedCar--;
        }
        GamePresets.car = Resources.Load<GameObject>("Prefabs/Cars/pref_Car" + (SelectedCar + 1));


    }

    public void OnPrevCar()
    {



        if (SelectedCar != Cars.Length - 1)
        {
            SelectedCar++;
        }
        GamePresets.car = Resources.Load<GameObject>("Prefabs/Cars/pref_Car" + (SelectedCar + 1));
    }



    public void OnNameChanged()
    {
        if (NameInputField.text != "")
        {
            GamePresets.PlayerName = NameInputField.text;  
        }
        
        
    }


    public void StartAsServer()
    {
        GamePresets.joinServer = false;
    }

    //private void StartServer()
    //{
    //    Network.InitializeServer(32, 25000, !Network.HavePublicAddress());
    //    MasterServer.RegisterHost(TypeName, GameName);
    //}

    private void connectToServer()
    {
        GamePresets.joinServer = true;
    }



    public void OnBackToMenü()
    {
        Application.LoadLevel("Menü");
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }

    public void OnLobbyPlay() {


        CreateServerCanvas.SetActive(true);
        OldCanvas.SetActive(false);

    }




    //public static bool IsConnected
    //{
    //    get
    //    {
    //        return Network.isClient || Network.isServer;
    //    }
    //}

    //void ONGui(){
        

    //        if (GUILayout.Button("refresh hosts"))
    //            MasterServer.RequestHostList(GamePresets.serverName);
    //        if (hostList != null)
    //        {
    //            for (int i = 0; i < hostList.Length; i++)
    //            {
    //                if (GUILayout.Button(hostList[i].gameName))
    //                    Network.Connect(hostList[i]);
    //            }
    //        }
        
    //}


    public void OnGetHostList()
    {
        MasterServer.RequestHostList(TypeName);


        if (hostList != null)
        {
            displayGui = true;
        }

    }


    void OnGUI()
    {
        if (displayGui)
        {
            for (int i = 0; i < hostList.Length; i++)
            {
                if (GUILayout.Button(hostList[i].gameName))
                {
                    GamePresets.joinServer = true;
                    GamePresets.host = hostList[i];
                    //GamePresets.serverName = hostList.
                    //Network.Connect(hostList[i]);
                    Application.LoadLevel("Track 2");
                }

                // PREFAB
                //var prefab = Resources.Load<GameObject>("Prefabs/Cars/pref_Car" + (index + 1) );

            }
        }
        
    }


    public void OnServerNameInput()
    {
        if (serverNameInput.text.Length != 0)
        {
            GamePresets.serverName = serverNameInput.text;
        }
        
    }

    public void OnServerInputClose()
    {
        CreateServerCanvas.SetActive(false);
        OldCanvas.SetActive(true);

    }

    public void OnStartGame()
    {
        if (GamePresets.serverName != "")
        {
            Application.LoadLevel("Track 2");
        }
    }
   


}
