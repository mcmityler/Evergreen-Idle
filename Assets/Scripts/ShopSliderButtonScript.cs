/*
Created by:  Tyler McMillan
Description: This script deals with the shop slider button that opens and closes the shop screen
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopSliderButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    GameObject _manager; //gamemanager reference
    [SerializeField] Animator _shopAnimator; //animator that opens and closes shop && moves button when hovering
    [SerializeField] TMP_Text _shopSlideText; //text on shop open button
    bool _isOpen = false;

    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GameManager"); //get reference
    }
    public void OnPointerEnter(PointerEventData pointerEventData)//when mouse is ontop of this shop open button
    {
        //slide open shop button depending on if the shop screen is open or closed
        _shopAnimator.SetBool("SlideOut", true);
        _shopAnimator.SetBool("SlideIn", true);

    }
    public void OnPointerDown(PointerEventData eventData)//when mouse clicks on the shop open button.. open/close shop
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");

        if (!_isOpen) //shop is closed so open it
        {
            _shopAnimator.SetTrigger("OpenShop");
            _shopAnimator.ResetTrigger("CloseShop");

            _isOpen = true;
            _shopSlideText.text = ">> SHOP";
            _manager.GetComponent<ShopScript>().SetShopOpen(_isOpen);

        }
        else if (_isOpen)//shop is open so close it
        {
            _shopAnimator.SetTrigger("CloseShop");
            _shopAnimator.ResetTrigger("OpenShop");

            _isOpen = false;
            _shopSlideText.text = "<< SHOP";
            _manager.GetComponent<ShopScript>().SetShopOpen(_isOpen);
        }
    }
    public void OnPointerExit(PointerEventData eventData)// cursor is no longer ontop of button so make it undo its sliding effect
    {
        _shopAnimator.SetBool("SlideOut", false);

        _shopAnimator.SetBool("SlideIn", false);

    }
}
