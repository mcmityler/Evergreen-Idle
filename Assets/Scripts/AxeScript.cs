/*
Created by:  Tyler McMillan
Description: This script deals with the axe and swinging / animations and completing swing
*/
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    [SerializeField] GameObject _axeParticleSpawnPoint; //where to spawn axe slash / tree particles
    bool _canSwing = false; //am i over something i can swing at
    bool _swingingNow = false; //is swing anim playing
    [SerializeField] Animator _axeAnimator;  //animator to swing
    GameObject _player;
    [SerializeField] float _swingSpeed = 0.5f;
    [SerializeField] int _axeDmg = 10;
    GameObject _currentTree = null;
    bool _autoSwingOn = false;
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
        if ((Input.GetMouseButton(0) || _autoSwingOn == true) && _canSwing && _swingingNow == false && GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShopScript>().IsShopOpen() == false)
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
    public void SetCurrentTree(GameObject m_currentTree)
    {
        _currentTree = m_currentTree;
        if (m_currentTree != null) //ensure you arent setting the current tree to null (happens when you leave the current tree or destroy it)
        {

        }
    }
    public void ChangeSwing(bool m_canSwing) //change canSwing Variable, also if false stop the current swing if one is on.
    {
        _canSwing = m_canSwing;
        if (_canSwing == false)
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
        if (_currentTree != null)
        {


            //only allow if its over a tree
            _currentTree.GetComponent<TreeScript>().DamageTree(_axeDmg);


            //Play hit SFX
            FindObjectOfType<AudioManager>().Play("AxeChop");
        }
    }
    public void UpDMG(int m_increaseAmount)
    {
        _axeDmg += m_increaseAmount;
    }
    public void ChangeAutoSwing(bool m_isAutoSwingOn)
    {
        _autoSwingOn = m_isAutoSwingOn;
    }
}
