using UnityEngine;
using System.Collections;


//Base class for Pickups, does not need to inherit Monobehaviour
//Used as template for Pickup behaviour
abstract	public class Pickup  {

	public	bool	Delete=false;		//If set, is scheduled for deletion

	public	abstract	void	UpdatePlayer (PlayerController tPlayer,float tTime);	//Used by player to ask Pickup to modify player

	public	abstract	string	Name { get; }		//Get the name of this pickup

}
