using UnityEngine;

public class CandleBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        int i = 0;

        foreach (ContactPoint contact in collision.contacts)
        {
            print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            // Visualize the contact point
            //Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
}
