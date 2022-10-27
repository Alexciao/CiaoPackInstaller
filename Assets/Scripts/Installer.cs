using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Installer : MonoBehaviour
{
    #region Singleton
    public static Installer instance;
    
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
    
    public void InstallPack(Pack pack)
    {
        string loaderName = "loader.jar";
        
        
        Debug.Log("Downloading Loader...");
        GameManager.instance.SetTextStatus(Status.InstallingLoader);
        DownloadFile(pack.loader.url, FileManager.Instance.GetMinecraftDirectory(), loaderName);
        StartJar(pack.loader.args, FileManager.Instance.GetMinecraftDirectory() + "/" + loaderName, pack.mcVersion);
        
        GameManager.instance.SetTextStatus(Status.InstallingMods);
        Debug.Log("Downloading mods...");
        for (int i = 0; i < pack.mods.Count; i++)
        {
            DownloadFile(pack.mods[i].url, FileManager.Instance.GetMinecraftDirectory() + "/mods", pack.mods[i].name + ".jar");
        }
        
        GameManager.instance.ResetText();
    }
    

    void DownloadFile(string url, string path, string outfile)
    {
        WebClient client = new WebClient();
        client.DownloadFile(url, path + "/" + outfile);
    }
    
    void StartJar(string args, string path, string mcversion)
    {
        Debug.Log("Starting loader...");
        System.Diagnostics.Process.Start("javaw","-jar " + path + " " + args + " -mcversion " + mcversion);
    }
}
