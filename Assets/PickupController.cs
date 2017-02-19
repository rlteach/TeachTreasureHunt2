using UnityEngine;
using System.Collections;

public class PickupController : Entity {

    public override EType Type {     //get Type of Enity
        get {
            return EType.Pickup;
        }
    }
}
