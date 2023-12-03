using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SobelPostProcessing : MonoBehaviour
{
    public Material fullMaterial;
    public float darkRegionThreshold = 0.2f;
    public Color darkColor;
    public Color lightColor;
    public bool useColor = false;


    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (fullMaterial != null)
        {
            int width = src.width;
            int height = src.height;
            fullMaterial.SetFloat("_DarkRegionThreshold", darkRegionThreshold);
            fullMaterial.SetColor("_DarkColor", darkColor);
            fullMaterial.SetColor("_LightColor", lightColor);
            //get luminance
            RenderTexture luminanceSource = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(src, luminanceSource, fullMaterial, 0);

            RenderTexture sobelSource = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(luminanceSource, sobelSource, fullMaterial, 1);

            RenderTexture stippleSource = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(luminanceSource, stippleSource, fullMaterial, 2);

            RenderTexture.ReleaseTemporary(luminanceSource);
            fullMaterial.SetTexture("_StippleTex", stippleSource);

            RenderTexture combined = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(sobelSource, combined, fullMaterial, 3);


            if (useColor)
            {
                Graphics.Blit(combined, dest, fullMaterial, 4);

            }
            else
            {
                Graphics.Blit(combined, dest);
            }

            RenderTexture.ReleaseTemporary(stippleSource);

            RenderTexture.ReleaseTemporary(sobelSource);
            RenderTexture.ReleaseTemporary(combined);

        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}