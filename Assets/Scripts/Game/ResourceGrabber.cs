using UnityEngine;

public class ResourceGrabber : MonoBehaviour
{
    public int CollectFrequency = 1;
    private ResourceSpot _currentSpot;
    private FrequencyExecutor _executor;

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var resourceSpot = other.GetComponent<ResourceSpot>();
        if (resourceSpot)
        {
            Debug.Log(other.name);
            _currentSpot = resourceSpot;
            _executor = new FrequencyExecutor(CollectFrequency, this);
            _executor.OnFire += () => Collect();
            _executor.Start();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var resourceSpot = other.GetComponent<ResourceSpot>();
        if (_currentSpot == resourceSpot)
        {
            _currentSpot = null;
        }
    }

    private void Collect()
    {
        Debug.Log("Collecting Resources");
    }

}