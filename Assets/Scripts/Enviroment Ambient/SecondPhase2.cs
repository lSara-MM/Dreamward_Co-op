using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SecondPhase2 : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D plattform1;
    public Light2D plattform2;
    public Light2D bossEye1;
    public Light2D bossEye2;
    public Light2D bossEye3;
    public Light2D moon;

    public SpriteRenderer spriteMoon;

    public bool secondPhase = false;
    public bool faddingHasEnded = false;
    public float timing = 0f;

    [SerializeField] private BossHealth bossHealth;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.LeftShift))
        {
            bossHealth.bossSP = true;
        }

        if (!bossHealth.bossSP)
        {
            globalLight.intensity = 1.32f;
            globalLight.color = new Color(0.580f, 0.654f, 0.764f, 1f);
            spriteMoon.color = new Color(1f, 0.878f, 0.843f, 1f);

            faddingHasEnded = false;

            bossEye1.intensity = 0.57f;
            bossEye2.intensity = 0.343f;
            bossEye3.intensity = 2.51f;

            plattform1.intensity = 0f;
            plattform2.intensity = 0f;

            moon.intensity = 3.58f;

            timing = 0f;
        }
        else
        {
            timing += Time.deltaTime;

            if (!faddingHasEnded)
            {
                if (globalLight.intensity > 0f)
                {
                    globalLight.intensity -= 0.005f * timing;
                }

                if (spriteMoon.color.r > 0.1f && spriteMoon.color.g > 0.160f && spriteMoon.color.b > 0f && spriteMoon.color.a > 0.1f)
                {
                    spriteMoon.color = new Color(spriteMoon.color.r, spriteMoon.color.g, spriteMoon.color.b - 0.005f * timing, spriteMoon.color.a);
                }

                if (spriteMoon.color.r > 0.1f && spriteMoon.color.g > 0.160f && spriteMoon.color.b > 0f && spriteMoon.color.a > 0.1f)
                {
                    spriteMoon.color = new Color(spriteMoon.color.r, spriteMoon.color.g - 0.005f * timing, spriteMoon.color.b, spriteMoon.color.a);
                }

                if (spriteMoon.color.r > 0.717f && spriteMoon.color.g > 0.160f && spriteMoon.color.b > 0f && spriteMoon.color.a > 0.1f)
                {
                    spriteMoon.color = new Color(spriteMoon.color.r - 0.005f * timing, spriteMoon.color.g, spriteMoon.color.b, spriteMoon.color.a);
                }

                if (bossEye3.color.a > 0f)
                {
                    bossEye3.color = new Color(bossEye3.color.r, bossEye3.color.g, bossEye3.color.b, bossEye3.color.a - 0.005f * timing);
                }
            }

            if (timing > 2.3f)
            {
                faddingHasEnded = true;
                globalLight.intensity = 1f;
                globalLight.color = new Color(1, 0.070f, 0, 1);
                spriteMoon.color = new Color(0.717f, 0.160f, 0, 1f);
                timing = 0f;

                ChangeLigth();
            }
        }
    }

    void ChangeLigth()
    {
        bossEye1.intensity = 1.86f;
        bossEye2.intensity = 1.86f;
        bossEye3.intensity = 0.63f;

        plattform1.intensity = 1.15f;
        plattform2.intensity = 1.15f;

        moon.intensity = 2.48f;
    }
}
