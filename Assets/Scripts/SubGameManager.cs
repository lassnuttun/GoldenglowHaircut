using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using System.Threading.Tasks;

// 需要与 FightManager 一起重构
public class SubGameManager
{
    public static SubGameManager Instance { get; private set; } = new SubGameManager();

    public SubgameConfigInfo subgameInfo { get; private set; }

    public Player player { get; private set; }

    public List<Enemy> enemyList { get; private set; }


    public void Init(string gameID)
    {
        string file = AssetBundleManager.LoadResource<Object>(gameID, "config").ToString();
        subgameInfo = JsonConvert.DeserializeObject<SubgameConfigInfo>(file);

        Object resource = AssetBundleManager.LoadResource<Object>("Goldenglow", "skeleton");
        GameObject playerModel = Object.Instantiate(resource) as GameObject;
        player = playerModel.AddComponent<Player>();

        List<Vector3> enemyPos = new List<Vector3>();
        switch (subgameInfo.enemies.Length)
        {
            case 1:
                enemyPos.Add(new Vector3(5, -1.5f));
                break;
            case 2:
                enemyPos.Add(new Vector3(3, -1.5f));
                enemyPos.Add(new Vector3(6, -1.5f));
                break;
            case 3:
                enemyPos.Add(new Vector3(1, -1.5f));
                enemyPos.Add(new Vector3(3.5f, -1.5f));
                enemyPos.Add(new Vector3(6, -1.5f));
                break;
        }

        enemyList = new List<Enemy>();
        for (int i = 0; i < subgameInfo.enemies.Length; i++)
        {
            resource = AssetBundleManager.LoadResource<Object>(subgameInfo.enemies[i], "skeleton");
            GameObject enemyModel = Object.Instantiate(resource) as GameObject;
            enemyModel.name = subgameInfo.enemies[i];
            enemyModel.transform.position = enemyPos[i];
            Enemy enemy = enemyModel.AddComponent<Enemy>();

            file = AssetBundleManager.LoadResource<Object>(subgameInfo.enemies[i], "config").ToString();
            EnemyConfigInfo enemyConfigInfo = JsonConvert.DeserializeObject<EnemyConfigInfo>(file);
            enemy.maxHP = enemyConfigInfo.enemyHP;
            enemy.curHP = enemy.maxHP;
            enemyList.Add(enemy);
        }
    }
}

public class SubgameConfigInfo
{
    public string subgameID;

    public string subgameName;

    public string[] enemies;
}

public class EnemyConfigInfo
{
    public string enemyID;

    public string enemyName;

    public uint enemyHP;

    public uint enemySP;
}
