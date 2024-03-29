//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;
    public int lerpSpeed;

    private float currentAmount;
    private Slider self;

    public void OnEnable()
    {
        self = GetComponent<Slider>();

        StatsTracker.updateHealthUI += Init;
        StatsTracker.updateDamageUI += DamageReceived;
    }

    public void OnDisable()
    {
        StatsTracker.updateHealthUI -= Init;
        StatsTracker.updateDamageUI -= DamageReceived;
    }

    void Update()
    {
        UpdateBar();
    }

    public void Init(int maxHealth, Transform root)
    {
        if (!hasSameRoot(root))
        {
            return;
        }
            

        self.maxValue = maxHealth;
        currentAmount = maxHealth;
        self.value = currentAmount;
        

        fill.color = gradient.Evaluate(1f);
    }

    public void DamageReceived(int dmg, Transform root)
    {
        if (!hasSameRoot(root))
            return;

        currentAmount = self.value + dmg;
    }

    private void UpdateBar()
    {
        if(currentAmount != self.value)
        {
            self.value = Mathf.Lerp(self.value, currentAmount, Time.deltaTime * lerpSpeed);
            fill.color = gradient.Evaluate(self.normalizedValue);
        }
    }

    private bool hasSameRoot(Transform root)
    {
        return root == gameObject.transform.root;
    }
}
