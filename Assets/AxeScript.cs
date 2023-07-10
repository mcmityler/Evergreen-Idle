using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    [SerializeField] GameObject _axeParticleSpawnPoint; //where to spawn axe slash / tree particles
    bool _canSwing = false; //am i over something i can swing at
    bool _swingingNow = false; //is swing anim playing
    [SerializeField] Animator _axeAnimator;  //animator to swing
    GameObject _player;
    [SerializeField] float _swingSpeed = 0.5f;
    private void Start()
    {
        _player = transform.parent.gameObject;
        _axeAnimator.SetFloat("SwingSpeed", _swingSpeed);
    }
    private void Update()
    {
        //FOLLOW THE MOUSE
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _player.transform.position = mousePosition - (Vector2.up / 2);


        //SWING IF YOU CLICK
        if (Input.GetMouseButton(0) && _canSwing && _swingingNow == false)
        {
            _axeAnimator.SetTrigger("Swing");
            _swingingNow = true;
        }
        //STOP SWING IF LET GO
        if (Input.GetMouseButtonUp(0) && _canSwing && _swingingNow == true)
        {
            _axeAnimator.SetTrigger("StopSwing");
            _swingingNow = false;

        }
    }
    public void ChangeSwing(bool m_canSwing) //change canSwing Variable, also if false stop the current swing if one is on.
    {
        _canSwing = m_canSwing;
        if(_canSwing == false)
        {
            _axeAnimator.SetTrigger("StopSwing");
            _swingingNow = false;

        }
    }
    public void ChangeSwingSpeed(float m_swingSpeed) //change how fast the axe animation plays (aka how fast you swing)
    {
        _swingSpeed = m_swingSpeed;
        _axeAnimator.SetFloat("SwingSpeed", _swingSpeed);
    }
    public void AxeSwingComplete()
    {
        _swingingNow = false; //reset hit anim so you can swing again
        //only allow if its over a tree

        //spawn particles at particle spawn point

        //Play hit SFX
    }
}
