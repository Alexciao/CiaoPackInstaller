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
    #region Variables
    [Header("References")]
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Button openModButton;
    
    [Header("Directories")]
    [SerializeField] private string modDirectory;
    
    [Header("Settings")]
    [SerializeField] private bool showModButtonIfError;

    [Header("Messages")]
    [SerializeField] private string completedText;
    [SerializeField] private string notEmptyText;
    
    [Space]
    [SerializeField] private string generalInstallingText;
    [SerializeField] private string modInstallingText;
    [SerializeField] private string loaderInstallingText;
    #endregion

    void Start()
    {
        openModButton.gameObject.SetActive(false);
    }
    
    public void GetAndInstall()
    {
        if (!IsDirectoryEmpty(FileManager.Instance.GetMinecraftDirectory() + modDirectory) || !Directory.Exists(FileManager.Instance.GetMinecraftDirectory() + modDirectory))
        {
            progressText.color = Color.red;
            progressText.text = notEmptyText;
            if (showModButtonIfError)
            {
                ShowMinecraftButton();
            }
        }
        else
        {
            progressText.color = Color.white;
            progressText.text = generalInstallingText;
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
            progressText.text = loaderInstallingText;
        }
        else if (status == Status.InstallingMods)
        {
            progressText.color = Color.white;
            progressText.text = modInstallingText;
        }
    }

    public void ShowMinecraftButton()
    {
        if (FileManager.Instance.GetOS() == OS.Windows)
        {
            openModButton.gameObject.SetActive(true);
        }
        else
        {
            openModButton.gameObject.SetActive(false);
        }
    }
    
    public bool IsDirectoryEmpty(string path)
    {
        return !Directory.EnumerateFileSystemEntries(path).Any();
    }

    public void OpenDotMinecraft()
    {
        if (FileManager.Instance.GetOS() == OS.Windows)
        {
            System.Diagnostics.Process.Start("explorer.exe", FileManager.Instance.GetMinecraftDirectory());
        }
    }
}

public enum Status
{
    InstallingLoader,
    InstallingMods
}
