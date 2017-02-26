using UnityEngine;
using System.Collections;


//Deals with GameObject collision 
public class PickupController : Entity {

	public	enum PickupTypes {		//Type of this pickup
		Water
		,Food
		,Lava
	}

	[Header("Choose pickup type")]
	public	PickupTypes	PickupType;		//Allow choice of pickup type in inspector

	// Use this for initialization
	protected override	void Start () {
		base.Start ();		//Call base class, to set up Entity	
	}

	public override EType Type {     //get Type of Enity
		get {
			return EType.Pickup;
		}
	}


    //Create new object for Modifier list
    Pickup CreatePickup() {
		switch(PickupType) {
		case	PickupTypes.Water:
			return	new WaterPickup ();		//Create Water Pickup
		case	PickupTypes.Food:
			return	new FoodPickup ();		//Create Food Pickup
		case	PickupTypes.Lava:
			return	new LavaPickup ();		//Create Lava Pickup
		}
		return	null;
	}


	//Deal with collisions
	protected   override void    Collision(Entity vOther,bool vIsTrigger) {
        if (vOther.Type == EType.Player) {      //If colliding with player
            PlayerController tPC = vOther.GetComponent<PlayerController>();
            if (tPC != null) {
                Pickup tPickup = CreatePickup();        //Create Pickup modifer
                if (tPC.AcceptPickup(tPickup)) {        //Ask player to accept it
                    Die();      //If accepted kill GameObject
                }
            }
        }
	}

	protected  override  void OnDestroy() {
		if(GM.OS!=null) {
			GM.OS.CmdRemovePickup (gameObject);		//Make sure its removed from the list
		}
	}
}
