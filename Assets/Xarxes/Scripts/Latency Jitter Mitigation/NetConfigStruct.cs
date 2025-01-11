public struct NetConfig
{
    public bool jitter;
    public bool packetLoss;

    public int minJitt;
    public int maxJitt;
    public int lossThreshold;

    public static NetConfig ApplyDefault()
    {
        NetConfig config = new NetConfig();

        config.jitter = true;
        config.packetLoss = true;
        config.minJitt = 0;
        config.maxJitt = 800;
        config.lossThreshold = 90;

        return config;
    }
}