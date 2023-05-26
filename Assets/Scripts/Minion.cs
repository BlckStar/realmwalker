using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class Minion : MonoBehaviour
{
    public int Level;
    public Vector3 Destination;
    public float MovementSpeed = 10f;
    public bool reached;
    public HQ EnemyHQ;

    private Animator Animator;
    public bool isDead;
    private int health = 100;
    public Slider healthBar;

    public TextMeshProUGUI lvlText;
    public void OnSpawn()
    {
        this.health = (int)(100f * math.pow(2f, this.Level));
        this.healthBar.maxValue = this.health;
        this.healthBar.value = this.health;
        this.Animator = this.GetComponent<Animator>();
        this.lvlText.text = "LVL" + this.Level;
        Animator.SetBool("Walk Forward", true);
        EnemyHQ = FindObjectsByType<HQ>(FindObjectsSortMode.None).Where((HQ hq) => hq.tag != this.tag).First();
        this.transform.Rotate(Vector3.up, 90);
    }

    public void Update()
    {
        if (reached)
        {
            return;
        }

        float3 to = (Destination - this.transform.position).normalized;
        float3 delta = to * (Time.deltaTime * MovementSpeed);

        if (math.length(Destination - this.transform.position) > 0.1f)
        {
            this.transform.position += ((Vector3)delta);
        }
        else
        {
            this.Animator.SetBool("Walk Forward", false);
            StartCoroutine("Attack");
            this.reached = true;
        }
    }

    public IEnumerator Attack()
    {
        this.Animator.SetTrigger("Attack 02");
        yield return new WaitForSeconds(0.8f);
        if (!this.isDead)
        {
            // attack
            EnemyHQ.Hit((int)(10f * math.pow(2f, this.Level)));
        
            yield return new WaitForSeconds(0.2f);
            StartCoroutine("Attack");
        }
    }

    public void Hit(int dmg)
    {
        if (this.isDead)
        {
            return;
        }

        this.health -= dmg;
        this.healthBar.value = this.health;
        if (this.health <= 0)
        {
            this.isDead = true;
            this.Animator.SetTrigger("Die");
            Destroy(this.gameObject, 1f);
        }
    }

}
