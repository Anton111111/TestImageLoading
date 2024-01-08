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

    private int _nextUrl = 0;

    private List<string> _urls =
        new()
        {
            "https://raw.githubusercontent.com/Anton111111/TestImageLoading/main/Assets/Images/banner1.png",
            "https://raw.githubusercontent.com/Anton111111/TestImageLoading/main/Assets/Images/banner2.png",
            "https://raw.githubusercontent.com/Anton111111/TestImageLoading/main/Assets/Images/banner3.png"
        };

    void Start()
    {
        StartCoroutine(ChangeImage());
    }

    private IEnumerator ChangeImage()
    {
        var url = _urls[_nextUrl];
        Debug.Log(">>>>" + url);
        _nextUrl = (_nextUrl + 1 >= _urls.Count) ? 0 : _nextUrl + 1;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
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
    }

    private bool _pressed = false;

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

        OVRInput.Update();
        if (OVRInput.Get(OVRInput.Button.One) && !_pressed)
        {
            _pressed = true;
            StartCoroutine(ChangeImage());
        }

        if (!OVRInput.Get(OVRInput.Button.One) && _pressed)
        {
            _pressed = false;
        }
    }
}
