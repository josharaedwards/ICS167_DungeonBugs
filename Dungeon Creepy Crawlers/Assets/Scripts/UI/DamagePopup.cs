//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float disappearSpeed = 3f;
    [SerializeField] private float textSpeed = 5f;
    [SerializeField] private float disappearTimer = 1f;

    [SerializeField] private Color damageColor;
    [SerializeField] private Color healColor;

    private TextMeshPro damageText;
    private Color textColor;

    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(UIManager.GetInstance().damagePopupInst, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        if (damageAmount < 0)
        {
            damageText.color = damageColor;
        }
        else if (damageAmount > 0)
        {
            damageText.color = healColor;
        }
        int displayAmount = Mathf.Abs(damageAmount);
        textColor = damageText.color;

        damageText.SetText(displayAmount.ToString());
    }

    private void Update()
    {
        transform.position += new Vector3(0, textSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if(disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            damageText.color = textColor;

            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
