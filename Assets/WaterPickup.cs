using System.Collections;

public class WaterPickup : Pickup {

    int Value=10;

    public  WaterPickup() {
        Name = "Water";
     }

    public override string ToString() {     //Nice formatted item string
        return string.Format("{0} {1}", Value, Name);
    }

    public override bool PickUpItem(PlayerController vPC) {       //Get Specific effect
        bool tAllowed = (vPC.CountInventory<WaterPickup>() < 2);
        if (!tAllowed) {
            GM.DebugText = "Only allowed 2 water";
        }
        return (tAllowed);        //Can only carry 3 food
    }




    public override void UseItem(PlayerController vPC) {             //Implement effects on player
        vPC.Water += Value;        //Add 10 to players water
        vPC.Inventory.Remove(this);     //Delete after use
    }
}
