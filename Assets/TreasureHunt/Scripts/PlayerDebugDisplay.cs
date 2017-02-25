using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text; //needed for String builder

public class PlayerDebugDisplay : MonoBehaviour {

    Text mText;

    PlayerController mPC;
    public PlayerController PC {
        set {
            mPC = value;
        }
        get {
            return mPC;
        }
    }

	// Use this for initialization
	void Start () {
        mText = GetComponentInChildren<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
        StringBuilder tSB = new StringBuilder();
        tSB.Append(string.Format("{0:f2}\n",PC.name));

        foreach(Pickup tP in PC.Modifiers) {
            tSB.Append(tP.Name);
            if(PC.Modifiers.Count>1) {      //Only add newline if not last
                if(tP!=PC.Modifiers[PC.Modifiers.Count-1]) {
                    tSB.Append("\n");
                }
            }
        }
        mText.text = tSB.ToString();
    }
}
