using System;
using System.Collections.Generic;
using System.Linq;

namespace iTunesLibraryReader.Library
{
    /// <summary>
    /// Object representing an album
    /// </summary>
    public class Album : IComparable<Album>
    {
        /// <summary>
        /// Represents a CD off a given album to keep track of CD count
        /// </summary>
        public class CD : IComparable<CD>
        {
            #region Properties
            private readonly List<Song> _songs = new List<Song>();
            /// <summary>
            /// All the songs on this CD
            /// </summary>
            public List<Song> songs
            {
                get { return this._songs; }
            }

            private readonly byte _CDNumber = 0;
            /// <summary>
            /// One based index of this CD
            /// </summary>
            public byte CDNumber
            {
                get { return this._CDNumber; }
            }

            private byte _trackCount = 0;
            /// <summary>
            /// Amount of songs on this CD
            /// </summary>
            public byte trackCount
            {
                get { return this._trackCount; }
            }
            #endregion

            #region Constructor
            /// <summary>
            /// Creates a new CD from the given amount of songs
            /// </summary>
            /// <param name="num">Amount of songs on the CD</param>
            public CD(byte num)
            {
                this._CDNumber = num;
            }
            #endregion

            #region Methods
            /// <summary>
            /// Adds the song to the CD
            /// </summary>
            /// <param name="song">Song to add</param>
            public void AddSong(Song song)
            {
                this._songs.Add(song);
            }

            /// <summary>
            /// Updates an sorts all the songs on this CD
            /// </summary>
            public void Update()
            {
                this._trackCount = _songs.Max(s => s.trackCount);
                this._songs.Sort();
            }

            /// <summary>
            /// Compares this CD to another instance for sorting
            /// </summary>
            /// <param name="other">Other CD to compare to</param>
            /// <returns>1 if the CD comes after the other, -1 if it comes before, 0 if both are equal</returns>
            public int CompareTo(CD other)
            {
                return this._CDNumber - other._CDNumber;
            }
            #endregion
        }

        #region Properties
        private readonly string _title = "album";
        /// <summary>
        /// Title of the album
        /// </summary>
        public string title
        {
            get { return this._title; }
        }

        private Artist _artist = null;
        /// <summary>
        /// Artist of this album
        /// </summary>
        public Artist artist
        {
            get { return this._artist; }
        }

        private readonly List<CD> _CDs = new List<CD>();
        /// <summary>
        /// CDs on this album
        /// </summary>
        public List<CD> CDs
        {
            get { return this._CDs; }
        }

        private readonly List<Song> _songs = new List<Song>();
        /// <summary>
        /// Songs on this album
        /// </summary>
        public List<Song> songs
        {
            get { return this._songs; }
        }

        private TimeSpan _time = TimeSpan.Zero;
        /// <summary>
        /// Total time of this album
        /// </summary>
        public TimeSpan time
        {
            get { return this._time; }
        }

        private FileSize _size = FileSize.zero;
        /// <summary>
        /// Total size of this album
        /// </summary>
        public FileSize size
        {
            get { return this._size; }
        }

        private byte _trackCount = 0;
        /// <summary>
        /// Amount of songs on this album
        /// </summary>
        public byte trackCount
        {
            get { return this._trackCount; }
        }

        private byte _cdCount = 0;
        /// <summary>
        /// Amount of CDs for this album
        /// </summary>
        public byte cdCount
        {
            get { return this._cdCount; }
        }

        private ushort _year = 0;
        /// <summary>
        /// Release year of this album
        /// </summary>
        public ushort year
        {
            get { return this._year; }
        }

        private string _genre = "genre";
        /// <summary>
        /// Genre of this album
        /// </summary>
        public string genre
        {
            get { return this._genre; }
        }

        private bool _isCompilation = false;
        /// <summary>
        /// If this album is a compilation album
        /// </summary>
        public bool isCompilation
        {
            get { return this._isCompilation; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new Album from the given title and artist
        /// </summary>
        /// <param name="title">Title of the album</param>
        /// <param name="artist">Artist of the album</param>
        public Album(string title, Artist artist)
        {
            this._title = title;
            this._artist = artist;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a song to the album
        /// </summary>
        /// <param name="song">Song to add</param>
        public void AddSong(Song song)
        {
            CD cd = GetAddCD(song.CDNumber);
            cd.AddSong(song);
        }

        /// <summary>
        /// Fetches a song from the album
        /// </summary>
        /// <param name="title">Song to find</param>
        /// <returns>Title of the song of the given title</returns>
        public Song GetSong(string title)
        {
            return this._songs.Find(s => s.title == title);
        }

        /// <summary>
        /// Tries to get a CD from the album. If fails, creates a new CD at this CD number and adds it to the album, then returns it.
        /// </summary>
        /// <param name="num">CD number to get</param>
        /// <returns>The CD ath this number</returns>
        public CD GetAddCD(byte num)
        {
            foreach(CD cd in this._CDs)
            {
                if (cd.CDNumber == num) { return cd; }
            }
            return AddCD(num);
        }

        /// <summary>
        /// Adds a CD to the album
        /// </summary>
        /// <param name="num">CD number to add</param>
        /// <returns>The CD created</returns>
        public CD AddCD(byte num)
        {
            CD cd = new CD(num);
            this.CDs.Add(cd);
            return cd;
        }

        /// <summary>
        /// Updates and sorts all the CDs and songs on this album
        /// </summary>
        public void Update()
        {
            foreach (CD cd in this._CDs)
            {
                cd.Update();
                this._trackCount += cd.trackCount;
            }
            this._CDs.Sort();
            foreach(CD cd in this._CDs)
            {
                this._songs.AddRange(cd.songs);
            }
            foreach(Song song in this._songs)
            {
                this._time = this._time.Add(song.time);
                this._size = this._size.Add(song.size);
            }
            this._trackCount = (byte)this._CDs.Sum(cd => cd.trackCount);
            this._cdCount = this._songs.Max(s => s.CDCount);
            this._year = this._songs.Mode(s => s.year);
            this._genre = this._songs.Mode(s => s.genre);
            this._isCompilation = this._songs.Any(s => s.isCompilation);
        }

        /// <summary>
        /// Compares this Album by year and then by title
        /// </summary>
        /// <param name="other">Other album to compare to</param>
        /// <returns>1 if this instance comes after the other, -1 if it comes before, 0 if they are equal</returns>
        public int CompareTo(Album other)
        {
            if (other._year == this._year) { return this.title.CompareTo(other._title); }
            return this._year - other._year;
        }

        /// <summary>
        /// Returns the string track count for a given song off this Album
        /// </summary>
        /// <param name="song">Song to get the track count of</param>
        /// <returns>String track count of this song</returns>
        public string GetTrackString(Song song)
        {
            if (song.CDCount > 1)
            {
                CD cd = this._CDs.Find(c => c.CDNumber == song.CDNumber);
                if (cd.trackCount > 0)
                {
                    return song.track + " of " + cd.trackCount;
                }
            }
            else if (this._trackCount > 0)
            {
                return song.track + " of " + this._trackCount;
            }
            return song.track.ToString();
        }

        /// <summary>
        /// Returns the string cd count for a given song off this Album
        /// </summary>
        /// <param name="song">Song to get the CD count from</param>
        /// <returns>The string CD count for this song</returns>
        public string GetCDString(Song song)
        {
            if (this._cdCount > 0)
            {
                return song.CDNumber + " of " + this._cdCount;
            }
            return song.CDNumber.ToString();
        }
        #endregion
    }
}
