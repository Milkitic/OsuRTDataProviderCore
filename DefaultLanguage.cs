namespace OsuRTDataProvider
{
    internal static class DefaultLanguage
    {
        public const string LANG_OSU_NOT_FOUND = "[ID:{0}]Not found osu!.exe process";
        public const string LANG_OSU_FOUND = "[ID:{0}]Found osu!.exe process";
        public const string LANG_TOURNEY_HINT = "Tourney Mode: {0}";

        public const string CHECK_GOTO_RELEASE_PAGE_HINT = "Enter \"ortdp releases\" to open the releases page in your browser.";
        public const string LANG_CHECK_ORTDP_UPDATE = "Found a new version of OsuRTDataProvider({0})";
        public const string LANG_INIT_STATUS_FINDER_FAILED = "[ID:{0}]Init StatusFinder Failed! Retry after {1} seconds";
        public const string LANG_INIT_STATUS_FINDER_SUCCESS = "[ID:{0}]Init StatusFinder Success!";
        public const string LANG_INIT_PLAY_FINDER_FAILED = "[ID:{0}]Init PlayFinder Failed! Retry after {1} seconds";
        public const string LANG_INIT_PLAY_FINDER_SUCCESS = "[ID:{0}]Init PlayFinder Success!";
        public const string LANG_INIT_BEATMAP_FINDER_FAILED = "[ID:{0}]Init BeatmapFinder Failed! Retry after {1} seconds";
        public const string LANG_INIT_BEATMAP_FINDER_SUCCESS = "[ID:{0}]Init BeatmapFinder Success!";
        public const string LANG_INIT_MODE_FINDER_FAILED = "[ID:{0}]Init ModeFinder Failed! Retry after {1} seconds";
        public const string LANG_INIT_MODE_FINDER_SUCCESS = "[ID:{0}]Init ModeFinder Success!";
        public const string LANG_INIT_HIT_EVENT_SUCCESS = "[ID:{0}]Init HitEventFinder Success!";
        public const string LANG_INIT_HIT_EVENT_FAIL = "[ID:{0}]Init HitEventFinder Failed! Retry after {1} seconds";

        public const string LANG_BEATMAP_NOT_FOUND = "Beatmap not found";

        public const string ListenInterval = "Listen interval(ms)";
        public const string EnableTourneyMode = "Tourney mode";
        public const string TeamSize = "Team size";
        public const string DebugMode = "Debug mode";
        public const string ForceOsuSongsDirectory = "Force OSU! songs directory";
        public const string GameMode = "Game Mode";
        public const string DisableProcessNotFoundInformation = "Disable OSU! process not found information";
        public const string EnableModsChangedAtListening = "Enable Mods Changed At Listening(Experimental)";
    }
}