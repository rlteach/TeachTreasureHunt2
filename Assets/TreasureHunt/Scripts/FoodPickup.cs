using System.Collections;

public class FoodPickup : Pickup {


    int Value = 15;


    public FoodPickup() {
        Name = "Food";
     }

    public override string ToString() {
        return string.Format("{0} {1}", Value,Name);
    }

    public override bool PickUpItem(PlayerController vPC) {       //Get Specific effect
        bool tAllowed = (vPC.CountInventory<FoodPickup>() < 3);
        if(!tAllowed) {
            GM.DebugText = "Only allowed 3 food";
        }
        return (tAllowed);        //Can only carry 3 food
    }

    public override void UseItem(PlayerController vPC) {             //Implement effects on player
        vPC.Food += Value;        //Add 10 to players water
        vPC.Water -= 1;     //Eating makes you thirsty
        vPC.Inventory.Remove(this);     //Delete after use
    }

}
