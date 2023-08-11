using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{

    public bool cardInSlot = false;
    public bool isZoomed = false;
    Vector3 scaleChange = new Vector3(0.4f,0.8f,0.12f);

    CharacterData _thisCardData;

    [SerializeField]
    TMP_Text _name;
    [SerializeField]
    TMP_Text _cost;
    [SerializeField]
    TMP_Text _power;
    [SerializeField]
    TMP_Text _health;
    [SerializeField]
    TMP_Text _description;
    [SerializeField]
    Image _image;

    private GameObject _hologramObject = null;

    private static int _leftSatelliteCards, _wonderCards, _rightSatelliteCards = 0;



    public void ChangeKin(Rigidbody rig){
        if(!cardInSlot){
            rig.isKinematic = false;
        }

        if(cardInSlot){
            rig.isKinematic = true;
        }
    }

    public void PopulateCard(CharacterData cardData)
    {
        _thisCardData = cardData;
        _name.text = _thisCardData.name.ToString();
        _cost.text = _thisCardData.mana.ToString();
        _power.text = _thisCardData.attack.ToString();
        _health.text = _thisCardData.health.ToString();
        _description.text = _thisCardData.description;
        _image.sprite = _thisCardData.artwork;
        _hologramObject = _thisCardData.hologram;
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
        Vector3 firstcardSlot = (playZone.position - cardOffset * 2.5f + new Vector3(0, 0.1f, 0));
        transform.position = firstcardSlot + cardOffset * currentCardCount;
    }
}
