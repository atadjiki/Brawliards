using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{

    public static EffectsManager instance = null;
    public GameObject explosionFX;
    public GameObject smokeFX;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Init();
    }

    public void Init()
    {


        if (smokeFX == null)
        smokeFX = Resources.Load<GameObject>("VFX/FlamesParticleEffect");
        if(explosionFX == null)
        explosionFX = Resources.Load<GameObject>("VFX/BigExplosionEffect");
    }

    public IEnumerator DoSmoke(GameObject target, float time)
    {
        GameObject vfx = Instantiate<GameObject>(smokeFX);
        vfx.transform.position = target.transform.position;
        vfx.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(time);
        vfx.GetComponent<ParticleSystem>().Stop();
        Destroy(vfx);
    }

    public IEnumerator DoExplosion(GameObject target, float time)
    {
        GameObject vfx = Instantiate<GameObject>(explosionFX);
        vfx.transform.position = target.transform.position;
        vfx.transform.localScale = new Vector3(3, 3, 3);

        vfx.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(time);
        vfx.GetComponent<ParticleSystem>().Stop();
        Destroy(vfx);

    }
}
