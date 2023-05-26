using System.Collections;
using Blobcreate.ProjectileToolkit;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Minion lockedMinion;
    private int Dmg;
    public void Fire(Minion minion, int Dmg)
    {
        
        lockedMinion = minion;
        this.Dmg = Dmg;

        Vector3 v = Projectile.VelocityByTime(this.transform.position, lockedMinion.transform.position, 0.1f);
        this.GetComponent<Rigidbody>().AddForce(v, ForceMode.VelocityChange);

        StartCoroutine("Destroy");
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.15f);
        lockedMinion.Hit(Dmg);

        Destroy(this.gameObject);
    }
}