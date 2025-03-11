using UnityEngine;

[RequireComponent(typeof(MeshExplosiveEffect))]
public class FragmentEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private GameObject[] fragmentPrefab;
    private Renderer[] fragmentRenderers;
    private MeshExplosiveEffect meshExplosiveEffect => GetComponent<MeshExplosiveEffect>();
    private float _timer = 0f;
    private float _fragmentDestoryTime;
    [SerializeField]private float _dissolveTimeBeforeDestroy = 2f;
    void Start()
    {
        //caches the fragment prefabs
        fragmentPrefab = new GameObject[transform.childCount];
        fragmentRenderers = new Renderer[transform.childCount];
        for(int i = 0; i < fragmentPrefab.Length; i++){
            fragmentPrefab[i] = transform.GetChild(i).gameObject;
            fragmentRenderers[i] = fragmentPrefab[i].GetComponent<MeshRenderer>();
        }
        _fragmentDestoryTime = meshExplosiveEffect.FragmentsLifeTime;
    }
    void Update()
    {
        if(_timer > _fragmentDestoryTime - _dissolveTimeBeforeDestroy){
            for(int i = 0; i < fragmentRenderers.Length; i++){
                foreach(var material in fragmentRenderers[i].materials){
                    material.SetFloat("_DissolveAmount",   (_fragmentDestoryTime - _timer) / _dissolveTimeBeforeDestroy);
                }
            }
        }
        _timer += Time.deltaTime;
    }
}
