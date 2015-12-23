using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using iTunesLibraryReader.Library;

namespace iTunesLibraryReader
{
    /// <summary>
    /// The main form of the LibraryReader
    /// </summary>
    public partial class LibraryReader : Form
    {
        #region Current
        private static LibraryReader _current = null;
        /// <summary>
        /// Gets the current active LibraryReader (Singleton-ish of the Form)
        /// </summary>
        public static LibraryReader current
        {
            get { return _current; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new LibraryReader Form
        /// </summary>
        public LibraryReader()
        {
            if (_current == null)
            {
                InitializeComponent();
                //Sets instance
                _current = this;
                //Gets base values
                this.library = this.libraryTree.Nodes["libraryNode"];
                this.selected = this.library;
                this.artists = this.library.Nodes["artistsNode"];
                this.compilations = this.library.Nodes["compilationsNode"];
                this.songsView.Columns.Remove(this.trackIDColumn);
            }
            //If another instance exists, idk whatever happened but this shouldn't be happening
            else { Dispose(true); }
        }
        #endregion

        #region Fields
        //Various helper fields
        public XmlDocument document = null;
        private TreeNode library = null, artists = null, compilations = null, selected = null;
        private List<Song> displayed = new List<Song>();
        private FileSize size = FileSize.zero;
        private TimeSpan time = TimeSpan.Zero;
        //Address separator in the tree, probably the only chain that would not be in a song's/artist's/album's name
        private static readonly string[] separators = { @"\\" };
        private bool cancel = false;
        private Song editedSong = null;
        private ListViewItem editedItem = null;
        #endregion

        #region Methods
        /// <summary>
        /// Updates the TreeView
        /// </summary>
        private void SetTree()
        {
            this.libraryTree.BeginUpdate();
            SetArtistsTree();
            SetCompilationsTree();
            this.libraryTree.EndUpdate();
        }

        /// <summary>
        /// Sets the artists in the tree
        /// </summary>
        private void SetArtistsTree()
        {
            this.artists.Nodes.Clear();
            foreach (Artist artist in MusicLib.current.artists)
            {
                TreeNode ar = new TreeNode(artist.name);
                ar.Name = artist.name;
                if (this.showCompInArtistsCheck.Checked)
                {
                    foreach (Album album in artist.albums)
                    {
                        TreeNode al = ar.Nodes.Add(album.title, album.title);
                        foreach (Song song in album.songs)
                        {
                            al.Nodes.Add(song.trackID.ToString(), song.title);
                        }
                    }
                }
                else
                {
                    foreach (Album album in artist.albums)
                    {
                        if (album.artist != Artist.various)
                        {
                            TreeNode al = ar.Nodes.Add(album.title, album.title);
                            foreach (Song song in album.songs)
                            {
                                al.Nodes.Add(song.trackID.ToString(), song.title);
                            }
                        }
                    }
                }
                if (ar.Nodes.Count > 0) { this.artists.Nodes.Add(ar); }
            }
        }

        /// <summary>
        /// Set the Compilations part of the tree
        /// </summary>
        private void SetCompilationsTree()
        {
            this.compilations.Nodes.Clear();
            foreach (Album album in Artist.various.albums)
            {
                TreeNode al = this.compilations.Nodes.Add(album.title, album.title);
                foreach (Song song in album.songs)
                {
                    al.Nodes.Add(song.trackID.ToString(), song.title);
                }
            }
        }

        /// <summary>
        /// Sets song grid view
        /// </summary>
        private void SetSongsView()
        {
            MusicLib lib = MusicLib.current;
            this.size = FileSize.zero;
            this.time = TimeSpan.Zero;
            ClearEdited();
            this.displayed = new List<Song>();
            this.songsView.BeginUpdate();
            this.songsView.Items.Clear();
            if (this.selected == this.library)
            {
                if (this.showCompilationsCheck.Checked)
                {
                    foreach(Album album in lib.albums)
                    {
                        AddSongsToView(album.songs);
                    }
                }
                else
                {
                    foreach (Album album in lib.albums)
                    {
                        if (album.artist != Artist.various) { AddSongsToView(album.songs); }
                    }
                }
            }
            else if (this.selected == this.artists)
            {
                if (this.showCompInArtistsCheck.Checked)
                {
                    foreach (Artist artist in lib.artists)
                    {
                        AddSongsToView(artist.songs);
                    }
                }
                else
                {
                    foreach (Artist artist in lib.artists)
                    {
                        foreach (Album album in artist.albums)
                        {
                            if (album.artist != Artist.various) { AddSongsToView(album.songs); }
                        }
                    }
                }
            }
            else if (this.selected == compilations)
            {
                AddSongsToView(Artist.various.songs);
            }
            else if (this.selected != null)
            {
                string[] paths = this.selected.FullPath.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string album = string.Empty, song = string.Empty, branch = paths[1];
                Artist artist = null;
                if (branch == "Artists")
                {
                    switch (paths.Length)
                    {
                        case 5:
                            song = paths[4]; goto case 4;

                        case 4:
                            album = paths[3]; goto case 3;

                        case 3:
                            artist = lib.GetArtist(paths[2]); break;
                    }
                    if (string.IsNullOrEmpty(album))
                    {
                        if (showCompInArtistsCheck.Checked)
                        {
                            AddSongsToView(artist.songs);
                        }
                        else
                        {
                            foreach(Album al in artist.albums)
                            {
                                if (al.artist != Artist.various) { AddSongsToView(al.songs); }
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(song))
                    {
                        AddSongsToView(artist.GetAlbum(album).songs);
                    }
                    else
                    {
                        AddSongsToView(new List<Song> { artist.GetAlbum(album).GetSong(song) });
                    }
                }
                else
                {
                    artist = Artist.various;
                    switch (paths.Length)
                    {
                        case 4:
                            song = paths[3]; goto case 3;

                        case 3:
                            album = paths[2]; break;
                    }

                    if (string.IsNullOrEmpty(song))
                    {
                        AddSongsToView(artist.GetAlbum(album).songs);
                    }
                    else
                    {
                        AddSongsToView(new List<Song> { artist.GetAlbum(album).GetSong(song) });
                    }
                }
            }
            this.itemsLabel.Text = this.displayed.Count + " of " + MusicLib.current.songCount + " song(s)";
            this.timeLabel.Text = this.time.AutoString();
            this.sizeLabel.Text = this.size.ToString();
            this.songsView.EndUpdate();


        }

        /// <summary>
        /// Adds the given songs to the grid view
        /// </summary>
        /// <param name="songs">Songs to add</param>
        private void AddSongsToView(List<Song> songs)
        {
            foreach (Song song in songs)
            {
                Album album = song.album;
                string[] subItems = new string[]
                {
                    song.title,
                    song.artist.name,
                    album.title,
                    song.year.ToString(),
                    song.genre,
                    album.GetTrackString(song),
                    album.GetCDString(song),
                    song.playCount.ToString(),
                    song.time.AutoString(),
                    song.size.ToString(),
                    song.trackID.ToString()
                };

                this.songsView.Items.Add(new ListViewItem(subItems));
                this.size = this.size.Add(song.size);
                this.time = this.time.Add(song.time);
            }
            this.displayed.AddRange(songs);
        }

        /// <summary>
        /// Sets the progress bar to marquee style
        /// </summary>
        private void StartMarqueeBar()
        {
            this.progressBar.Enabled = true;
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.loadingLabel.Enabled = true;
            this.loadingLabel.Text = "Working...";
        }

        /// <summary>
        /// Sets the progress bar to continuous style
        /// </summary>
        private void EndMarqueeBar()
        {
            this.progressBar.Enabled = false;
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.loadingLabel.Enabled = false;
            this.loadingLabel.Text = string.Empty; ;
        }

        /// <summary>
        /// Clears the currently edited song
        /// </summary>
        private void ClearEdited()
        {
            this.editedSong = null;
            this.editedItem = null;this.infoLabel.Text = "Song:\nArtist:\nAlbum:\nYear:\nGenre:";
            this.songsView.SelectedItems.Clear();
            this.playCount.Value = 0;
            this.playCount.Enabled = false;
            this.setButton.Enabled = false;
        }
        #endregion

        #region Events
        //Form events, won't comment them all, most are self explaining
        private void browseButton_Click(object sender, EventArgs e)
        {
            if (this.fileSearcher.ShowDialog() == DialogResult.OK)
            {
                this.urlText.Text = this.fileSearcher.FileName;
                this.loadButton.Enabled = true;
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(urlText.Text))
            {
                this.errorProvider.SetError(this.urlText, "File does not exist.");
                return;
            }
            else { this.errorProvider.SetError(this.urlText, string.Empty); }

            MusicLib.Clear();
            this.loadingLabel.Enabled = true;
            this.loadingLabel.Text = "Loading...";
            this.progressBar.Enabled = true;
            this.Cursor = Cursors.WaitCursor;
            this.document = new XmlDocument();
            try
            {
                document.Load(urlText.Text);
                XmlNodeList nodes = document.ChildNodes[2].ChildNodes[0].ChildNodes[15].ChildNodes;
                this.progressBar.Maximum = nodes.Count / 2;
                if (MusicLib.SetupNewLibrary(nodes))
                {
                    this.viewPanel.Enabled = true;
                }
                else
                {
                    this.document = null;
                    MusicLib.Clear();
                }
            }
            catch (Exception ex)
            {
                this.document = null;
                MusicLib.Clear();
                MessageBox.Show(this, String.Format("An error happened when trying to read the iTunes library.\nThe operation has been aborted.\nError: {0}\nStack Trace:\n{1}", ex.Message, ex.StackTrace), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            this.progressBar.Value = 0;
            this.Cursor = Cursors.Default;
            this.fileSaver.FileName = urlText.Text;
            this.fileSaver.InitialDirectory = fileSearcher.InitialDirectory;

            this.progressBar.Style = ProgressBarStyle.Marquee;
            SetTree();
            this.libraryTree.SelectedNode = this.library;
            this.selected = this.library;
            SetSongsView();
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.progressBar.Enabled = false;
            this.loadingLabel.Text = string.Empty;
            this.loadingLabel.Enabled = false;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Close iTunes Library Reader?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes) { Close(); }
        }

        /// <summary>
        /// Brief barebones about window using the popup dialog system
        /// </summary>
        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "iTunes Library Reader\nChristophe Savard (stupid_chris) © 2015\nchristophe_savard@hotmail.ca\nHelper app to visualize and modify an iTunes library and modify play counts easily", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (this.fileSaver.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.document.Save(this.fileSaver.FileName);
                    MessageBox.Show(this, "iTunes Library successfully saved to:\n" + this.fileSaver.FileName, "Success!", MessageBoxButtons.OK);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(this, String.Format("An error happened when trying to save the iTunes library.\nThe operation has been aborted.\nError: {0}\nStack Trace:\n{1}", ex.Message, ex.StackTrace), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void setButton_Click(object sender, EventArgs e)
        {
            this.editedSong.playCount = (ushort)this.playCount.Value;
            this.editedSong.playCountNode.Value = this.editedSong.playCount.ToString();
            this.editedItem.SubItems[7].Text = this.editedSong.playCountNode.Value;
        }

        private void showCompilationsCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.libraryTree.BeginUpdate();
            if (this.showCompilationsCheck.Checked)
            {
                this.library.Nodes.Add(this.compilations);
            }
            else
            {
                this.library.Nodes.Remove(this.compilations);
            }
            this.libraryTree.EndUpdate();
        }

        /// <summary>
        /// Cancels collapse on double click
        /// </summary>
        private void libraryTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (this.cancel)
            {
                e.Cancel = true;
                this.cancel = false;
            }
        }

        /// <summary>
        /// Cancels expand on double click
        /// </summary>
        private void libraryTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (this.cancel)
            {
                e.Cancel = true;
                this.cancel = false;
            }
        }

        /// <summary>
        /// Sets double click flag
        /// </summary>
        private void libraryTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks > 1)
            {
                this.cancel = true;
            }
            else
            {
                this.cancel = false;
            }
        }

        private void songsView_ItemActivate(object sender, EventArgs e)
        {
            this.editedItem = this.songsView.SelectedItems[0];
            int trackID;
            if (int.TryParse(this.editedItem.SubItems[10].Text, out trackID))
            {
                this.editedSong = MusicLib.current.GetSong(trackID);
                this.infoLabel.Text = String.Format("Song:   {0}\nArtist:   {1}\nAlbum: {2}\nYear:    {3}\nGenre:  {4}", editedSong.title, editedSong.artist.name, editedSong.album.title, editedSong.year, editedSong.genre);
                this.playCount.Enabled = true;
                this.playCount.Value = editedSong.playCount;
                this.setButton.Enabled = true;
            }
        }

        private void showTrackIDCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.songsView.BeginUpdate();
            if (this.showTrackIDCheck.Checked)
            {
                this.songsView.Columns.Add(this.trackIDColumn);
            }
            else
            {
                this.songsView.Columns.Remove(this.trackIDColumn);
            }
            this.songsView.EndUpdate();
        }

        private void showCompInArtistsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (MusicLib.current != null)
            {
                StartMarqueeBar();
                this.libraryTree.BeginUpdate();
                SetArtistsTree();
                this.libraryTree.EndUpdate();
                this.selected = this.libraryTree.SelectedNode;
                SetSongsView();
                EndMarqueeBar();
            }
        }

        /// <summary>
        /// Chances the songs grid view when double clicking a node
        /// </summary>
        private void libraryTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && this.selected != e.Node)
            {
                this.selected = e.Node;
                SetSongsView();
            }
        }
        #endregion
    }
}
