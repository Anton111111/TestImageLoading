using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    private List<RawImage> _images = new();

    [SerializeField]
    private TextMeshProUGUI _currentStateText;

    void Start()
    {
        StartCoroutine(Downloader());
    }

    private IEnumerator Downloader()
    {
        var status = "";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(
            ""
        );
        yield return www.SendWebRequest();
        var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        for (int i = 0; i < _images.Count; i++)
        {
            var image = _images[i];
            var nTexture = new Texture2D(texture.width, texture.height, texture.format, true);
            nTexture.SetPixelData(texture.GetRawTextureData<byte>(), 0);
            switch (i)
            {
                case 0:
                nTexture.mipMpa
                    nTexture.filterMode = FilterMode.Trilinear;
                    nTexture.anisoLevel = 1;
                    break;
                case 1:
                    nTexture.filterMode = FilterMode.Trilinear;
                    nTexture.anisoLevel = 2;
                    break;
                case 2:
                    nTexture.filterMode = FilterMode.Trilinear;
                    nTexture.anisoLevel = 4;
                    break;
                case 3:
                    nTexture.filterMode = FilterMode.Trilinear;
                    nTexture.anisoLevel = 8;
                    break;

                case 4:
                    nTexture.filterMode = FilterMode.Trilinear;
                    nTexture.anisoLevel = 8;
                    break;
                case 5:
                    nTexture.filterMode = FilterMode.Trilinear;
                    nTexture.anisoLevel = 4;
                    break;
                case 6:
                    nTexture.filterMode = FilterMode.Trilinear;
                    nTexture.anisoLevel = 2;
                    break;
                case 7:
                    nTexture.filterMode = FilterMode.Trilinear;
                    nTexture.anisoLevel = 1;
                    break;
            }
            nTexture.Apply();
            image.texture = nTexture;
        }
        _currentStateText.text = status;
    }

    private void Update()
    {
        var status = "";
        foreach (var image in _images)
        {
            if (image.texture)
            {
                status +=
                    image.texture.mipmapCount
                    + "; "
                    + image.texture.filterMode
                    + "; "
                    + image.texture.anisoLevel
                    + "; "
                    + (image.texture as Texture2D).mipMapBias
                    + "; "
                    + QualitySettings.antiAliasing
                    + "\n";
            }
        }

        _currentStateText.text = status;
    }
}
