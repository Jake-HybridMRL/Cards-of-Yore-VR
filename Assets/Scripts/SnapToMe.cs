using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class SnapToMe : MonoBehaviour
{
    public float snapDistance = 0.2f; // Distance threshold for snapping
    bool cardInSlot = false;
    GameObject Card;

    private void Update(){}

//Checks for collision
    private void OnTriggerEnter(Collider collider1){
        if (collider1.gameObject.name == "Card"){
            cardInSlot = true;
            Card = collider1.gameObject;
            Card.GetComponent<MeshRenderer>().material.color = Color.green;
            PointableUnityEventWrapper wrapper = collider1.GetComponent <PointableUnityEventWrapper>();
            wrapper.WhenRelease.AddListener(SnapToSlot);
            wrapper.WhenRelease.RemoveListener(UnSnapToSlot);

        }
    }

//Release the collision
    private void OnTriggerExit(Collider collider1){
        if (collider1.gameObject.name == "Card" && cardInSlot){
            cardInSlot = false;
            Card.GetComponent<MeshRenderer>().material.color = Color.red;
            PointableUnityEventWrapper wrapper = collider1.GetComponent <PointableUnityEventWrapper>();
            wrapper.WhenRelease.RemoveListener(SnapToSlot);
        }
        if(collider1.gameObject.name == "Card"){
            PointableUnityEventWrapper wrapper = collider1.GetComponent <PointableUnityEventWrapper>();
            wrapper.WhenRelease.AddListener(UnSnapToSlot);
        }
    }

    private void SnapToSlot(PointerEvent pointer){
        if(cardInSlot == true){
            Card.transform.position = transform.position;
            Card.transform.SetParent(transform);
        }
    }

        private void UnSnapToSlot(PointerEvent pointer){
        if(cardInSlot == false){
            Card.transform.SetParent(null);
        }
    }


//Assets/Scripts/SnapToMe.cs(23,45): error CS1503: Argument 1: cannot convert from 'method group' to 'UnityAction<PointerEvent>'
}