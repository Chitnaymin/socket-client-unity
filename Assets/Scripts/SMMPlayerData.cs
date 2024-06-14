using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMMPlayerData 
{
    public string id { get; set; }
    public int positionX { get; set; }
    public int positionY { get; set; }

    public SMMPlayerData(string id, int positionX, int positionY)
    {
        this.id = id;
        this.positionX = positionX;
        this.positionY = positionY;
    }
    
    public void Print()
    {
        Debug.Log("SMM I am "+ id +" standing " + " X : "+  positionX + " | Y : "+ positionY);
    }
}
