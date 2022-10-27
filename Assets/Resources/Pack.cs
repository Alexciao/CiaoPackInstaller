using System.Collections.Generic;

public class Loader
{
    public string name { get; set; }
    public string url { get; set; }
    public string args { get; set; }
}

public class Mod
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Pack
{
    public string name { get; set; }
    public string mcVersion { get; set; }
    public Loader loader { get; set; }
    public List<Mod> mods { get; set; }
}