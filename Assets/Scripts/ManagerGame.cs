using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    public static int altura = 20;
    public static int largura = 10;

    public static Transform[,] grade = new Transform[largura, altura];

    public bool dentroGrade(Vector2 posicao)
    {
        return ((int)posicao.x >= 0 && (int)posicao.x < largura && (int)posicao.y >= 0);
    }

    public Vector2 arredonda(Vector2 nA)
    {
        return new Vector2(Mathf.Round(nA.x), Mathf.Round(nA.y));
    }

    public void atualizaGrade(TetroMove partTetro)
    {
        for(int y=0; y < altura; y++)
        {
            for(int x=0; x < largura; x++)
            {
                if (grade[x, y] != null)
                {
                    if(grade[x, y].parent == partTetro.transform)
                    {
                        grade[x, y] = null;
                    }
                }
            }
        }

        foreach(Transform parts in partTetro.transform)
        {
            Vector2 pos = arredonda(parts.position);

            if (pos.y < altura) 
            {
                grade[(int)pos.x, (int)pos.y] = parts;
            }
        }
    }
    public Transform posTransformGrid (Vector2 pos)
    {
        if (pos.y > (altura - 1))
        {
            return null;
        }
        else{
            return grade[(int) pos.x, (int) pos.y];
        }
    }

    public bool fullLine(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            if (grade[x, y] == null){
                return false;
            }
        }
        return true;
    }

    public void deleteParts (int y)
    {
        for (int x = 0; x < largura; x++)
        {
            Destroy (grade[x, y].gameObject);

            grade[x, y] = null;
        }
    }

    public void moveLineDown(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            if(grade[x, y] != null)
            {
                grade[x, y -1] = grade[x, y];
                grade[x, y] = null;

                //visual movement
                grade[x, y -1].position += new Vector3 (0, -1, 0);
            }
        }
    }

    public void moveAllDown(int y)
    {
        for (int i = y; i < altura; i++)
        {
            moveLineDown(i);
        }
    }

    public void deleteAllLine()
    {
        for (int y = 0; y < altura; y++)
        {
            if (fullLine(y))
            {
                deleteParts(y);
                moveAllDown(y+1);
                y--;
            }
        }
    }
}
