using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class JustStipplePP : MonoBehaviour
{
    public Material sobelMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (sobelMaterial != null)
        {
            int width = src.width;
            int height = src.height;
            //get luminance
            RenderTexture luminanceSource = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(src, luminanceSource, sobelMaterial, 0);

            RenderTexture stippleSource = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(luminanceSource, stippleSource, sobelMaterial, 2);

            Graphics.Blit(stippleSource, dest);

            RenderTexture.ReleaseTemporary(stippleSource);
            RenderTexture.ReleaseTemporary(luminanceSource);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
