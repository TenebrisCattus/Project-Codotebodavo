using UnityEngine;
using UnityEngine.UI;

public class CutSceneCanvasScript : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteArray; 
    [SerializeField] private float switchDelay = 1.0f; 
    [SerializeField] private Image targetImage;
    [SerializeField] private GameObject canvas; // Это текущий канвас

   
    
    public void StartCutscene()
    {
        canvas.SetActive(true);
        if (spriteArray.Length > 0 && targetImage != null)
        {
            StartCoroutine(RotateSpritesCoroutine());
        }
    }

    private System.Collections.IEnumerator RotateSpritesCoroutine()
    {
        for (int i = 0; i < spriteArray.Length; i++)
        {
            targetImage.sprite = spriteArray[i];

            yield return new WaitForSeconds(switchDelay);
        }
        canvas.SetActive(false);
    }
}
