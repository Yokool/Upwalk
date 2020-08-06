using UnityEngine;

public class ArrivalInformation
{

    public GameObject TravellingObject { get; private set; }
    public int TargetX { get; private set; }
    public int TargetY { get; private set; }

    public ArrivalInformation(GameObject TravellingObject, int TargetX, int TargetY)
    {

        this.TravellingObject = TravellingObject;
        this.TargetX = TargetX;
        this.TargetY = TargetY;

    }

}