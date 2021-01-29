using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    [SerializeField] private GameObject _subs;
    [SerializeField] private Text       _line;

    private bool _subsOn, _reading, _inRange;

    void Start()
    {
        _subs.SetActive(false);
        _subsOn = true;
    }

    void Update()
    {
        if (Time.timeScale == 1 && _reading && _subsOn && _inRange)
            _subs.SetActive(true);
        
        else
            _subs.SetActive(false);

    }

    public void Set(bool toggle)
    {
        _subsOn = toggle;
    }

    public void InRange(bool inside)
    {
        if (inside) _inRange = true;

        else _inRange = false;
    }

    public void ReadVHS_0()
    {
        if (_subsOn && _inRange)
        {
            Stop();
            
            _subs.SetActive(true);
            
            StartCoroutine(VHS_0());
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
        _line.text = "";
    }

    IEnumerator VHS_0()
    {
        _reading = true;

        yield return new WaitForSeconds(2.5f);
        
        _line.text = 
            "Dad: Hello Bruce. We're recording this video because we have to run.";
        
        yield return new WaitForSeconds(4.2f);
        
        _line.text = 
            "You have questions, we understand. But we can't answer them," 
            + " not now, not all in one single tape. It wouldn't be safe.";
        
        yield return new WaitForSeconds(7.8f);
        
        _line.text =
            "To keep it short, you have a gift, you remember things no one else "
            + "does as if they happened yesterday, we're counting on that.";
        
        yield return new WaitForSeconds(7.1f);
        
        _line.text =
            "Only you can find us.";

        yield return new WaitForSeconds(2);
        
        _line.text =
            "Are you done with that, honey?";

        yield return new WaitForSeconds(2);
        
        _line.text =
            "Mom: Yes. We have to go, there's no time to waste.";

        yield return new WaitForSeconds(4);
        
        _line.text =
            "Dad: Yeah, right behind you. Clock's ticking son. We love you, goodbye.";

        yield return new WaitForSeconds(6);

        _line.text = "";
        _subs.SetActive(false);

        _reading = false;
    }
}
