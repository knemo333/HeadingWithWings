using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Dash : BossSkillBase, IBoss_Skill
{
    //플레이어 방향으로 직선으로 돌진하는 스킬입니다.
    //EnemyAttackMelee와는 별개로 OverlapSphere 이용해서 돌진의 데미지를 따로 입혀주면 좋을 것 같습니다.
    //돌진은 점프와 마찬가지로 피아식별을 하지 않습니다.
    //피아식별무시는 PlayerTakeDamage와 EnemyTakeDamage가 상속받는 ITakeBossAttack 이라는 인터페이스로 모두 데미지를 줄 수 있습니다
    Boss_Skill_Manager skillManager;
    public float dotDamageTime = 0.1f;
    public Transform damageZone = null;

    private EnemyInfo enemyInfo = null;
    private Rigidbody2D rb2D = null;
    private bool isDash = false;
    private WaitForSeconds dotDamageSec = null;
    private const string PLAYER = "PLAYER";
    private const string ENEMY = "ENEMY";
    public AudioClip[] audioClips;

    private Vector3 dashPos = Vector3.zero;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        rb2D = GetComponent<Rigidbody2D>();
        dotDamageSec = new WaitForSeconds(dotDamageTime);
        skillManager = GetComponent<Boss_Skill_Manager>();
        enemyInfo = GetComponent<EnemyInfo>();
    }

    public void ActivateSkill()
    {
        skillManager.currentSkill = this;
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        StartCoroutine(ShowDamageZone());
        yield return new WaitForSeconds(0.5f);

        skillManager.audioSource.PlayOneShot(audioClips[0], 0.6f);
        isDash = true;
        StartCoroutine(DashDotDamage());
        rb2D.AddForce((dashPos - this.transform.position) * 2f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);

        rb2D.velocity = Vector2.zero;
        isDash = false;

        Boss_Skill_Manager.isSkillEnd = true;
        Boss_Skill_Manager.animator.SetTrigger("reset");
    }

    IEnumerator ShowDamageZone()
    {
        dashPos = enemyInfo.playerTransform.position;
        damageZone.rotation = Utilities.LookAt2(this.transform, dashPos);
        Vector2 Scaling = Vector2.one;
        Scaling.x = Vector2.Distance(dashPos, this.transform.position) * 1.25f * 0.5f;
        damageZone.localScale = Scaling;

        yield return new WaitForSeconds(0.5f);

        damageZone.localScale = Vector3.zero;
    }

    IEnumerator DashDotDamage()
    {
        while(isDash)
        {
            yield return dotDamageSec;

            Collider2D[] hitColls = Physics2D.OverlapCircleAll(this.transform.position, 2f);

            foreach(Collider2D hitColl in hitColls)
            {
                if((hitColl.CompareTag(PLAYER) || hitColl.CompareTag(ENEMY)) && hitColl.gameObject != this.gameObject)
                {
                    skillManager.audioSource.PlayOneShot(audioClips[1]);
                    ITakeBossAttack temp = hitColl.GetComponent<ITakeBossAttack>();
                    temp.TakeBossAttack(this.transform, skillManager.skillDamage, skillManager.skillKnockBackSize);
                }
            }
        }
    }
}
