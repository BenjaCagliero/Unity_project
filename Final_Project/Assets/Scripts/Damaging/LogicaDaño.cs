using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaDaño : MonoBehaviour
{
    public int hp;
    public int dañoArma;
    public int dañoPuño;
    public Animator anim;
    private void OnCollisionEnted(Collider other)
    {
        if (other.tag == "weaponImpact")
        {
            if (anim != null)
            {
                anim.Play("isHit");
            }

            hp -= dañoArma;
        }

        if (hp <= 0)
        {
            anim.Play("takeDamage");
        }
    }

}
