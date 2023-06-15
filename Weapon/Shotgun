using UnityEngine;

public class Shotgun : Weapon
{
    public override void Shoot(Transform shootPoint)
    {
        Instantiate(Bullet, shootPoint.position, Quaternion.Euler(new Vector3(0, 0, 15)));
        Instantiate(Bullet, shootPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Instantiate(Bullet, shootPoint.position, Quaternion.Euler(new Vector3(0, 0, -15)));
    }
}
