using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class JustSobelPP : MonoBehaviour
{
    public Material sobelMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (sobelMaterial != null)
        {
            int width = src.width;
            int height = src.height;


            RenderTexture sobelSource = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(src, sobelSource, sobelMaterial, 1);


            Graphics.Blit(sobelSource, dest);

            RenderTexture.ReleaseTemporary(sobelSource);
            //get luminance
            // RenderTexture luminanceSource = RenderTexture.GetTemporary(width, height, 0, src.format);
            // Graphics.Blit(src, luminanceSource, sobelMaterial, 0);

            // RenderTexture sobelSource = RenderTexture.GetTemporary(width, height, 0, src.format);
            // Graphics.Blit(luminanceSource, sobelSource, sobelMaterial, 1);


            // Graphics.Blit(sobelSource, dest);

            // RenderTexture.ReleaseTemporary(sobelSource);
            // RenderTexture.ReleaseTemporary(luminanceSource);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
