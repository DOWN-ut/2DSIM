using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AtomManager : MonoBehaviour
{
    public float radius = 1;

    public bool attract = true;
    public bool repulse = true;

    public float attractForce = 1;
    public float attractPow = 2;

    public float areaSlow = 1.01f;

    public float repulseForce = 1;
    public float repulsePow = 2;

    public float sizePow = 1 / 2f;

    public float dragPow = 1/3f;
    public float maxVelocity = 20f;

    public GameObject pause;
    public float time;

    public InputField addAtomsInput;
    public int spawnQuant;
    public InputField electronsInput;
    public int spawnElec;
    public InputField tailleInput;
    public float tail;

    public InputField forceInput;
    public InputField repulInput;

    public RandomSpawnerAtom spawner;

    public Atom[] atoms;

    public float consta = 1.259f;

    private void Start()
    {
        spawner.size = PlayerPrefs.GetFloat("Atom.Taille");
        tailleInput.text = spawner.size.ToString("F2");
        forceInput.text = attractForce.ToString();
        repulInput.text = repulseForce.ToString();
        time = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Pause();
        }

        if (pause.activeSelf)
        {
            if(int.TryParse(addAtomsInput.text,out int r))
            {
                spawnQuant = r;
            }
            if (int.TryParse(electronsInput.text, out int s))
            {
                if (s > 0 && s < 10)
                {
                    spawnElec = s;
                }               
            }
            if (float.TryParse(tailleInput.text, out float t))
            {
                spawner.size = t;
                PlayerPrefs.SetFloat("Atom.Taille", spawner.size);
            }
            if (float.TryParse(forceInput.text, out float f))
            {
                attractForce = f;
            }
            if (float.TryParse(repulInput.text, out float rf))
            {
                repulseForce = rf;
            }
        }
    }

    public void Temps(float temps = 1)
    {
        time = temps;
        Time.timeScale = time;
    }

    public void Pause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            pause.SetActive(true);
        }
        else
        {
            pause.SetActive(false);
            Time.timeScale = time;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Spawn()
    {
        spawner.Spawn(spawnQuant,spawnElec);
    }

    /*
    void GetForce(Transform thit, Transform tar, int index)
    {
        //Vector3 dir = (transform.position - trans.position) * (Mathf.Pow((transform.position - trans.position).magnitude, 2) - transform.localScale.x);

        GPU.SetFloats("target", new float[3] { tar.position.x, tar.position.y, tar.position.z });
        GPU.SetFloats("pos", new float[3] { thit.position.x, thit.position.y, thit.position.z });
        GPU.SetFloat("dist", Mathf.Abs((thit.position - tar.position).magnitude - ((thit.localScale.x+tar.localScale.x)*0.5f ) ) );// /(float)( (tar.GetComponent<Rigidbody2D>().velocity - thit.GetComponent<Rigidbody2D>().velocity).magnitude+1));
        //GPU.SetFloats("vel", new float[2] { tar.GetComponent<Rigidbody2D>().velocity.x, tar.GetComponent<Rigidbody2D>().velocity.y });

        GPU.SetBuffer(indexOfKernel, "Res", buffers[index]);
        GPU.Dispatch(indexOfKernel, 1, 1, 1);
    }
    */

    /*
void Attract()
{
    int i = 0;
    while (i < atoms.Length)
    {
        int o = 0;
        while (o < atoms[i].link.Length)
        {
            if (buffers[i*o] != null && atoms[i].link[o] != null)
            {
                float[] dir3 = new float[3] { 1, 1, 1 };
                buffers[i * o].GetData(dir3);
                Vector3 dir = new Vector3(dir3[0], dir3[1], dir3[2]);
                atoms[i].link[o].GetComponent<Rigidbody2D>().AddForce( (Vector2)(dir * atoms[i].force * attractForce), ForceMode2D.Force);//-atoms[i].link[o].GetComponent<Rigidbody2D>().velocity
            }
            o++;
        }
        i++;
    }
}
*/


}
