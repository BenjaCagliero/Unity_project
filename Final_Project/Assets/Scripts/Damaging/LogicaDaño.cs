using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaDaño : MonoBehaviour
{
    public int hp;
    public int dañoArma;
    public int dañoPuño;
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weaponImpact")
        {
            if (anim != null)
            {
                anim.Play("Enemigo");
            }

            hp -= dañoArma;
        }

        if (other.gameObject.tag == "impact")
        {
            if (anim != null)
            {
                anim.Play("Enemigo");
            }

            hp -= dañoPuño;
        }

        if (hp <= 0)
        {
            //TODO: Que hacer cuando le quite el hp
            anim.Play("Death");
        }
    }

}
