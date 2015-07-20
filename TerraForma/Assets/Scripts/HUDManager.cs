using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour {

    GameObject hud;
    GameObject playerHUD;
    GameObject unitHUD;
    GameObject unitActionsHUD;

    Text p_name;
    Text p_mana;

    Text u_name;
    Text u_health;
    Text u_att;
    Text u_def;
    Text u_speed;

	// Use this for initialization
	void Start () {
        if (!hud) Init();
	}

    public void Init()
    {
        hud = GameObject.FindGameObjectWithTag("HUD");
        playerHUD = hud.transform.Find("Player_Info").gameObject;
        unitHUD = hud.transform.Find("Unit_Info").gameObject;
        unitActionsHUD = hud.transform.Find("Unit_Actions").gameObject;

        p_name = playerHUD.transform.Find("Player_name").gameObject.GetComponent<Text>();
        p_mana = playerHUD.transform.Find("Player_mana").gameObject.GetComponent<Text>();

        u_name = unitHUD.transform.Find("Unit_name").gameObject.GetComponent<Text>();
        u_health = unitHUD.transform.Find("Unit_health").gameObject.GetComponent<Text>();
        u_att = unitHUD.transform.Find("Unit_attack").gameObject.GetComponent<Text>();
        u_def = unitHUD.transform.Find("Unit_defense").gameObject.GetComponent<Text>();
        u_speed = unitHUD.transform.Find("Unit_speed").gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPlayerInfo(Player p)
    {
        p_name.text = p.Name;
        p_mana.text = "Mana: " + p.Mana.ToString();
    }

    public void SetUnitInfo(Unit u)
    {
        u_name.text = u.Name;
        string h = "Health: " + u.Health;
        u_health.text = h;
        u_att.text = "Att: " + u.Attack.ToString();
        u_def.text = "Def: " + u.PhysicalDefense.ToString();
        u_speed.text = "Speed: " + u.Move.ToString();
    }

    public void HideUnitInfo()
    {
        unitHUD.SetActive(false);
    }

    public void ShowUnitInfo()
    {
        unitHUD.SetActive(true);
        unitActionsHUD.SetActive(true);
    }

    public void HideUnitActions()
    {
        unitActionsHUD.SetActive(false);
    }
}
