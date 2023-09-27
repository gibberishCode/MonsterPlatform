using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float DamageFrequency = 1;
    [SerializeField] float Damage = 1;
    private FrequencyExecutor _executor;
    private Damageable _damageable;



    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<Damageable>();
        if (damageable)
        {
            _executor = new FrequencyExecutor(DamageFrequency, this);

        }
    }


    private void OnTriggerExit(Collider other)
    {
        var damageable = other.GetComponent<Damageable>();
        if (damageable && _damageable == damageable)
        {
            _damageable = null;
            _executor.Stop();
        }

    }

}