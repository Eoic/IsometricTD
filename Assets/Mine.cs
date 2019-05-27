using UnityEngine;
using UnityEngine.EventSystems;

public enum MineType
{
    STONE = 0,
    IRON = 1
}

public class Mine : MonoBehaviour
{
    public GameObject mineEntry;
    public GameObject mineBuildMenu;
    private MineType type;
    private bool isBuilt = false;
    private int digAttempts = 100;
    private int amountPerDig = 5;

    private void OnMouseEnter()
    {
        if (!isBuilt)
            mineBuildMenu.SetActive(true);
    }

    private void OnMouseExit()
    {
        mineBuildMenu.SetActive(false);
    }

    public void BuildStoneMine()
    {
        type = MineType.STONE;
        SetAsBuilt();
    }

    public void BuildIronMine()
    {
        type = MineType.IRON;
        SetAsBuilt();
    }

    void StartCollecting()
    {
        digAttempts--;

        if (digAttempts == 0)
        {
            CancelInvoke("StartCollecting");
            return;
        }

        if (type == MineType.STONE)
            ResourceManager.Instance.AddStone(amountPerDig);
        else if (type == MineType.IRON)
            ResourceManager.Instance.AddIron(amountPerDig);
    }

    void SetAsBuilt()
    {
        if(GameAudioManager.instance != null)
            GameAudioManager.instance.Play("Build");
        else
            AudioManager.instance.Play("Building01");
        
        mineEntry.SetActive(true);
        InvokeRepeating("StartCollecting", 2f, 5f);
        isBuilt = true;
    }
}