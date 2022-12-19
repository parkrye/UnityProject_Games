using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Save : MonoBehaviour
{
    public Camera saveCamera;
    public GameObject Cursor;

    public void SaveImage()
    {
        StartCoroutine(Work());
    }

    IEnumerator Work()
    {
        Cursor.SetActive(false);
        RenderTexture rt = new RenderTexture(800, 540, 24);
        saveCamera.targetTexture = rt;

        saveCamera.Render();
        RenderTexture.active = rt;

        Texture2D screenShot = new Texture2D(800, 540, TextureFormat.RGB24, false);

        yield return new WaitForEndOfFrame();
        screenShot.ReadPixels(new Rect(0, 0, 800, 540), 0, 0);
        screenShot.Apply();

        saveCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        Destroy(screenShot);

        string filename = Application.dataPath + "/" + DateTime.Now.ToString(("yyyyMMddHHmmss"));
        System.IO.File.WriteAllBytes(filename + ".png", bytes);
        Cursor.SetActive(true);
    }
}
