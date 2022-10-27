using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    
    private void Awake() 
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }
    #endregion
    
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private string completedText;

    public void GetAndInstall()
    {
        if (!IsDirectoryEmpty(FileManager.Instance.GetMinecraftDirectory() + "/mods") || !Directory.Exists(FileManager.Instance.GetMinecraftDirectory() + "/mods"))
        {
            progressText.color = Color.red;
            progressText.text = "The 'mods' folder is not empty or doesn't exist.";
        }
        else
        {
            progressText.color = Color.white;
            progressText.text = "Installing pack...";
            FileManager.Instance.ChoosePack();
        }
    }

    public void ContinueInstall()
    {
        JSONManager.instance.LoadPack();
        Installer.instance.InstallPack(JSONManager.instance.pack);
        // progressText.text = defaultText;
    }

    public void ResetText()
    {
        progressText.color = Color.green;
        progressText.text = completedText;
    }
    
    public void SetTextStatus(Status status)
    {
        if (status == Status.InstallingLoader)
        {
            progressText.color = Color.white;
            progressText.text = "Installing Loader...";
        }
        else if (status == Status.InstallingMods)
        {
            progressText.color = Color.white;
            progressText.text = "Installing mods...";
        }
    }
    
    public bool IsDirectoryEmpty(string path)
    {
        return !Directory.EnumerateFileSystemEntries(path).Any();
    }
}

public enum Status
{
    InstallingLoader,
    InstallingMods
}
