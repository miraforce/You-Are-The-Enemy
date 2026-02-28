using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class portrait_animator : MonoBehaviour
{
    public Sprite defaultFrame;
    public Sprite attackFrame;

    public void PortraitAttackAnimation()
    {
        StartCoroutine(PlayAttackFrame());
    }

    IEnumerator PlayAttackFrame ()
    {
        GetComponent<Image>().sprite = attackFrame;
        yield return new WaitForSeconds(2f);
        ResetFrame();
    }

    private void ResetFrame()
    {
        GetComponent<Image>().sprite = defaultFrame;
    }
}
