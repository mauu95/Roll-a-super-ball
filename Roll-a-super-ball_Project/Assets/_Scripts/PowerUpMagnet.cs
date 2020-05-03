using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : PowerUp {
    public float activeTime = 5;
    public float speed = 10;
    public float range = 10;
    public bool isAttracting;
    public string soundName = "PUMagnet";

    private float nextTimeToDeactivate = 0;
    private GameObject forceFieldPrefab;
    private ForceField forceField;

    private void Awake() {
        id = "magnet";
        cooldownTime = 5;
        forceFieldPrefab = PrefabManager.instance.forceField;

        forceField = Instantiate(forceFieldPrefab).GetComponent<ForceField>();
        forceField.targetDim = Vector3.zero;
        forceField.transform.localScale = Vector3.zero;
        forceField.follow = transform;
        forceField.magnet = this;

        AudioManager.instance.Play(soundName);
    }

    private void LateUpdate()
    {
        AudioManager.instance.GetSound(soundName).source.volume = ( forceField.transform.localScale.x/range ) * 0.2f;
    }

    public override void doStuff() {
        isAttracting = true;
        forceField.fadeIn(Vector3.one * range);
        StartCoroutine(ReturnToNormalAfterTime(activeTime));
    }

    public void Attract(GameObject hit)
    {
        if (hit.transform.CompareTag("PickUp"))
        {
            Vector3 distance = transform.position - hit.transform.position;
            hit.transform.position += distance.normalized * speed * Time.deltaTime;
        }
    }

    IEnumerator ReturnToNormalAfterTime(float t)
    {
        yield return new WaitForSeconds(t);
        ReturnToNormal();
    }

    public override void ReturnToNormal()
    {
        isAttracting = false;
        forceField.fadeOut();
        AudioManager.instance.GetSound(soundName).source.volume = 0f;
    }


}
