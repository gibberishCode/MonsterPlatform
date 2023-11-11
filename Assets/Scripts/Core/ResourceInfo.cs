public enum ResourceTypes
{
    Wood,
    Stone
}


[System.Serializable]
public class ResourceInfo
{
    public float Amount;
    public ResourceTypes Type;
    public ResourceInfo(float amount, ResourceTypes type) {
        Amount = amount;
        Type = type;
    }

}