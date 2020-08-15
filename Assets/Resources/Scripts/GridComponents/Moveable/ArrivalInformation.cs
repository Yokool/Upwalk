using UnityEngine;

public class ArrivalInformation
{

    public GameObject TravellingObject { get; private set; }
    public int dX { get; private set; }
    public int dY { get; private set; }

    public ArrivalInformation(GameObject TravellingObject, int TargetX, int TargetY)
    {

        this.TravellingObject = TravellingObject;
        this.dX = TargetX;
        this.dY = TargetY;

    }

}