using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopSliderButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    GameObject _manager;
    [SerializeField] Animator _shopAnimator;
    [SerializeField] TMP_Text _shopSlideText;
    bool _isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseEnter()
    {
        if (!_isOpen)
        {
            _shopAnimator.SetTrigger("SlideOut");

        }
        else if (_isOpen)
        {
            _shopAnimator.SetTrigger("SlideIn");
        }
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _shopAnimator.SetBool("SlideOut", true);


        _shopAnimator.SetBool("SlideIn", true);

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");

        if (!_isOpen)
        {
            _shopAnimator.SetTrigger("OpenShop");
            _isOpen = true;
            _shopSlideText.text = ">> SHOP";
            _manager.GetComponent<ShopScript>().SetShopOpen(_isOpen);

        }
        else if (_isOpen)
        {
            _shopAnimator.SetTrigger("CloseShop");
            _isOpen = false;
            _shopSlideText.text = "<< SHOP";
            _manager.GetComponent<ShopScript>().SetShopOpen(_isOpen);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _shopAnimator.SetBool("SlideOut", false);

        _shopAnimator.SetBool("SlideIn", false);

    }
}
