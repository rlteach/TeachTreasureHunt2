using UnityEngine;
using System.Collections;
using System;

public class PickupController : Entity {

    public  enum PickupTypes {
        Water=0
        ,Food
    }

    Pickup mPickupItem;

    public PickupTypes PickupType;

    protected override void Start() {
        base.Start();
        mPickupItem=MakePickup();
    }

    private Pickup  MakePickup() {          //Creates a pickup from a list of items
        switch(PickupType) {
            case PickupTypes.Water:
                return CreatePickup<WaterPickup>();     //Create specified item
            case PickupTypes.Food:
                return CreatePickup<FoodPickup>();
        }
        return null;
    }

    private T CreatePickup<T>() where T:Pickup,new() {      //Make a Pickup Item up on the fly
        return new T();
    }

    public override EType Type {     //get Type of Enity, can by used instead of tags
        get {
            return EType.Pickup;        //Pickup Item
        }
    }

    public  Pickup    PickupItem(PlayerController vPC) {
        bool tShouldPickup = mPickupItem.PickUpItem(vPC);
        if( tShouldPickup) {
            Destroy(gameObject);        //get rid of it
            return mPickupItem;     //Pickup allowed return item 
        }
        return   null;      //Pickup not allowed
    }
}
