using log4net;
using log4net.Config;

public static class Logger
{
    private static ILog log = LogManager.GetLogger("LOGGER");


    public static ILog Log
    {
        get { return log; }
    }

    static Logger()
    {
        XmlConfigurator.Configure();
    }
}