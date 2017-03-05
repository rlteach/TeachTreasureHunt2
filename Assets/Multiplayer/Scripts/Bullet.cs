using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Multiplayer {
    public class Bullet : Entity {


		public	float	TimeToLive=2f;
		public	float	Speed=15f;

		Rigidbody	mRB;

		public	PlayerController	PC;

		public override EType Type {
			get {
				return	EType.Bullet;
			}
		}

		bool	isTarget(Entity vTarget) {		//Return true for valid bullet target
			return	(vTarget.Type == EType.RemotePlayer);
		}

		public	override	void	OnStartServer() {
			mRB = GetComponent<Rigidbody> ();
			transform.position = PC.BulletSpawn.position;
			transform.rotation = PC.BulletSpawn.rotation;
			mRB.velocity = transform.forward * Speed;	//Bullet moves forward
			Destroy (gameObject, TimeToLive);	//Bullets last limited time
		}

		public override	void	OnStartClient() {		//Ignore base startup code
		}


		protected override	void	CollidedWith(Entity vOther, bool vIsTrigger) {
			if (isTarget (vOther)) {			//Bullets should only hit player
				PlayerController	tPlayer = (PlayerController)vOther;		//We know its a player, so cast it
				Destroy (gameObject);	//Kill bullet
				tPlayer.TakeHit (1);		//Knock down player health
			}
		}
    }
}
