using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetroMove : MonoBehaviour
{
    public bool podeRodar;
    public bool roda360;

    public float velocidade;
    float timer;
    public float down;

    ManagerGame mgGame;
    SpawnTetro spawner;

    void Start() {
        mgGame = GameObject.FindObjectOfType<ManagerGame>();
        spawner = GameObject.FindObjectOfType<SpawnTetro>();
        velocidade = 0.1f;
        timer = velocidade;
    }
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            timer = velocidade;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            directionMove(1, 0);
            validTransform(-1, 0, true);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            directionMove(-1, 0);
            validTransform(1, 0, true);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            directionMove(0, -1);
            timer += Time.deltaTime;
            validTransform(0, 1, false);
            
        }

        if(Time.time - down >= 1 && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0);
            validTransform(0, 1, false);
            down = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            checaRodar();
        }
    }

    void directionMove (int directionX, int directionY)
    {
        timer += Time.deltaTime;

        if (timer > velocidade)
        {
            transform.position += new Vector3(directionX, directionY, 0);
            timer = 0;
        }
    }

    void checaRodar()
    {
        if (podeRodar)
        {
            if (!roda360)
            {
                if (transform.rotation.z < 0)
                {
                    transform.Rotate(0, 0, 90);
                    validRotate(-90);
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                    validRotate(90);
                }
            }
            else
            {
                transform.Rotate(0, 0, -90);
                validRotate(90);
            }
        }
    }

    bool posicaoValida()
    {
        foreach(Transform child in transform){
            Vector2 posBloco = mgGame.arredonda(child.transform.position);

            if (mgGame.dentroGrade(posBloco) == false)
            {
                return false;
            }

            if(mgGame.posTransformGrid(posBloco) != null && mgGame.posTransformGrid(posBloco).parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    void validRotate (int value)
    {
        if (posicaoValida())
        {
            mgGame.atualizaGrade(this);
        }
        else 
        {
            transform.Rotate(0, 0, value);
        }
    }

    void validTransform (int valueX, int valueY, bool ative)
    {
        if(posicaoValida())
        {
            mgGame.atualizaGrade(this);
        }
        else
        {
            transform.position += new Vector3(valueX , valueY, 0);
            mgGame.deleteAllLine();
            enabled = ative;
            if(!ative)
            {
                spawner.proxPart();
            }
        }
    }
}
