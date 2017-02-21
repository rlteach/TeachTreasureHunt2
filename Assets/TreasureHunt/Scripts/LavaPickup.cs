using UnityEngine;
using System.Collections;

public class LavaPickup : Pickup {

	float	mTime;

	int		mCount=10;

	bool	mStarted=false;

	//http://tinyurl.com/hx4zep9


	//Food is added over 10 seconds
	public	override	void	UpdatePlayer(PlayerController tPlayer,float tTime) {
		if (mStarted) {
			if (mTime >= 1f) {
				mTime = 0;
				mCount--;
				if (mCount <= 0) {
					tPlayer.JumpHeight = -1;
					Delete = true;
				}
			} else {
				mTime += tTime;
			}
		} else {
			mStarted = true;
			tPlayer.JumpHeight = 25f;
		}
	}

	public override string Name {
		get {
			return	"Lava";
		}
	}
}
