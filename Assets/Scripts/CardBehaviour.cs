using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{

    public bool cardInSlot = false;
    public bool isZoomed = false;
    Vector3 scaleChange = new Vector3(0.4f,0.8f,0.12f);

    [SerializeField]
    CharacterData _thisCardData;
    private static int _leftSatelliteCards, _wonderCards, _rightSatelliteCards = 0;
    [SerializeField]
    private GameObject _hologramObject = null;


    public void ChangeKin(Rigidbody rig){
        if(!cardInSlot){
            rig.isKinematic = false;
        }

        if(cardInSlot){
            rig.isKinematic = true;
        }
    }


    public void SizeUpCard(Transform tran){
        if(!isZoomed){
            tran.localScale += scaleChange;
            isZoomed = true;
        }
    }

    public void SizeDownCard(Transform tran){
        if(isZoomed){
            tran.localScale -= scaleChange;
            isZoomed = false;
        }
    }

        public void SizeDownCardOutside(Transform tran){
        if(isZoomed){
            tran.localScale -= scaleChange*0.2f;
            isZoomed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Location")
        {
            if (other.gameObject.name == "Left satellite")
            {
                PositionCard(other.transform, _leftSatelliteCards);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                _leftSatelliteCards++;
            }
            else if (other.gameObject.name == "Right satellite")
            {
                PositionCard(other.transform, _rightSatelliteCards);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;

                _rightSatelliteCards++;
            }
            else if (other.gameObject.name == "Wonder")
            {
                PositionCard(other.transform, _wonderCards);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;

                _wonderCards++;
            }
        }
    }

    void PositionCard(Transform playZone, int currentCardCount)
    {
        Vector3 cardOffset = new Vector3(.15f, 0, 0);
        Vector3 firstcardSlot = (playZone.position - cardOffset * 2 + new Vector3(0, 0.1f, 0));
        transform.position = firstcardSlot + cardOffset * currentCardCount;
    }
}
