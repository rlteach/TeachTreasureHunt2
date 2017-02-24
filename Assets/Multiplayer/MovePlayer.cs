using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MovePlayer : NetworkBehaviour {


	public	GameObject	BulletPrefab;
	public	Transform	BulletSpawn;

	// Use this for initialization
	void Start () {
	}

	public override void OnStartLocalPlayer() {
		gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
		name += "Local";
	}

	// Update is called once per frame
	void Update () {
		if (isLocalPlayer) {
			DoMovePlayer ();
			if (Input.GetKeyUp (KeyCode.Space)) {
				CmdDoFire ();
			}
		}
	}
	void	DoMovePlayer() {
		float	tRotate = Input.GetAxis("Horizontal")*360f*Time.deltaTime;
		float	tMove = Input.GetAxis("Vertical")*10f*Time.deltaTime;
		transform.Rotate(0,tRotate,0);
		transform.position+=transform.TransformDirection (Vector3.forward*tMove);
	}

	[Command]
	void	CmdDoFire() {
		Debug.Log (name);
		var tBullet=Instantiate (BulletPrefab,BulletSpawn.position,BulletSpawn.rotation) as GameObject;
		var	tRB = tBullet.GetComponent<Rigidbody> ();
		tRB.velocity = tBullet.transform.forward * 3f;
		Destroy (tBullet, 2f);
		tBullet.name = name+"Bullet";
		NetworkServer.Spawn(tBullet);
	}
}
