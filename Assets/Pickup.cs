using System.Collections;
using System.Collections.Generic;

abstract public class Pickup  {

    string mPickupName;       //Name of Object

    private Pickup mPickup;     //Reference for this Pickup object

    public  string    Name {
        get {                   //Allow everyone to read it
            return mPickupName;
        }
        protected   set {       //Only allow derived classes to change it
            mPickupName = value;
        }
    }

    //Allow access to Pickup Item
    public  Pickup  PickupItem {
        get {
            return mPickup;
        }
        protected   set {
            mPickup = value;
        }
    }

    public virtual bool PickUpItem(PlayerController vPC) {       //Implement pickup effect on player, return true if this should be added to inventory
        GM.DebugMsg("PickUpItem:" + Name);
        return true;
    }
    public virtual  void UseItem(PlayerController vPC) {             //Implement effects on player
        GM.DebugMsg("Use Item:" + Name);
    }
}
