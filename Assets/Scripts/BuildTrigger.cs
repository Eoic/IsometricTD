using UnityEngine;

public class BuildTrigger : MonoBehaviour
{
    // Unity's inspector cannot handle enum as parameter 
    // for button function. Using integer instead.
    public void SetBuildingID(int buildingId)
    {
        StructureBuilder.Instance.EnableBuildMode((Constants.Buildings)buildingId);
    }
}
