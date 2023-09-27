public enum ResourceTypes
{
    Wood,
    Stone
}


[System.Serializable]
public class ResourceInfo
{
    public int Amount;
    public ResourceTypes Type;

}