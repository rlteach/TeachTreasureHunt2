﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using RL_Helpers;

namespace Multiplayer {		//Networking version of entiy base class

	abstract	public class Entity : NetworkBehaviour {

		public	enum EType {
			None=0
			,ClientPlayer
			,LocalPlayer
			,RemotePlayer
			,Bullet
		}

		abstract	public	EType	Type { get; }	//Get Type must override this

		public	override	void	OnStartServer() {
			DB.Message(GetType().Name +":"+ System.Reflection.MethodBase.GetCurrentMethod().Name);	//Print where we are		
		}

		public	override	void	OnStartClient() {
			DB.Message(GetType().Name +":"+ System.Reflection.MethodBase.GetCurrentMethod().Name);	//Print where we are		
		}

		public	override	void	OnStartLocalPlayer() {
			DB.Message(GetType().Name +":" +System.Reflection.MethodBase.GetCurrentMethod().Name);	//Print where we are		
		}
	
		#region Updates & house keeping
		void Update () {
			if (isLocalPlayer) {		//Process Local Player code
				ProcessLocalPlayer ();
			}
			if (isServer) {			//Process Server specific code
				ProcessServer ();
			}
		}


		//Passed up to process local player
		public	virtual	void	ProcessLocalPlayer () {
		}

		//Passed up to process Server code
		public	virtual	void	ProcessServer () {
		}

		#endregion


		#region Interaction		//Handle interactions such as collisions & triggers

		void	OnTriggerEnter(Collider vOther) {		//Simple wrapper to manage Unity two versions of collisions
			if (isServer) {
				ProcessEntityCollision (vOther.gameObject, true);
			}
		}

		void 	OnCollisionEnter(Collision vOther) {
			if (isServer) {
				ProcessEntityCollision (vOther.gameObject, false);
			}
		}

		void	ProcessEntityCollision(GameObject vGO,bool vIsTrigger) {
			Entity	tEntity = vGO.GetComponent<Entity> ();
			if (tEntity != null) {
				CollidedWith (tEntity, vIsTrigger);
			} else {
				DB.Message(string.Format("{0} {1} {2}"
					,System.Reflection.MethodBase.GetCurrentMethod().Name
					,name
					, "Not an Entity"));	//Warning		
			}
		}

		protected virtual	void	CollidedWith(Entity vEntity, bool vIsTrigger) {
			DB.Message(string.Format("{0} {1} {3} with {2} Type {4}",
				System.Reflection.MethodBase.GetCurrentMethod().Name
				,name
				,vEntity.name
				,((vIsTrigger)?"trigger":"Collided")
				,Type));	//Default debug
		}


		#endregion

	}
}
