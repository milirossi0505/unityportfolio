using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CuadroManager : MonoBehaviour
{
    public Renderer cuadroRenderer;
    public VideoPlayer cuadroVideo;
    [SerializeField] private List<CuadroData> _datas;
    private int _currentIndex;

    private void Start()
    {
        ChangeMaterial(0);
    }

    public void ChangeMaterial(int value)
    {
        _currentIndex += value;
        _currentIndex %= _datas.Count;
        if (_currentIndex < 0)
        {
            _currentIndex = _datas.Count-1;
        }

        cuadroVideo.enabled = _datas[_currentIndex].texture == null;
        if (_datas[_currentIndex].texture != null)
        {
            cuadroRenderer.material.mainTexture = _datas[_currentIndex].texture;
        }
        else
        {
            cuadroVideo.clip = _datas[_currentIndex].clip;
        }
        if (_datas[_currentIndex].isSquare)
        {
            cuadroRenderer.transform.parent.localScale = Vector3.one;
        }
        else
        {
            cuadroRenderer.transform.parent.localScale = new Vector3(1.6f, 1, 1);
        }
    }

    [System.Serializable]
    public class CuadroData
    {
        public string name;
        public Texture2D texture;
        public VideoClip clip;
        public bool isSquare;
    }
}
