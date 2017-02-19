using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : Entity {

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

    void MoveCharacter() {
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
}
