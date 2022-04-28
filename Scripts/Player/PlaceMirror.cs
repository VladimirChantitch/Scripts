using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMirror : MonoBehaviour
{
    [Header("Mirror Stats")]
    [SerializeField] private AudioClip destructionSound;
    [SerializeField] private int maximumMirrors;
    [SerializeField] private int currentMirrors_Count;
    [SerializeField] private float mirror_cooldown;
    [SerializeField] private float startTime;
    [SerializeField] private float spawnDistance;
    [SerializeField] private GameObject mirrorPrefab;
    [SerializeField] private List<GameObject> placed_mirrors = new List<GameObject>();
    [Header("references")]
    [SerializeField] private Transform placeOrigin;
    [SerializeField] LayerMask whatIsDestroyableMirror;
    [SerializeField] AudioSource audioSource;

    private void StartCoolDown(){
        startTime = Time.time;
    }

    private bool CanSpawnMirror(){
        if (UI_Manager.Instance.GetIsInAMenu()){
            return false;
        }
        if (Time.time < startTime + mirror_cooldown){
            return false;
        }
        StartCoolDown();
        return true;
    }
    public void SpawnAMirror(){
        if (!CanSpawnMirror()){
            return;
        }

        if (currentMirrors_Count >= maximumMirrors){
            Destroy(placed_mirrors[0]);
            placed_mirrors.RemoveAt(0);
        }

        Vector3 spawnPosition = placeOrigin.position + placeOrigin.forward * spawnDistance;

        GameObject mirror = Instantiate(mirrorPrefab, spawnPosition, Quaternion.identity);

        Mirror mirrorScript = mirror.GetComponent<Mirror>();
        Renderer renderer =  mirrorScript.GetMirrorMesh().GetComponent<Renderer>();
        Material TemplateMirrorMaterial = renderer.material;

        RenderTexture newRenderTexture = new RenderTexture(1000,2000,1);
        Material newMaterial = new Material(TemplateMirrorMaterial);

        renderer.material = newMaterial;
        newMaterial.mainTexture = newRenderTexture;
        mirrorScript.GetCamera().targetTexture = newRenderTexture;

        mirror.transform.LookAt(transform);

        placed_mirrors.Add(mirror);

        currentMirrors_Count += 1;
    }

    public void DestroyMirror(){
        RaycastHit raycastHit;

        if (Physics.Raycast(placeOrigin.position, placeOrigin.forward, out raycastHit, spawnDistance + 2f, whatIsDestroyableMirror)){
            Destroy(raycastHit.transform.gameObject);
            audioSource.PlayOneShot(destructionSound);
        }
    }

    public void DestroyAllMirrors(){
        for (int i = 0; i < placed_mirrors.Count; i++){
            Destroy(placed_mirrors[i]);
        }
    }

    public GameObject NewMirror(GameObject newMirrorPrefab){
        Mirror mirrorScript = newMirrorPrefab.GetComponent<Mirror>();
        Renderer renderer =  mirrorScript.GetMirrorMesh().GetComponent<Renderer>();
        Material TemplateMirrorMaterial = renderer.material;

        RenderTexture newRenderTexture = new RenderTexture(1000,2000,1);
        Material newMaterial = new Material(TemplateMirrorMaterial);

        renderer.material = newMaterial;
        newMaterial.mainTexture = newRenderTexture;
        mirrorScript.GetCamera().targetTexture = newRenderTexture;

        return newMirrorPrefab;
    }
}
