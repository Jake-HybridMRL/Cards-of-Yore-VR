using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class SnapToMe : MonoBehaviour
{
    public float snapDistance = 0.2f; // Distance threshold for snapping
    bool cardInSlot = false;
    GameObject Card;

    bool cardCooldown = false;
    private void Update(){}
    PointableUnityEventWrapper wrapper1;
    //Checks for collision
    private void OnTriggerEnter(Collider collider1){
        if (collider1.gameObject.name == "Card" && collider1.GetComponent<CardBehaviour>() != null && collider1.GetComponent<CardBehaviour>().CurrentCardSlot == null && !collider1.GetComponent<CardBehaviour>().cardInSlot && !cardCooldown)
        {
            cardInSlot = true;
            collider1.GetComponent<CardBehaviour>().CurrentCardSlot = this;
            collider1.GetComponent<CardBehaviour>().test = true;
            Card = collider1.gameObject;
            PointableUnityEventWrapper wrapper = collider1.GetComponent <PointableUnityEventWrapper>();
            wrapper.WhenRelease.AddListener(SnapToSlot);
            wrapper.WhenRelease.RemoveListener(UnSnapToSlot);
            wrapper1 = wrapper;
            cardCooldown = false;
            StartCoroutine(snapCooldown());
        }
    }

    IEnumerator snapCooldown ()
    {
        yield return new WaitForSeconds(1);
        cardCooldown = true;
    }

//Release the collision
    private void OnTriggerExit(Collider collider1){
        if(cardCooldown)
        {
            if (collider1.gameObject.name == "Card" && cardInSlot && collider1.GetComponent<CardBehaviour>().CurrentCardSlot == this)
            {
                cardInSlot = false;
                PointableUnityEventWrapper wrapper = collider1.GetComponent<PointableUnityEventWrapper>();

            }
            if (collider1.gameObject.name == "Card" && collider1.GetComponent<CardBehaviour>().CurrentCardSlot == this && collider1.GetComponent<CardBehaviour>().cardInSlot)
            {
                PointableUnityEventWrapper wrapper = collider1.GetComponent<PointableUnityEventWrapper>();
                collider1.GetComponent<CardBehaviour>().cardCanSnap = false;
                Card.GetComponent<CardBehaviour>().CurrentCardSlot = null;
            }

            if ((collider1.gameObject.name == "Card" && collider1.GetComponent<CardBehaviour>().cardInSlot && collider1.GetComponent<CardBehaviour>().CurrentCardSlot == null && collider1.GetComponent<CardBehaviour>().cardCanSnap) && !cardCooldown)
            {
                PointableUnityEventWrapper wrapper = collider1.GetComponent<PointableUnityEventWrapper>();
                wrapper.WhenRelease.AddListener(UnSnapToSlot);
                wrapper.WhenRelease.RemoveListener(SnapToSlot);
                collider1.GetComponent<CardBehaviour>().test = false;
            }
        }
    }

    private void SnapToSlot(PointerEvent pointer){
        if(cardInSlot == true && Card.GetComponent<CardBehaviour>().CurrentCardSlot == this)
        {
            Card.GetComponent<CardBehaviour>().ChangeKin(true);
            Card.GetComponent<CardBehaviour>().cardInSlot = true;
            Card.transform.position = transform.position;
            Card.transform.LookAt(transform.position-Camera.main.transform.position);
            Card.transform.SetParent(transform);
            wrapper1.WhenSelect.AddListener(UnSnapToSlot);
        }
        
    }

    private void UnSnapToSlot(PointerEvent pointer){
    //if(cardInSlot == false)
        {
            Card.transform.SetParent(null);
            Card.GetComponent<CardBehaviour>().cardInSlot = false;
            Card.GetComponent<CardBehaviour>().ChangeKin(false);
            Card.GetComponent<CardBehaviour>().CurrentCardSlot = null;
            wrapper1.WhenSelect.RemoveListener(UnSnapToSlot);
            wrapper1.WhenRelease.RemoveListener(SnapToSlot);
        }
    //if(Card.GetComponent<CardBehaviour>().CurrentCardSlot == this)
        Card.GetComponent<CardBehaviour>().SizeDownCardOutside(Card.transform);

    }

}