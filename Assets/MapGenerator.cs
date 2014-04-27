using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	public Transform matrix;

	// Use this for initialization
	void Start () {
	
		for (int i = 0; i < 4; i++) {
						for (int j = 0; j < 4; j++) {
							
							Instantiate(matrix,new Vector3(i,j,0),Quaternion.identity);
						}
				}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
