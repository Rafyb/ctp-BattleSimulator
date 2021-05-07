using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private int _restant;
    private int _idx = -1;
    private int[] _unitRest = new int[3];

    public Image[] unitBtn;
    public Text[] unitNb;
    public Text restantNb;

    public GameObject groupPrep;
    public GameObject groupWin;
    public GameObject groupLose;

    public int GetRestant()
    {
        return _restant;
    }

    public void SetRestant(int nb)
    {
        _restant = nb;
        restantNb.text = "Encore : " + nb;
    }

    public int GetUnitRestant(int i)
    {
        return _unitRest[i];
    }

    public void SetUnitRestant(int i,int nb)
    {
        _unitRest[i] = nb;
        unitNb[i].text = ""+nb;
    }

    public int GetSelected()
    {
        return _idx;
    }

    public void Validate()
    {
        Game.Instance.OnStart();
        groupPrep.SetActive(false);
    }

    public void OnPrep()
    {
        groupPrep.SetActive(true);
        groupWin.SetActive(false);
        groupLose.SetActive(false);
    }

    public void OnWin()
    {
        groupPrep.SetActive(false);
        groupWin.SetActive(true);
        groupLose.SetActive(false);
    }

    public void OnLose()
    {
        groupPrep.SetActive(false);
        groupWin.SetActive(false);
        groupLose.SetActive(true);
    }

    public void Select(int id)
    {
        foreach(Image b in unitBtn)
        {
            b.color = Color.white;
        }
        unitBtn[id].color = Color.cyan;
        _idx = id;
    }

}
