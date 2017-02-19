using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : Entity {

    public int Water=0;
    public int Food = 0;

    //Set the speed of movement in inspector;
    public float MoveSpeed=10f;

    CharacterController mCC;

    Vector3 mMoveDirection = Vector3.zero;

    List<Pickup> mPickups = new List<Pickup>();     //List of stuff player has picked up

    public override EType Type {     //get Type of Enity
        get {
            return EType.Player;
        }
    }

    public List<Pickup> Inventory {       //Expose Pickup list
        get {
            return mPickups;
        }
    }

    protected override  void Start () {
        base.Start();       //Process base class Startup
        mCC = GetComponent<CharacterController>();
        GM.AddPlayer(this);     //Add to player list
	}
	
	// Update is called once per frame
	void Update () {
        MoveCharacter();
    }

    void MoveCharacter() {          //Move Character with controller
        if (mCC.isGrounded) {
            transform.Rotate(0, IC.GetInput(IC.Directions.MoveX), 0);
            mMoveDirection.x = 0f;
            mMoveDirection.y = 0f;
            mMoveDirection.z = IC.GetInput(IC.Directions.MoveY);
            mMoveDirection = transform.TransformDirection(mMoveDirection);      //Move in direction character is facing
            mMoveDirection *= MoveSpeed;
            if (IC.GetInput(IC.Directions.Jump) > 0f) {
                mMoveDirection.y = 10f;        //Jump
            }
        }
        mMoveDirection.y += Physics.gravity.y * Time.deltaTime;
        mCC.Move(mMoveDirection * Time.deltaTime);
    }

    protected override void Collision(Entity vOther, bool vIsTrigger) {     //This means Player collided
        if(vOther.Type==EType.Pickup) {
            PickupObject(vOther);
        }
    }

    void PickupObject(Entity vOther) {
        if (vOther.GetType().IsAssignableFrom(typeof(PickupController))) {       //Check if its the right type
            PickupController tPickupController = (PickupController)vOther;      //We know its a pickup so this gives us access to its code
            Pickup  tPickup= tPickupController.PickupItem(this);
            if(tPickup!=null) {     //If pickup not allowed this is null
                Inventory.Add(tPickup);    //Add the pickup Item to inventory
            }
        }
    }

    public  int CountInventory<T>() where T : Pickup {
        int tCount = 0;
        foreach (var tP in Inventory) {
            if(tP.GetType()==typeof(T)) {
                tCount++;
            }
        }
        return tCount;
    }
}
