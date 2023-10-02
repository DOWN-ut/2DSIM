using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGame_Case : MonoBehaviour
{
    public int state;

    public Vector2Int position;

    public int surrounding;

    //public LifeGame_Manager manager;

    public LifeGame_Damier damier;

    public SpriteRenderer visual;
    public TextMesh count;

    public void Start()
    {
        count.text = "";
        Colorize();
    }

    public void FixedUpdate()
    {
        int pre = damier.manager.step % 3;

        if (pre == 0 || !damier.manager.pre)
        {
            surrounding = 0;

            int i = 0; int x = 0; int y = 0;
            while (i < 8)
            {
                x = GetX(i); y = GetY(i);
                if (Test(x, y, position))
                {
                    if (damier.cases2D[position.x + x][position.y + y].GetComponent<LifeGame_Case>().state == 1)
                    {
                        surrounding++;
                    }
                }
                i++;
            }
        }

        if (damier.manager.simulate && damier.manager.step > 1)
        {
            if (pre == 2)
            {
                if (state == 2)
                {
                    state = 0;
                }
                else if (state == 3)
                {
                    state = 1;
                }
            }
            if(pre == 1)
            {
                if (state == 1)
                {
                    if (surrounding < damier.manager.under || surrounding > damier.manager.over)
                    {
                        if (!damier.manager.pre)
                        {
                            state = 0;
                        }
                        else
                        {
                            state = 2;
                        }
                    }
                }
                else
                {
                    if (surrounding == damier.manager.born)
                    {
                        if (!damier.manager.pre)
                        {
                            state = 1;
                        }
                        else
                        {
                            state = 3;
                        }
                    }
                }
            }

            Colorize();
        }

        if (count != null)
        {
            if (surrounding > 0)
            {
                count.text = surrounding.ToString();
            }
            else
            {
                count.text = "";
            }
            //count.color = damier.manager.colors[state + 4];
        }
    }

    bool Test(int x, int y, Vector2Int p)
    {
        bool b = true;

        if (x < 0 && p.x <= 0)
        {
            b = false;
        }
        else if (x > 0 && p.x >= damier.size - 1)
        {
            b = false;
        }
        else if (y < 0 && p.y <= 0)
        {
            b = false;
        }
        else if (y > 0 && p.y >= damier.size - 1)
        {
            b = false;
        }

        return b;
    }

    int GetX(int i)
    {
        int x = 0;

        if (i == 0 || i == 3 || i == 5)
        {
            x = -1;
        }
        if (i == 1 || i == 6)
        {
            x = 0;
        }
        if (i == 2 || i == 4 ||i == 7)
        {
            x = 1;
        }

        return x;
    }

    int GetY(int i)
    {
        int y = 0;

        if (i == 0 || i == 1 || i == 2)
        {
            y = -1;
        }
        if (i == 3 || i == 4)
        {
            y = 0;
        }
        if (i == 5 || i == 6 ||i == 7)
        {
            y = 1;
        }

        return y;
    }

    public void Colorize(bool visibilize = true)
    {
        visual.color = damier.manager.colors[state];
        if (!visual.gameObject.activeSelf && visibilize)
        {
            damier.Visibilize(damier.visibleSize * 1.1f);
        }
    }
}