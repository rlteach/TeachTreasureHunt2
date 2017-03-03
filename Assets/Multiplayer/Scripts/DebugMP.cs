using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text; //Needed for string builder
using System.Net;

namespace Multiplayer {     //Allows different code with the same names be used for different purposes, useful to split up projects into logical components

    public class DebugMP : MonoBehaviour {

        [Header("Debug")]
        [Range(0.1f,5f)]
        public float UpdateSpeed=1f;

	    Text	mDebugtext;

	    // Use this for initialization
	    void Start () {
		    mDebugtext = GetComponent<Text> ();
           StartCoroutine(UpdateStatus());      //Show status regularly
        }

        IEnumerator UpdateStatus() {
            do {
                StringBuilder tSB = new StringBuilder();        //Faster than string concat with +
                tSB.AppendLine(string.Format("Player:{0}", NetworkManager.singleton.numPlayers));
                if (NetworkServer.active) {          //Do not use Network.isServer, its broken
                    if (NetworkClient.active) {
                        tSB.AppendLine("Host");
                        tSB.AppendLine(ClientConnections);
                    } else {
                        tSB.AppendLine("Server");
                        tSB.AppendLine(ClientConnections);
                    }
                } else if (NetworkClient.active) {
                    tSB.AppendLine("Client");
					tSB.AppendLine(ServerInfo);
				} else {
					tSB.AppendLine("Idle");
				}
                mDebugtext.text = tSB.ToString();
                yield return new WaitForSeconds(UpdateSpeed);
            } while (true);	//Will loop forever
        }

		string ServerInfo {	//Get connected server info
			get {
				StringBuilder tSB = new StringBuilder();        //Faster than string concat with +
				tSB.AppendLine(NetworkManager.singleton.client.connection.address);
				return	tSB.ToString();
			}
		}


        string ClientConnections {		//Get client connections by IP
            get {
                StringBuilder tSB = new StringBuilder();        //Faster than string concat with +
                int tI = 0;
                foreach (var tConnection in NetworkServer.connections) {
                    if(tConnection!=null) {
						string	tRemote = GetIPAddress(tConnection.address);
						tSB.AppendLine(string.Format("{0}-{1}:{2}", ++tI, tRemote, (tConnection.isConnected)?"-C":"-NC"));
                    }
                }
                return tSB.ToString();
            }
        }

        string  GetIPAddress(string vText) {		//Messy way to split IP address, but only used for debug
			char[]	tDemlimiter = { ':', '.' };
			string[]	tSplit = vText.Split (tDemlimiter);
			int tCount = (tSplit != null) ? tSplit.Length : 0;
			if (tCount == 7) {
				return	string.Format ("{0}.{1}.{2}.{3}", tSplit [3], tSplit [4], tSplit [5], tSplit [6]);
			}
			return	"N/A";
        }
    }
}
