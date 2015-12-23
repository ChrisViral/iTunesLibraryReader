using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;

namespace iTunesLibraryReader.Library
{
    /// <summary>
    /// A container class for the whole Music Library
    /// NOTE: Should probably rework this to use Dictionary :/
    /// </summary>
    public class MusicLib
    {
        #region Instance
        private static MusicLib _current = null;
        /// <summary>
        /// Current instance of the library
        /// </summary>
        public static MusicLib current
        {
            get { return _current; }
        }
        #endregion

        #region Properties
        private readonly List<Artist> _artists = new List<Artist>();
        /// <summary>
        /// Sorted list of the artists
        /// </summary>
        public List<Artist> artists
        {
            get { return this._artists; }
        }

        private readonly List<Album> _albums = new List<Album>();
        /// <summary>
        /// All the albums by all the artists
        /// </summary>
        public List<Album> albums
        {
            get { return this._albums; }
        }

        private readonly List<Song> _songs = new List<Song>();
        /// <summary>
        /// All the songs by all the artists
        /// </summary>
        public List<Song> songs
        {
            get { return this._songs; }
        }

        private int _artistsCount = 0;
        /// <summary>
        /// Amount of artists in the library
        /// </summary>
        public int artistsCount
        {
            get { return this._artistsCount; }
        }

        private int _albumCount = 0;
        /// <summary>
        /// Amount of albums in the library
        /// </summary>
        public int albumCount
        {
            get { return this._albumCount; }
        }

        private int _songCount = 0;
        /// <summary>
        /// Amount of songs in the library
        /// </summary>
        public int songCount
        {
            get { return this._songCount; }
        }

        private TimeSpan _totalTime = TimeSpan.Zero;
        /// <summary>
        /// Total running time of the library
        /// </summary>
        public TimeSpan totalTime
        {
            get { return this._totalTime; }
        }

        private FileSize _totalSize = FileSize.zero;
        /// <summary>
        /// Total size of the library
        /// </summary>
        public FileSize totalSize
        {
            get { return this._totalSize; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new MusicLib. Privacy prevents creating objects on the fly externally
        /// </summary>
        private MusicLib() { }
        #endregion

        #region Methods
        /// <summary>
        /// Sets up the library from the given list of XML nodes
        /// </summary>
        /// <param name="nodes">Nodes to create the library from</param>
        /// <returns>If the creation succeeded</returns>
        private bool SetupLibrary(XmlNodeList nodes)
        {
            List<XmlNode> playCounts;
            List<Dictionary<string, Value>> tracks = ParseTrackList(nodes, out playCounts);
            if (tracks.Count > 0)
            {
                ToolStripProgressBar progressBar = LibraryReader.current.progressBar;
                for (int i = 0; i < tracks.Count; i++)
                {
                    CreateSong(tracks[i], playCounts[i]);
                    progressBar.PerformStep();
                }
                progressBar.Style = ProgressBarStyle.Marquee;
                UpdateAll();
                progressBar.Style = ProgressBarStyle.Continuous;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to fetch an artist from the library. If fails, creates artist, adds it to the library, and returns it.
        /// </summary>
        /// <param name="name">Name of the artist to get</param>
        /// <returns>The artist of the given name</returns>
        public Artist GetAddArtist(string name)
        {
            foreach(Artist artist in this._artists)
            {
                if (artist.name == name) { return artist; }
            }
            return AddArtist(name);
        }

        /// <summary>
        /// Adds the artist to the library and returns it
        /// </summary>
        /// <param name="name">Name of the new artist to add</param>
        /// <returns>Artist created</returns>
        public Artist AddArtist(string name)
        {
            Artist a = new Artist(name);
            this._artists.Add(a);
            return a;
        }

        /// <summary>
        /// Finds and returns the artist of the given name
        /// </summary>
        /// <param name="name">Name of the Artist to find</param>
        /// <returns>Artist of the given name</returns>
        public Artist GetArtist(string name)
        {
            return this._artists.Find(a => a.name == name);
        }

        /// <summary>
        /// Tries to find a song by Track ID and returns it
        /// </summary>
        /// <param name="trackID">ID of the song to find</param>
        /// <returns>The song with the given ID</returns>
        public Song GetSong(int trackID)
        {
            return this._songs.Find(s => s.trackID == trackID);
        }

        /// <summary>
        /// Creates a new song from the given values
        /// </summary>
        /// <param name="track">Values of the song</param>
        /// <param name="playCount">Play count node reference</param>
        public void CreateSong(Dictionary<string, Value> track, XmlNode playCount)
        {
            Artist artist = null;
            Album album = null;
            Value temp;
            string s = string.Empty;
            if (track.TryGetValue("Artist", out temp))
            {
                temp.TryGetString(ref s);
            }
            artist = GetAddArtist(s);

            bool isComp = false;
            if (track.TryGetValue("Compilation", out temp))
            {
                temp.TryGetBool(ref isComp);
            }

            s = string.Empty;
            if (track.TryGetValue("Album", out temp))
            {
                temp.TryGetString(ref s);
            }
            album = artist.GetAddAlbum(s, isComp);
            Song song = new Song(artist, album, track, playCount, isComp);
            album.AddSong(song);
            if (song.isCompilation)
            {
                Artist.various.GetAddAlbum(album.title, false).AddSong(song);
            }
        }

        /// <summary>
        /// Updates and sorts all artists, albums, and songs
        /// </summary>
        private void UpdateAll()
        {
            foreach (Artist artist in this._artists)
            {
                artist.Update();
                this._totalTime.Add(artist.totalTime);
                this._totalSize.Add(artist.totalSize);
            }
            this._artists.Sort();
            this._artistsCount = this._artists.Count;
            if (Artist.various.albums.Count > 0)
            {
                foreach(Artist artist in this._artists)
                {
                    foreach(Album album in artist.albums)
                    {
                        if (album.artist != Artist.various) { this._albums.Add(album); }
                    }
                    this._songs.AddRange(artist.songs);
                }
                Artist.various.Update();
                this._albums.AddRange(Artist.various.albums);
            }
            else
            {
                foreach(Artist artist in this._artists)
                {
                    this._albums.AddRange(artist.albums);
                    this._songs.AddRange(artist.songs);
                }
            }         
            this._albumCount = this._albums.Count;
            this._songCount = this._songs.Count;
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Creates and sets up a new library from the given XML nodes list
        /// </summary>
        /// <param name="nodes">XML Nodes list from which to create the document</param>
        /// <returns>If the setup succeeded</returns>
        public static bool SetupNewLibrary(XmlNodeList nodes)
        {
            MusicLib lib = new MusicLib();
            if (lib.SetupLibrary(nodes))
            {
                _current = lib;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clears the current music library
        /// </summary>
        public static void Clear()
        {
            _current = null;
        }

        /// <summary>
        /// If the given XML node is a key
        /// </summary>
        /// <param name="node">XML node to identify</param>
        /// <returns>True if the node is a key</returns>
        private static bool NodeIsKey(XmlNode node)
        {
            return node.NodeType == XmlNodeType.Element && node.Name == "key";
        }

        /// <summary>
        /// If the given XML node is a dict
        /// </summary>
        /// <param name="node">XML node to identify</param>
        /// <returns>True if the node is dict</returns>
        private static bool NodeIsDict(XmlNode node)
        {
            return node.NodeType == XmlNodeType.Element && node.Name == "dict";
        }

        /// <summary>
        /// PArses all the tracks in the XML document
        /// </summary>
        /// <param name="nodes">XML nodes list of the document</param>
        /// <param name="playCounts">Value to output the list of play count nodes references to</param>
        /// <returns>A list of value dictionaries for each track</returns>
        private static List<Dictionary<string, Value>> ParseTrackList(XmlNodeList nodes, out List<XmlNode> playCounts)
        {
            List<Dictionary<string, Value>> result = new List<Dictionary<string, Value>>();
            playCounts = new List<XmlNode>();
            for (int i = 0; i < nodes.Count; i += 2)
            {
                XmlNode key = nodes[i], dict = nodes[i + 1];
                if (NodeIsKey(key) && NodeIsDict(dict))
                {
                    XmlNode playCount;
                    Dictionary<string, Value> track = ParseTrack(dict.ChildNodes, out playCount);
                    if (track.Count > 0)
                    {
                        result.Add(track);
                        playCounts.Add(playCount);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Creates a value dictionary for all the elements of a track from a given XML node list
        /// </summary>
        /// <param name="nodes">Nodes to create the track from</param>
        /// <param name="playCount">Reference to the play count node to output</param>
        /// <returns>A dictionary of values representing the track</returns>
        private static Dictionary<string, Value> ParseTrack(XmlNodeList nodes, out XmlNode playCount)
        {
            playCount = null;
            Dictionary<string, Value> result = new Dictionary<string, Value>();
            if (nodes.Count == 0) { return result; }

            for (int i = 0; i < nodes.Count; i += 2)
            {
                XmlNode key = nodes[i], value = nodes[i + 1];
                if (NodeIsKey(key))
                {
                    string name = key.FirstChild.Value;
                    switch (name)
                    {
                        case "Track ID":
                        case "Size":
                        case "Total Time":
                        case "Disc Number":
                        case "Disc Count":
                        case "Track Number":
                        case "Track Count":
                        case "Year":
                        case "Compilation":
                        case "Name":
                        case "Artist":
                        case "Album":
                        case "Genre":
                            break;

                        case "Play Count":
                            playCount = value.FirstChild; break;

                        default:
                            continue;
                    }
                    string type = value.Name;
                    switch (type)
                    {
                        case "string":
                            {
                                result.Add(name, new Value(value.FirstChild.Value, Value.ValueType.STRING));
                                break;
                            }
                        case "true":
                        case "false":
                            {
                                bool b;
                                if (bool.TryParse(type, out b))
                                {
                                    result.Add(name, new Value(b, Value.ValueType.BOOL));
                                }
                                break;
                            }
                        case "integer":
                            {
                                int n;
                                if (int.TryParse(value.FirstChild.Value, out n))
                                {
                                    result.Add(name, new Value(n, Value.ValueType.INTEGER));
                                }
                                break;
                            }
                    }
                }
            }
            if (playCount == null)
            {
                XmlDocument doc = LibraryReader.current.document;
                XmlNode k = doc.CreateElement("key"), c = doc.CreateElement("integer");
                k.AppendChild(doc.CreateTextNode("Play Count"));
                c.AppendChild(doc.CreateTextNode("0"));
                playCount = c.FirstChild;
                nodes[0].ParentNode.AppendChild(k);
                nodes[0].ParentNode.AppendChild(c);
                result.Add("Play Count", new Value(0, Value.ValueType.INTEGER));
            }
            return result;
        }
        #endregion
    }
}
