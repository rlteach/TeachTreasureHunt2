using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	
	public bool destroyOnDeath;
	public const	int	maxHealth=100;
	public RectTransform HealthBar;
	public RectTransform HealthBarCanvas;


	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;


	public void TakeDamage(int amount)
	{
		if (isServer) {
			currentHealth -= amount;
			if (currentHealth <= 0) {
				currentHealth = 0;
				if (destroyOnDeath) {
					Destroy (gameObject);
				} else {
					RpcRespawn ();
				}
			}
		}
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			currentHealth = 100;
			gameObject.GetComponent<MovePlayer> ().ResetPlayer ();
		}
	}


	void OnChangeHealth (int currentHealth)	{
		HealthBar.sizeDelta = new Vector2(currentHealth, HealthBar.sizeDelta.y);
	}

	void Update()
	{
		HealthBarCanvas.transform.LookAt(HealthBarCanvas.transform.position + Camera.main.transform.rotation * Vector3.forward,Camera.main.transform.rotation * Vector3.up);
	}
}
