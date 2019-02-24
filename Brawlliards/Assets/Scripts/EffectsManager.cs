using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{

    public static EffectsManager instance = null;
    public GameObject explosionFX;
    public GameObject smokeFX;

    // Start is called before the first frame update
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        InitFX();

    }

    void InitFX()
    {
        if(smokeFX == null)
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
