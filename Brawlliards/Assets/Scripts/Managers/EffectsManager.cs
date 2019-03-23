using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{

    public static EffectsManager instance = null;
    public GameObject explosionFX;
    public float explosionScale = 3;
    public GameObject smokeFX;
    public float smokeScale = 1;
    public GameObject collideFX;
    public float collideScale = 1;


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
        if (explosionFX == null)
            explosionFX = Resources.Load<GameObject>("VFX/BigExplosionEffect");
        if (collideFX == null)
            collideFX = Resources.Load<GameObject>("VFX/BloodSprayEffect");
    }

    public void DoSmoke(GameObject target, float time)
    {
        StartCoroutine(DoFX(smokeFX, target, time, new Vector3(smokeScale, smokeScale, smokeScale)));
    }

    public void DoExplosion(GameObject target, float time)
    {
        StartCoroutine(DoFX(explosionFX, target, time, new Vector3(explosionScale, explosionScale, explosionScale)));

    }

    internal void DoCollide(GameObject target, float time)
    {
        StartCoroutine(DoFX(collideFX, target, time, new Vector3(collideScale, collideScale, collideScale)));
    }

    private IEnumerator DoFX(GameObject prefab, GameObject target, float time, Vector3 scale)
    {
        GameObject vfx = Instantiate<GameObject>(prefab);
        vfx.transform.position = target.transform.position;
        vfx.transform.localScale = scale;
        vfx.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(time);
        vfx.GetComponent<ParticleSystem>().Stop();
        DestroyImmediate(vfx);
    }
}
