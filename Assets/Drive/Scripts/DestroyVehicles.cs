using UnityEngine;

public class DestroyVehicles : MonoBehaviour
{

    private void OnCollisionEnter(Collision obj)
    {
        Destroy(obj.gameObject);
    }
}
