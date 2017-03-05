using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Multiplayer {

    public class Pickup : Entity {

    public  Texture2D TextureSheet;
        public override EType Type {
            get {
                return EType.Pickup;
            }
        }
    Renderer mRenderer;

    public int PickupType {
        set {
            mType = value % 7;
            float tTiling = 1f / 3f;
            float tX = tTiling * (mType % 3);
            float tY = (tTiling * 2f) - (tTiling * (mType / 3));
            mRenderer.material.SetTextureOffset("_MainTex", new Vector2(tX, tY));
        }
        get {
            return mType;
        }
    }

   int mType;

    // Use this for initialization
    void Start () {
            mRenderer = GetComponent<MeshRenderer>();
            mRenderer.material = new Material(Shader.Find("Standard"));
            mRenderer.material.mainTexture = TextureSheet;
            mRenderer.material.SetTextureScale("_MainTex", new Vector2(0.33f, 0.33f));
            PickupType = Random.Range(0, 1000) ;
        }
    }
}
