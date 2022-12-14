using System;
using System.Collections.Generic;
using System.IO;
using Coosu.Beatmap;
using static OsuRTDataProvider.DefaultLanguage;

namespace OsuRTDataProvider.BeatmapInfo
{
    public class Beatmap
    {
        public string DownloadLink
        {
            get
            {
                if (BeatmapID != 0) return $"http://osu.ppy.sh/b/{BeatmapID}";
                return LANG_BEATMAP_NOT_FOUND;
            }
        }

        /// <summary>
        /// If BeatmapSetID > 0. Return beatmap's download link.
        /// </summary>
        public string DownloadLinkSet
        {
            get
            {
                if (BeatmapSetID > 0) return $"http://osu.ppy.sh/s/{BeatmapSetID}";
                return LANG_BEATMAP_NOT_FOUND;
            }
        }

        private int m_beatmap_set_id = -1;

        /// <summary>
        /// Return set id.
        /// If no found return -1;
        /// </summary>

        public string Version => _coosu == null ? "" : _coosu.Metadata.Version;
        public string Difficulty => Version;
        public string Creator => _coosu == null ? "" : _coosu.Metadata.Creator;
        public string Artist => _coosu == null ? "" : _coosu.Metadata.Artist;
        public string ArtistUnicode => _coosu == null ? "" : _coosu.Metadata.ArtistUnicode;
        public string Title => _coosu == null ? "" : _coosu.Metadata.Title;
        public string TitleUnicode => _coosu == null ? "" : _coosu.Metadata.TitleUnicode;
        public string AudioFilename => _coosu == null ? "" : _coosu.General.AudioFilename;
        public string BackgroundFilename => _coosu == null ? "" : _coosu.Events.BackgroundInfo?.Filename;
        public string VideoFilename => _coosu == null ? "" : _coosu.Events.VideoInfo?.Filename;

        public int OsuClientID { get; }

        /// <summary>
        /// Return the first of all possible beatmap set paths.
        /// If not found.return string.Empty.
        /// </summary>
        public string Folder { get; } = string.Empty;
        public string Filename { get; } = string.Empty;
        public string FilenameFull { get; } = string.Empty;
        public int BeatmapID { get; }
        public int BeatmapSetID
        {
            get
            {
                if (m_beatmap_set_id > 0) return m_beatmap_set_id;

                if (Folder.Length > 0)
                {
                    string name = Folder;
                    int len = name.IndexOf(' ');
                    if (len != -1)
                    {
                        string id = name.Substring(0, len);

                        if (int.TryParse(id, out m_beatmap_set_id))
                            return m_beatmap_set_id;
                    }
                }
                return -1;
            }
            private set => m_beatmap_set_id = value;
        }


        private static readonly Beatmap s_empty = new Beatmap(0, -1, -1, null);
        private static readonly Action<OsuReadOptions> ReadOptions = k =>
        {
            k.ExcludeSection("Editor");
            k.ExcludeSection("Difficulty");
            k.ExcludeSection("TimingPoints");
            k.ExcludeSection("Colours");
            k.ExcludeSection("HitObjects");
            k.IgnoreSample();
            k.IgnoreStoryboard();
        };

        private readonly LocalOsuFile _coosu;

        public static Beatmap Empty => s_empty;

        public Beatmap(int osu_id, int set_id, int id, string filename)
        {
            BeatmapSetID = set_id;
            OsuClientID = osu_id;
            BeatmapID = id;

            if (filename != null)
            {
                Folder = Path.GetDirectoryName(filename);
                Filename = Path.GetFileName(filename);
                FilenameFull = filename;
                _coosu = OsuFile.ReadFromFile(filename, ReadOptions);
            }
        }

        public static bool operator ==(Beatmap a, Beatmap b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            if (a is null && !(b is null) ||
              !(a is null) && b is null)
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(Beatmap a, Beatmap b)
        {
            if (a is null && b is null)
            {
                return false;
            }
            if (a is null && !(b is null) ||
              !(a is null) && b is null)
            {
                return true;
            }
            return !a.Equals(b);
        }

        private static string GetPropertyValue(string line)
        {
            int pos = line.IndexOf(':');
            return line.Substring(pos + 1).Trim();
        }

        public override bool Equals(object obj)
        {
            if (obj is Beatmap beatmap)
            {
                return BeatmapID == beatmap.BeatmapID &&
                       BeatmapSetID == beatmap.BeatmapSetID &&
                       Difficulty == beatmap.Difficulty &&
                       Creator == beatmap.Creator &&
                       Artist == beatmap.Artist &&
                       Title == beatmap.Title &&
                       Filename == beatmap.Filename;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -173464191;
            hashCode = hashCode * -1521134295 + BeatmapID.GetHashCode();
            hashCode = hashCode * -1521134295 + BeatmapSetID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Difficulty ?? "");
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Creator ?? "");
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Artist ?? "");
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title ?? "");
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Filename ?? "");
            return hashCode;
        }
    }
}