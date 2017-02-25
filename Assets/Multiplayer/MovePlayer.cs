using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MovePlayer : NetworkBehaviour {

	public	GameObject	BulletPrefab;
	public	Transform	BulletSpawn;

	Vector3		mStartPosition=Vector3.zero;
	Quaternion	mStartRotation=Quaternion.identity;


	// Use this for initialization
	void Start () {
	}

	public	override	void	OnStartClient() {
		if (!isLocalPlayer) {
			gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
			name = "Remote Player";
		} else {
			name = "Local Player";
		}
		Debug.Log ("OnStartClient():"+name);
	}

	public override void OnStartLocalPlayer() {
		gameObject.GetComponent<MeshRenderer> ().material.color = Color.blue;
		mStartPosition = transform.position;
		mStartRotation = transform.rotation;
	}

	public	void	ResetPlayer() {
		transform.position=mStartPosition;
		transform.rotation=mStartRotation;
	}

	// Update is called once per frame
	void Update () {
		if (isLocalPlayer) {
			DoMovePlayer ();
			if (IC.GetInput(IC.Directions.Fire)>0f) {
				CmdDoFire ();
			}
		}
	}
	void	DoMovePlayer() {
		float	tRotate = IC.GetInput(IC.Directions.MoveX)*90f*Time.deltaTime;
		float	tMove = IC.GetInput(IC.Directions.MoveY)*10f*Time.deltaTime;
		transform.Rotate(0,tRotate,0);
		transform.position+=transform.TransformDirection (Vector3.forward*tMove);
	}



	[Command]
	void	CmdDoFire() {
		Debug.Log (name);
		var tBullet=Instantiate (BulletPrefab,BulletSpawn.position,BulletSpawn.rotation) as GameObject;
		var	tRB = tBullet.GetComponent<Rigidbody> ();
		tRB.velocity = tBullet.transform.forward * 10f;
		Destroy (tBullet, 2f);
		tBullet.name = name+"Bullet";
		NetworkServer.Spawn(tBullet);
	}
}
