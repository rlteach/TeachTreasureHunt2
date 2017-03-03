using UnityEngine;
using System.Collections;


namespace RL_Helpers {
	
public class DB : MonoBehaviour {
		public	static	bool	On=true;		//Allow Debug to be turned off gloablly
		public	static	void	Message(string vText) {
			if (On) {
				Debug.Log (vText);
			}
		}
	}
}
