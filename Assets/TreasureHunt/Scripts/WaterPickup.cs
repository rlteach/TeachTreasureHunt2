using UnityEngine;
using System.Collections;

public class WaterPickup : Pickup {

	//Water is one shot, once its added to the player it schedules its own deletion
	public	override	void	UpdatePlayer(PlayerController tPlayer,float tTime) {
		tPlayer.Water += 10;
		Delete = true;
	}

	public override string Name {
		get {
			return	"Water";
		}
	}
}
