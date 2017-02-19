using System.Collections;
using System.Collections.Generic;

abstract public class Pickup  {

    string mPickupName;       //Name of Object

    public  string    Name {
        get {                   //Allow everyone to read it
            return mPickupName;
        }
        protected   set {       //Only allow derived classes to change it
            mPickupName = value;
        }
    }
    public abstract bool AffectPlayer(PlayerController vPC);       //Implement effects on player
}
