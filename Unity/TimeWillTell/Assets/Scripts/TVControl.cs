using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVControl : MonoBehaviour
{
    [SerializeField] private GameObject         _tv;
    [SerializeField] private List<VideoClip>    _videoClips;
    [SerializeField] private bool               _loopQueue, _startOff;
    
    private VideoPlayer _videoPlayer;
    private bool        _isOn, _deletedNoSignal, _gamePaused;
    private int         _queuePos;

    void Start()
    {
        _videoPlayer = _tv.GetComponent<VideoPlayer>();
        _queuePos = 1;

        if (_startOff)
            Off();
        
        else
            On();
    }

    public bool IsOn()
    {
        if (_isOn) return true;
        
        else return false;
    }

    public void On()
    {
        _videoPlayer.clip = _videoClips[_queuePos];
        _videoPlayer.Play();
        _isOn = true;
    }

    public void Off()
    {
        _videoPlayer.clip = _videoClips[0];
        _isOn = false;
    }

    public void PlayTape(string tape)
    {
        if (tape == "VHS_0" && !_deletedNoSignal)
            _videoClips.RemoveAt(1);
            _deletedNoSignal = true;

        _queuePos = 1;
        _videoPlayer.clip = _videoClips[_queuePos];
        _videoPlayer.Stop();
        _videoPlayer.Play();
    }

    public void PlayNext()
    {
        _queuePos += 1;
        
        if (_queuePos >= _videoClips.Count)
        {
            _queuePos = 0;
            PlayNext();
        }

        _videoPlayer.clip = _videoClips[_queuePos];
        _videoPlayer.Play();
    }

    void Update()
    {
        if (_loopQueue && _isOn && !_videoPlayer.isPlaying &&
            _videoPlayer.frame > 0 && !_gamePaused)
            
            PlayNext();
        
        if (Time.timeScale == 0f)
        {
            _videoPlayer.Pause();
            _gamePaused = true;
        }
        
        if (Time.timeScale == 1f && _gamePaused)
        {
            _videoPlayer.Play();
            _gamePaused = false;
        }    
    }
}
