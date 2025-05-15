using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image bloodOverlay;
    public float fadeSpeed = 2f;
    public Color bloodColor = new Color(1, 0, 0, 0.5f); 

    private bool isTakingDamage = false;

    private void Update()
    {
        if (!isTakingDamage && bloodOverlay.color.a > 0)
        {
            //Blod overlay fading
            bloodOverlay.color = Color.Lerp(bloodOverlay.color, new Color(1, 0, 0, 0), fadeSpeed * Time.deltaTime);
        }
        isTakingDamage = false;
    }

    public void ShowDamageEffect()
    {
        isTakingDamage = true;
        bloodOverlay.color = bloodColor;
    }
}

