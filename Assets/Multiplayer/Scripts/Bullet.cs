using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Multiplayer {
    public class Bullet : Entity {

		public override EType Type {
			get {
				return	EType.Bullet;
			}
		}

		bool	isTarget(Entity vTarget) {		//Return true for valid bullet target
			return	(vTarget.Type == EType.RemotePlayer || vTarget.Type == EType.LocalPlayer);
		}

		protected override	void	CollidedWith(Entity vOther, bool vIsTrigger) {
			base.CollidedWith (vOther, vIsTrigger);		//Print debug
			if(isTarget(vOther)) {			//Bullets should only hit player
				Player	tPlayer=(Player)vOther;		//We know its a player, so cast it
				Destroy (gameObject);	//Kill bullet
				tPlayer.TakeHit(1);		//Knock down player health
			}
		}
    }
}
