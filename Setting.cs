namespace OsuRTDataProvider
{
    public static class Setting
    {
        public static bool DebugMode = false;
        public static int ListenInterval = 100;//ms
        public static bool EnableTourneyMode = false;
        public static int TeamSize = 1;
        public static string ForceOsuSongsDirectory = "";
        public static string GameMode = "Auto";
        public static bool DisableProcessNotFoundInformation = false;
        public static bool EnableModsChangedAtListening = false;

        public static double CurrentOsuVersionValue => Utils.ConvertVersionStringToValue(Variables.OsuVersion);
    }

    internal static class Variables
    {
        public static string SongsPath = string.Empty;
        public static string OsuVersion = string.Empty;
        public static string Username = string.Empty;
    }
}