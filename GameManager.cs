using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;




    private void Start()
    {
        SceneManager.LoadScene("Home");
    }
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud.gameObject);
            Destroy(menu.gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoad;
        
    }

    public List<Sprite> PlayerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;


    public Players player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAim;
    public GameObject hud;
    public GameObject menu;


    //Logic

    public int pesos;
    public int experience;

    //floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }


    //Hitpoint Bar

    public void OnHitpointChange()
    {

        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);

    }



    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level?
        if(weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if(pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }


        
        return false;
    }

   


    //Experience System

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while(experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) //Max Level
                return r;
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;


        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;

    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        Debug.Log("Level Up!");
        player.OnLevelUp();
    }

    //OnScene Loaded
    public void OnSceneLoad(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;   }



    //SaveStat
    /*
        *Int preferedSkin
        *Int pesos
        *Int experience
        *Int weaponLevel
        *
        *
        */
    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s,LoadSceneMode mode)
    {

        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');


        //Chang player skin

        pesos = int.Parse(data[0]);


        //Experience
        experience = int.Parse(data[0]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        //Chang the weapon Level
        weapon.SetWeaponLevel(int.Parse(data[0]));
    }


}
