using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : Entity {

	int mWater=3;
	int mFood = 3;


	bool	mDead=false;

	float	mConsumeTick=0f;

	public	bool	Dead {
		get {
			return	mDead;
		}
		set {
			mDead = value;
			if (mDead) {
				GM.DebugText = "You are dead";
			}
		}
	}

	public int Water {
		get {
			return	mWater;
		}
		set {
			mWater = value;
			if (mWater < 0) {
				mWater = 0;
				Dead = true;
			}
		}
	}

	public int Food {
		get {
			return	mFood;
		}
		set {
			mFood = value;
			if (mFood < 0) {
				mFood = 0;
				Dead = true;
			}
		}
	}

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
		Consume (1f);
    }

	void	Consume(float vSpeed) {
		if (mConsumeTick < 5f) {
			mConsumeTick +=Time.deltaTime*vSpeed;
		} else {
			Food--;
			Water--;
			mConsumeTick = 0;
		}
	}

    void MoveCharacter() {          //Move Character with controller
		if (!Dead) {
			if (mCC.isGrounded) {
				transform.Rotate (0, IC.GetInput (IC.Directions.MoveX), 0);
				mMoveDirection.x = 0f;
				mMoveDirection.y = 0f;
				mMoveDirection.z = IC.GetInput (IC.Directions.MoveY);
				mMoveDirection = transform.TransformDirection (mMoveDirection);      //Move in direction character is facing
				mMoveDirection *= MoveSpeed;
				if (IC.GetInput (IC.Directions.Jump) > 0f) {
					mMoveDirection.y = 10f;        //Jump
					Consume (3f);
				}
			}
			mMoveDirection.y += Physics.gravity.y * Time.deltaTime;
			mCC.Move (mMoveDirection * Time.deltaTime);
		}
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
