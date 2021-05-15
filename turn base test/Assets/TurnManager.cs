using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum TurnState {SelectUnit, Move, Attack, DirChange, NextTurn }

    public TurnState Turns;

    public PlayerMove pm;

    public int PlayerNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StateFunction();
    }

    void StateFunction()
    {
        if(Turns == TurnState.SelectUnit)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if(PlayerNum == 1)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Player")
                        {
                            pm = hit.transform.GetComponent<PlayerMove>();
                            pm.UnitIsSelected = true;
                        }
                    }
                }

                if (PlayerNum == 2)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Player2")
                        {
                            pm = hit.transform.GetComponent<PlayerMove>();
                            pm.UnitIsSelected = true;
                        }
                    }
                }
            }

        }
        else if (Turns == TurnState.Move)
        {
            ClickToMove();
        }
        else if (Turns == TurnState.Attack)
        {
            ClickToAttack();
        }
        else if (Turns == TurnState.DirChange)
        {
            ClickToChangeDir();
        }
        else if (Turns == TurnState.NextTurn)
        {
            NextTurn();
            Turns = TurnState.SelectUnit;
            pm.Endturn();
            pm = null;
        }
    }

    public void NextTurn()
    {
        if(PlayerNum == 1)
        {
            PlayerNum = 2;
        }
        else if (PlayerNum == 2)
        {
            PlayerNum = 1;
        }
    }

    public void ClickToMove()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    if (t.selectable)
                    {
                        pm.MoveToTile(t);
                        Turns = TurnState.Move;
                    }
                }
            }
        }
    }

    public void ClickToAttack()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Attackable")
                {

                    if(hit.collider.name == "FrontDir")
                    {
                        pm.ChangeDirection(0);
                    }

                    else if (hit.collider.name == "RightDir")
                    {
                        pm.ChangeDirection(1);
                    }

                    else if (hit.collider.name == "BackDir")
                    {
                        pm.ChangeDirection(2);
                    }

                    else if (hit.collider.name == "LeftDir")
                    {
                        pm.ChangeDirection(3);
                    }
                    pm.anim.SetTrigger("Hit");
                    pm.AttackRange.SetActive(false);

                    //Turns = TurnState.NextTurn;
                }
            }
        }
    }

    public void ClickToChangeDir()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Attackable")
                {

                    if (hit.collider.name == "FrontDir")
                    {
                        pm.ChangeDirection(0);
                    }

                    else if (hit.collider.name == "RightDir")
                    {
                        pm.ChangeDirection(1);
                    }

                    else if (hit.collider.name == "BackDir")
                    {
                        pm.ChangeDirection(2);
                    }

                    else if (hit.collider.name == "LeftDir")
                    {
                        pm.ChangeDirection(3);
                    }
                   Turns = TurnState.NextTurn;
                }
            }
        }
    }

}
