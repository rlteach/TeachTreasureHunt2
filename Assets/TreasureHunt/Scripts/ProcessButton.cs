using UnityEngine;
using System.Collections;

public class ProcessButton : MonoBehaviour {

    public  delegate void    ButtonClick(PlayerController vPC);
    public  PlayerController    PC;     //Using a delegate is  storing a method to be called at at a later time

    public  ButtonClick OnClick=null;    

    public  void    OnButtonClick() {
        if(OnClick!=null) {
            OnClick(PC);      //Call Inventory items Use Code
        }
    }
}
