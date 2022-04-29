using System.Linq;
using UnityEngine;

public class Utils
{
    public static Collider[] EnemysInRadius(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        return hitColliders.Where(coll => coll.gameObject.tag == "Enemy").ToArray();
    }
}