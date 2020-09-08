using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public float Speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.back * Speed;
    }

 
}
