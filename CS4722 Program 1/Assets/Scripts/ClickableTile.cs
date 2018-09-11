using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;
    public GameObject cleanTile;

    private void OnMouseUp()
    {
        Debug.Log("click!");

        map.GeneratePathTo (tileX, tileY);
        Clean();
    }
    void Clean()
    {
        if (this.gameObject.tag != "Clean")
        {
            Instantiate(cleanTile, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }
}
