using UnityEngine;
using System.Collections;

public class Auto_Destroyer : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		Destroy(this.gameObject,0.2f);
	}
}
