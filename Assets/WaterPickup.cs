using System.Collections;

public class WaterPickup : Pickup {
    public override bool AffectPlayer(PlayerController vPC) {       //Get Specific effect
        return false;   //Is this a one use item
    }
}
