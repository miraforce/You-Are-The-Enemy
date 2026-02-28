using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject spellSpritePrefab;
    private Renderer spriteRenderer;


    public Transform spawnHere;

    public void Start()
    {
        GameObject magicGO = Instantiate(spellSpritePrefab, spawnHere);
        spriteRenderer = magicGO.GetComponent<Renderer>();
        spriteRenderer.enabled = false;
    }

    public void AttackAnimation()
    {
        StartCoroutine(SpellSpriteAnimation());
        
    }

    public IEnumerator SpellSpriteAnimation ()
    {
        //makes the sprite visible for 2 seconds, then invisible again
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(2f);
        spriteRenderer.enabled = false;
    }
}
