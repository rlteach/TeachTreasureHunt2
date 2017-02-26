using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text; //Needed for string builder
using System.Text.RegularExpressions;

namespace Multiplayer {     //Allows different code with the same names be used for different purposes, useful if used with care

    public class DebugMP : MonoBehaviour {

        [Header("Debug")]
        [Range(0.1f,5f)]
        public float UpdateSpeed=1f;

	    Text	mDebugtext;
        Coroutine mStatusCoRoutine;

	    // Use this for initialization
	    void Start () {
		    mDebugtext = GetComponent<Text> ();
            mStatusCoRoutine = StartCoroutine(UpdateStatus());      //Show status regularly
        }

        IEnumerator UpdateStatus() {
            do {
                StringBuilder tSB = new StringBuilder();        //Faster than string concat with +
                tSB.AppendLine(string.Format("Player:{0}", NetworkManager.singleton.numPlayers));
                if (NetworkServer.active) {          //Do not use Network.isServer, its broken
                    if (NetworkClient.active) {
                        tSB.AppendLine("Host");
                        tSB.AppendLine(Connections);
                    } else {
                        tSB.AppendLine("Server");
                        tSB.AppendLine(Connections);
                    }
                } else if (NetworkClient.active) {
                    tSB.AppendLine("Client");
                }
                mDebugtext.text = tSB.ToString();
                yield return new WaitForSeconds(UpdateSpeed);
            } while (true);
        }

        string Connections {
            get {
                StringBuilder tSB = new StringBuilder();        //Faster than string concat with +
                int tI = 0;
                foreach (var tConnection in NetworkServer.connections) {
                    if(tConnection!=null) {
                        tSB.AppendLine(string.Format("{0}-{1}:{2}", ++tI, GetIP4String(tConnection.address), tConnection.isConnected));
                    }
                }
                return tSB.ToString();
            }
        }

        string  GetIP4String(string vText) {
            StringBuilder tSB = new StringBuilder();        //Faster than string concat with +
            string[] sections = Regex.Split(vText, ":.");
            for(int tI=0;tI<4;tI++) {
                tSB.Append(sections[5+tI]);
                if(tI<3) {
                    tSB.Append(".");
                }
            }
            return tSB.ToString();
        }
    }
}
