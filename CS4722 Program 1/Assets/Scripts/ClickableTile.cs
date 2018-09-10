﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;

    private void OnMouseUp()
    {
        Debug.Log("click!");

        map.GeneratePathTo (tileX, tileY);
    }
}
