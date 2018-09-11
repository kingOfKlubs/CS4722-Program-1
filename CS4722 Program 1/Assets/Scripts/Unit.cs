using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;

    public List<Node> currentPath = null;

    private void Update()
    {
        if(currentPath != null)
        {
            int currNode = 0;

            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) +
                new Vector3(0, 0, -1f);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y) + 
                    new Vector3(0, 0, -1f);

                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }
        MoveNextTile();
        transform.position = Vector3.Lerp(transform.position, map.TileCoordToWorldCoord(tileX, tileY), .00000000001f * Time.deltaTime);
        
    }

    public void MoveNextTile()
    {
        if(currentPath == null)
        {
            return;
        }

        currentPath.RemoveAt(0);

        tileX = currentPath[0].x;
        tileY = currentPath[0].y;
        transform.position = map.TileCoordToWorldCoord(tileX,tileY);
        

        if(currentPath.Count == 1)
        {
            currentPath = null;
        }
    }
}
