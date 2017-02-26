using UnityEngine;
using System.Collections;
namespace SinglePlayer {
    public class FoodPickup : Pickup {

        float mTime;

        int mCount = 10;

        //Food is added over 10 seconds
        public override void UpdatePlayer(PlayerController tPlayer, float tTime) {
            if (mTime >= 1f) {
                mTime = 0;
                tPlayer.Food += 1;
                mCount--;
                if (mCount <= 0)
                    Delete = true;
            } else {
                mTime += tTime;
            }
        }

        public override string Name {
            get {
                return "Food";
            }
        }
    }
}