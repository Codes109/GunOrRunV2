using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradePanelButtonFunctions : MonoBehaviour
{
    public GameObject upgradePanel;
    public GameObject upgradesManager;

    private void Start()
    {
        upgradesManager = GameObject.FindGameObjectWithTag("UpgradesManager");
    }

    public void OpenPanel()
    {
        upgradePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        upgradePanel.SetActive(false);
    }

    public void EquipStandardMissile()
    {
        upgradesManager.GetComponent<UpgradesManager>().isMissile = true;
        upgradesManager.GetComponent<UpgradesManager>().isRapidFire = false;
        upgradesManager.GetComponent<UpgradesManager>().isFastShip = false;
    }

    public void EquipRapidFire()
    {
        upgradesManager.GetComponent<UpgradesManager>().isMissile = false;
        upgradesManager.GetComponent<UpgradesManager>().isRapidFire = true;
        upgradesManager.GetComponent<UpgradesManager>().isFastShip = false;
    }

    public void EquipFastShip()
    {
        upgradesManager.GetComponent<UpgradesManager>().isMissile = false;
        upgradesManager.GetComponent<UpgradesManager>().isRapidFire = false;
        upgradesManager.GetComponent<UpgradesManager>().isFastShip = true;
    }
}
