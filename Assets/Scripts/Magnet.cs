using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{   //singleton class
    public static Magnet instance;

    [SerializeField] float magnetForce = 10f;
    List<Rigidbody> affectedRigibody = new List<Rigidbody>();
    Transform magnet;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        magnet = this.transform;
        affectedRigibody.Clear();
    }
    private void FixedUpdate()
    {
        if (!Game.isGameover && Game.isMoving)
        {
            foreach (Rigidbody rb in affectedRigibody)
            {
                rb.AddForce((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameover && (other.CompareTag("Obstacle") || other.CompareTag("Object")))
        {
            AddToMagnetField(other.GetComponent<Rigidbody>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!Game.isGameover && (other.CompareTag("Obstacle") || other.CompareTag("Object")))
        {
            RemoveFromMagnetField(other.GetComponent<Rigidbody>());
        }
    }

    public void AddToMagnetField(Rigidbody rb)
    {
        affectedRigibody.Add(rb);
    }
    public void RemoveFromMagnetField(Rigidbody rb)
    {
        affectedRigibody.Remove(rb);
    }
}
