using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVControl : MonoBehaviour
{
    [SerializeField] private GameObject         _tv;
    [SerializeField] private Subtitles          _subs; 
    [SerializeField] private List<VideoClip>    _videoClips;
    [SerializeField] private bool               _loopQueue, _startOff;
    
    private VideoPlayer _videoPlayer;
    private AudioSource _tvAudio;
    private bool        _isOn, _deletedNoSignal, _gamePaused;
    private int         _queuePos;

    void Start()
    {
        _videoPlayer = _tv.GetComponent<VideoPlayer>();
        _tvAudio     = _tv.GetComponent<AudioSource>();
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
        
        if (_deletedNoSignal)
        {
            if (_queuePos == 1) _subs.ReadVHS(0);

            else if (_queuePos == 2) _subs.ReadVHS(1);
        }
            
    }

    public void Off()
    {
        _videoPlayer.clip = _videoClips[0];
        _isOn = false;
        
        if (!_loopQueue) _subs.Stop();
    }

    public void PlayTape(string tape)
    {
        if (tape == "VHS_0")
        {
            _subs.ReadVHS(0);
            
            if (!_deletedNoSignal)
            {
                _videoClips.RemoveAt(1);
                _deletedNoSignal = true;
            }
        } 

        _queuePos = 1;
        _videoPlayer.clip = _videoClips[_queuePos];
        _videoPlayer.Stop();
        _videoPlayer.Play();
    }

    public void PlayNext(string type)
    {
        _queuePos += 1;
        
        if (_queuePos >= _videoClips.Count)
        {
            _queuePos = 0;
            PlayNext(type);
        }

        _videoPlayer.clip = _videoClips[_queuePos];
        _videoPlayer.Play();

        if (type == "VHS")
        {
            if (_queuePos == 1) _subs.ReadVHS(0);

            else if (_queuePos == 2) _subs.ReadVHS(1);
        }
    }

    void Update()
    {
        if (_isOn && !_videoPlayer.isPlaying &&
            _videoPlayer.frame > 0 && !_gamePaused)
        {
            if (_loopQueue) PlayNext("ANY");

            else PlayNext("VHS");
        }

        if (Time.timeScale == 0f)
        {
            // Fixes small sound glitches coming from the tv when paused.
            _tvAudio.enabled = false; 

            _videoPlayer.Pause();
            _gamePaused = true;
        }
        
        if (Time.timeScale == 1f && _gamePaused)
        {
            _videoPlayer.Play();
            _tvAudio.enabled = true;
            _gamePaused = false;
        }
    }
}
