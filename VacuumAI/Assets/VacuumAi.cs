using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumAi : MonoBehaviour {

    public GameObject cleanTile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckTile();
	}

    void CheckTile()
    {
        RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            Debug.Log(hit.distance);
            Debug.DrawLine(transform.position, hit.transform.position, Color.red);
            if(hit.collider.tag == "Dirty")
            {
                Instantiate(cleanTile, new Vector3(transform.position.x, transform.position.y, transform.position.z + .85f), Quaternion.identity);
                Destroy(hit.collider.gameObject);
                transform.Translate(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
            }
        }
    }
}
