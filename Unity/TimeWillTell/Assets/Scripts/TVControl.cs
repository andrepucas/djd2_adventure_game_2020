using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVControl : MonoBehaviour
{
    public string isPlaying;
    
    [SerializeField] private VideoClip[] _videoClips;
    
    private VideoPlayer _videoPlayer;
    
    void Start()
    {
        _videoPlayer        = GetComponent<VideoPlayer>();
        _videoPlayer.clip   = _videoClips[3];
        isPlaying           = "OFF";
    }

    public void Play(string tape)
    {
        isPlaying = tape;
        
        switch(isPlaying)
        {
            case "VHS_0":
                _videoPlayer.clip = _videoClips[0];
                break;
            
            case "VHS_1":
                _videoPlayer.clip = _videoClips[1];
                break;
            
            case "NONE":
                _videoPlayer.clip = _videoClips[2];
                break;

            case "OFF":
                _videoPlayer.clip = _videoClips[3];
                break;
        }
        _videoPlayer.Play();
    }
}
