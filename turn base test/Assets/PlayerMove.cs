using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : TacticsMove
{
    public bool UnitIsSelected;
    public GameObject SelectedIndicator;
    public GameObject AttackRange;
    public GameObject Directions;
    public TurnManager TM;
    public Animator anim;
    public Transform f, r, b, l;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

        if (UnitIsSelected)
        {
            if (TM.Turns == TurnManager.TurnState.SelectUnit)
            {
                FindSelectableTiles();
                SelectedIndicator.SetActive(true);
                TM.Turns = TurnManager.TurnState.Move;
            }

            if (!moving)
            {
                //FindSelectableTiles();
                //TM.CheckMouse();
            }
            else
            {
                Move();
                anim.SetBool("isWalking", true);
                
            }
            if (Donemoving)
            {
                TM.Turns = TurnManager.TurnState.Attack;
                AttackRange.SetActive(true);
                AttackRange.transform.position = this.transform.position;
                Donemoving = false;
                anim.SetBool("isWalking", false);
            }
        }
    }

    public void Endturn()
    {
        SelectedIndicator.SetActive(false);
        UnitIsSelected = false;
        Directions.SetActive(false);
        AttackRange.transform.position = Vector3.zero;
        Directions.transform.position = Vector3.zero;
    }

    public void ChangeDirection(int Dir)
    {
        if(Dir == 0)
        {
            transform.forward = f.transform.position - transform.position;
            transform.forward.Normalize();
        }
        else if (Dir == 1)
        {
            transform.forward = r.transform.position - transform.position;
            transform.forward.Normalize();
        }
        else if (Dir == 2)
        {
            transform.forward = b.transform.position - transform.position;
            transform.forward.Normalize();
        }
        else if (Dir == 3)
        {
            transform.forward = l.transform.position - transform.position;
            transform.forward.Normalize();
        }
    }

    public void EndAttack()
    {
        TM.Turns = TurnManager.TurnState.DirChange;
        Directions.SetActive(true);
        Directions.transform.position = this.transform.position;
    }
}
