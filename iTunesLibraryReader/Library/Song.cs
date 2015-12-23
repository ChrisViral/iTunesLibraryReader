using System;
using System.Collections.Generic;
using System.Xml;

namespace iTunesLibraryReader.Library
{
    /// <summary>
    /// A Song object holding all the information on the song
    /// </summary>
    public class Song : IComparable<Song>
    {
        #region Properties
        private readonly int _trackID = 0;
        /// <summary>
        /// Track ID of the Song
        /// </summary>
        public int trackID
        {
            get { return this._trackID; }
        }

        private readonly string _title = "title";
        /// <summary>
        /// Title of the Song
        /// </summary>
        public string title
        {
            get { return this._title; }
        }

        private readonly Artist _artist = null;
        /// <summary>
        /// Artist of the Song
        /// </summary>
        public Artist artist
        {
            get { return this._artist; }
        }

        private readonly Album _album = null;
        /// <summary>
        /// Album of the Song
        /// </summary>
        public Album album
        {
            get { return this._album; }
        }

        private ushort _playCount = 0;
        /// <summary>
        /// Play counts of the Song
        /// </summary>
        public ushort playCount
        {
            get { return this._playCount; }
            set { this._playCount = value; }
        }

        private readonly XmlNode _playCountNode = null;
        /// <summary>
        /// Play count node reference
        /// </summary>
        public XmlNode playCountNode
        {
            get { return this._playCountNode; }
        }

        private readonly byte _CDNumber = 1;
        /// <summary>
        /// CD number of this Song
        /// </summary>
        public byte CDNumber
        {
            get { return this._CDNumber; }
        }

        private readonly byte _CDCount = 0;
        /// <summary>
        /// The CD count for this Song
        /// </summary>
        public byte CDCount
        {
            get { return this._CDCount; }
        }

        private readonly byte _track = 1;
        /// <summary>
        /// The track for this Song
        /// </summary>
        public byte track
        {
            get { return this._track; }
        }

        private readonly byte _trackCount = 0;
        /// <summary>
        /// The track count for this song
        /// </summary>
        public byte trackCount
        {
            get { return this._trackCount; }
        }

        private readonly ushort _year = 0;
        /// <summary>
        /// Year this song was released
        /// </summary>
        public ushort year
        {
            get { return this._year; }
        }

        private readonly string _genre = "genre";
        /// <summary>
        /// Genre of this song
        /// </summary>
        public string genre
        {
            get { return this._genre; }
        }

        private readonly bool _isCompilation = false;
        /// <summary>
        /// If this song is off a compilations album
        /// </summary>
        public bool isCompilation
        {
            get { return this._isCompilation; }
        }

        private readonly TimeSpan _time = TimeSpan.Zero;
        /// <summary>
        /// Duration of the song
        /// </summary>
        public TimeSpan time
        {
            get { return this._time; }
        }

        private readonly FileSize _size = FileSize.zero;
        /// <summary>
        /// File size of the song
        /// </summary>
        public FileSize size
        {
            get { return this._size; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new song from the given artist, album, and values dictionary
        /// </summary>
        /// <param name="artist">Artist of the song</param>
        /// <param name="album">Album the song is off</param>
        /// <param name="track">Dictinonary of values to get the song info from</param>
        /// <param name="playCount">PlayCount nodes reference (should probs change to a pointer)</param>
        /// <param name="isComp">If the song is off a compilations album</param>
        public Song(Artist artist, Album album, Dictionary<string, Value> track, XmlNode playCount, bool isComp)
        {
            this._artist = artist;
            this._album = album;
            this._playCountNode = playCount;
            this._isCompilation = isComp;
            Value temp;
            int i = 0;
            if (track.TryGetValue("Track ID", out temp))
            {
                temp.TryGetInt(ref this._trackID);
            }
            if (track.TryGetValue("Size", out temp) && temp.TryGetInt(ref i))
            {
                this._size = FileSize.FromInt32(i);
            }
            if (track.TryGetValue("Name", out temp))
            {
                temp.TryGetString(ref this._title);
            }
            if (track.TryGetValue("Play Count", out temp) && temp.TryGetInt(ref i))
            {
                this._playCount = (ushort)i;
            }
            if (track.TryGetValue("Disc Number", out temp) && temp.TryGetInt(ref i))
            {
                this._CDNumber = (byte)i;
            }
            if (track.TryGetValue("Disc Count", out temp) && temp.TryGetInt(ref i))
            {
                this._CDCount = (byte)i;
            }
            if (track.TryGetValue("Track Number", out temp) && temp.TryGetInt(ref i))
            {
                this._track = (byte)i;
            }
            if (track.TryGetValue("Track Count", out temp) && temp.TryGetInt(ref i))
            {
                this._trackCount = (byte)i;
            }
            if (track.TryGetValue("Year", out temp) && temp.TryGetInt(ref i))
            {
                this._year = (ushort)i;
            }
            if (track.TryGetValue("Genre", out temp))
            {
                temp.TryGetString(ref this._genre);
            }
            if (track.TryGetValue("Total Time", out temp) && temp.TryGetInt(ref i))
            {
                this._time = TimeSpan.FromMilliseconds(i);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Compares a song by track number and then by title
        /// </summary>
        /// <param name="other">Other song to compare to</param>
        /// <returns>1 if this song comes after the other, -1 if it comes before, 0 if they are equal</returns>
        public int CompareTo(Song other)
        {
            if (this._track == other._track) { return this._title.CompareTo(other._title); }
            return this._track - other._track;
        }
        #endregion
    }
}
