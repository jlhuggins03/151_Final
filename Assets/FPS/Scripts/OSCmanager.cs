using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************** use UnityOSC namespace...
using UnityOSC;
//*************

using Unity.FPS.Game;
using Unity.FPS.AI;

public class OSCmanager : MonoBehaviour
{
    // variables
    public float gunHeatValue = 0;
    public int bossfight = 0;

    public GameFlowManager gameFlow;
    public EnemyTurret ET;

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true; //allows unity to update when not in focus

        //************* Instantiate the OSC Handler...
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/start", 1);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/playmusic", 1);
        //*************
    }

    // Update is called once per frame
    void Update()
    {
        // variables to export
        gunHeatValue = gameFlow.ammoRatio;

        if (ET.AiState == EnemyTurret.AIState.Attack)
        {
            bossfight = 1;
        } else
        {
            bossfight = 0;
        }

        Debug.Log("Heat Ratio: " + gunHeatValue);
        Debug.Log("Bossfight: " + bossfight);



        //************* Routine for receiving the OSC...
        OSCHandler.Instance.UpdateLogs();
        Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
        servers = OSCHandler.Instance.Servers;

        foreach (KeyValuePair<string, ServerLog> item in servers)
        {
            // If we have received at least one packet,
            // show the last received from the log in the Debug console
            if (item.Value.log.Count > 0)
            {
                int lastPacketIndex = item.Value.packets.Count - 1;
            }
        }
        //*************

        // exporting OSC messages
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/heatValue", gunHeatValue);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/bossFight", bossfight);

    }
}
