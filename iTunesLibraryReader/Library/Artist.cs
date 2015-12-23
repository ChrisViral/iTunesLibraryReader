using System;
using System.Collections.Generic;
using System.Linq;

namespace iTunesLibraryReader.Library
{
    /// <summary>
    /// Represents an artis, all it's albums and songs.
    /// NOTE: I need to make all these classes inherit from ICollection, that'd make more sense.
    /// </summary>
    public class Artist : IComparable<Artist>
    {
        #region Various artists
        private static readonly Artist _various = null;
        /// <summary>
        /// Represents the "Various Artists", used for compilations
        /// </summary>
        public static Artist various
        {
            get { return _various; }
        }
        #endregion

        #region Properties
        private readonly string _name = "artist";
        /// <summary>
        /// Name of the artist
        /// </summary>
        public string name
        {
            get { return this._name; }
        }

        private readonly List<Album> _albums = new List<Album>();
        /// <summary>
        /// Ordered list of the albums of this artist
        /// </summary>
        public List<Album> albums
        {
            get { return this._albums; }
        }

        private readonly List<Song> _songs = new List<Song>();
        /// <summary>
        /// All songs by this artist
        /// </summary>
        public List<Song> songs
        {
            get { return this._songs; }
        }

        private int _albumCount = 0;
        /// <summary>
        /// Amount of albums by this artist
        /// </summary>
        public int albumCount
        {
            get { return this._albumCount; }
        }

        private int _songCount = 0;
        /// <summary>
        /// Amount of songs by this artist
        /// </summary>
        public int songCount
        {
            get { return this._songCount; }
        }

        private TimeSpan _totalTime = TimeSpan.Zero;
        /// <summary>
        /// Total time of the songs by this artist
        /// </summary>
        public TimeSpan totalTime
        {
            get { return this._totalTime; }
        }

        private FileSize _totalSize = FileSize.zero;
        /// <summary>
        /// Total file size of the songs by this artist
        /// </summary>
        public FileSize totalSize
        {
            get { return this._totalSize; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new artist of the given name
        /// </summary>
        /// <param name="name"></param>
        public Artist(string name)
        {
            this._name = name;
        }

        /// <summary>
        /// Initializes the Various Artists
        /// </summary>
        static Artist()
        {
            _various = new Artist("Various artists");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tries to get an album by title. If fails, creates an album of this title and returns it
        /// </summary>
        /// <param name="title">Title of the album to get</param>
        /// <param name="isComp">If the album is a compilation</param>
        /// <returns>Album of the given title</returns>
        public Album GetAddAlbum(string title, bool isComp)
        {
            foreach(Album album in this._albums)
            {
                if (album.title == title) { return album; }
            }
            return AddAlbum(title, isComp);
        }

        /// <summary>
        /// Adds an album of the given title and returns it
        /// </summary>
        /// <param name="title">Title of the album to add</param>
        /// <param name="isComp">If the album is a compilation</param>
        /// <returns>The new album of the given title</returns>
        public Album AddAlbum(string title, bool isComp)
        {
            Album a;
            if (isComp) { a = new Album(title, _various); }
            else { a = new Album(title, this); }
            this.albums.Add(a);
            return a;
        }

        /// <summary>
        /// Gets and returns an album by title
        /// </summary>
        /// <param name="title">Title of the album to find</param>
        /// <returns>Album of the given title</returns>
        public Album GetAlbum(string title)
        {
            return this._albums.Find(a => a.title == title);
        }

        /// <summary>
        /// Updates all the albums and songs of this artist and sorts them correctly
        /// </summary>
        public void Update()
        {
            foreach (Album album in this._albums)
            {
                album.Update();
                this._totalTime.Add(album.time);
                this._totalSize.Add(album.size);
            }

            this._albums.Sort();
            this._albums.ForEach(a => this._songs.AddRange(a.songs));
            this._albumCount = this._albums.Count(a => a.artist != _various);
            this._songCount = this._songs.Count;
        }

        /// <summary>
        /// Compares this artist to another for sort
        /// </summary>
        /// <param name="other">Other artist to compare this instance to</param>
        /// <returns>1 if this artist comes after the other, -1 if it comes before, 0 if both are equal</returns>
        public int CompareTo(Artist other)
        {
            return this._name.CompareTo(other._name);
        }
        #endregion
    }
}
