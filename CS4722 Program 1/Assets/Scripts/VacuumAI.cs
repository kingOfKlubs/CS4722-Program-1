using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumAI : MonoBehaviour {

    public int[,] grid = new int[3, 3];
    public int currentPos = 0;

    private bool trash = false;
    private bool clean = true;

	// Use this for initialization
	void Start () {
        currentPos = grid[0, 2];
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public int[,] CheckNieghbors(int[,] position)
    {

        return position;
    }

    public void CheckUp()
    {
        if(trash)
        {

        }
    }

}
