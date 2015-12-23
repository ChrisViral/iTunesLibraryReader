using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace iTunesLibraryReader
{
    /// <summary>
    /// Auto-generated code for the form
    /// </summary>
    partial class LibraryReader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Artists");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Compilations");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Library", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.fileSearcher = new System.Windows.Forms.OpenFileDialog();
            this.browseButton = new System.Windows.Forms.Button();
            this.urlText = new System.Windows.Forms.TextBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.setButton = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.playCount = new System.Windows.Forms.NumericUpDown();
            this.songsView = new System.Windows.Forms.ListView();
            this.songCollumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.artistColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.albumColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.yearColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.genreColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trackColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.playCountColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trackIDColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.libraryTree = new System.Windows.Forms.TreeView();
            this.saveButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.fileSaver = new System.Windows.Forms.SaveFileDialog();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.loadingLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.itemsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sizeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutButton = new System.Windows.Forms.ToolStripMenuItem();
            this.exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.showCompilationsCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.showTrackIDCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.showCompInArtistsCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.statusBar.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSearcher
            // 
            this.fileSearcher.DefaultExt = "xml";
            this.fileSearcher.Filter = "XML files (*.xml)|*.xml|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.fileSearcher.Title = "Select iTunes Library file";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(12, 27);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(92, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // urlText
            // 
            this.urlText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconAlignment(this.urlText, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.errorProvider.SetIconPadding(this.urlText, 5);
            this.urlText.ImeMode = System.Windows.Forms.ImeMode.On;
            this.urlText.Location = new System.Drawing.Point(232, 29);
            this.urlText.Name = "urlText";
            this.urlText.Size = new System.Drawing.Size(772, 20);
            this.urlText.TabIndex = 2;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(110, 27);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(98, 23);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load library";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // viewPanel
            // 
            this.viewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewPanel.Controls.Add(this.setButton);
            this.viewPanel.Controls.Add(this.infoLabel);
            this.viewPanel.Controls.Add(this.playCount);
            this.viewPanel.Controls.Add(this.songsView);
            this.viewPanel.Controls.Add(this.libraryTree);
            this.viewPanel.Controls.Add(this.saveButton);
            this.viewPanel.Enabled = false;
            this.viewPanel.Location = new System.Drawing.Point(12, 56);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(992, 480);
            this.viewPanel.TabIndex = 5;
            // 
            // setButton
            // 
            this.setButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.setButton.Enabled = false;
            this.setButton.Location = new System.Drawing.Point(675, 441);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(154, 39);
            this.setButton.TabIndex = 11;
            this.setButton.Text = "Set play count";
            this.setButton.UseVisualStyleBackColor = true;
            this.setButton.Click += new System.EventHandler(this.setButton_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(199, 415);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(39, 65);
            this.infoLabel.TabIndex = 10;
            this.infoLabel.Text = "Song:\r\nArtist:\r\nAlbum:\r\nYear:\r\nGenre:";
            // 
            // playCount
            // 
            this.playCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.playCount.Enabled = false;
            this.playCount.Location = new System.Drawing.Point(675, 418);
            this.playCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.playCount.Name = "playCount";
            this.playCount.Size = new System.Drawing.Size(154, 20);
            this.playCount.TabIndex = 9;
            this.playCount.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // songsView
            // 
            this.songsView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.songsView.AllowColumnReorder = true;
            this.songsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.songsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.songCollumn,
            this.artistColumn,
            this.albumColumn,
            this.yearColumn,
            this.genreColumn,
            this.trackColumn,
            this.cdColumn,
            this.playCountColumn,
            this.timeColumn,
            this.sizeColumn,
            this.trackIDColumn});
            this.songsView.FullRowSelect = true;
            this.songsView.GridLines = true;
            this.songsView.HideSelection = false;
            this.songsView.Location = new System.Drawing.Point(202, 0);
            this.songsView.MultiSelect = false;
            this.songsView.Name = "songsView";
            this.songsView.Size = new System.Drawing.Size(790, 412);
            this.songsView.TabIndex = 0;
            this.songsView.UseCompatibleStateImageBehavior = false;
            this.songsView.View = System.Windows.Forms.View.Details;
            this.songsView.ItemActivate += new System.EventHandler(this.songsView_ItemActivate);
            // 
            // songCollumn
            // 
            this.songCollumn.Text = "Title";
            this.songCollumn.Width = 112;
            // 
            // artistColumn
            // 
            this.artistColumn.Text = "Artist";
            this.artistColumn.Width = 110;
            // 
            // albumColumn
            // 
            this.albumColumn.Text = "Album";
            this.albumColumn.Width = 107;
            // 
            // yearColumn
            // 
            this.yearColumn.Text = "Year";
            this.yearColumn.Width = 53;
            // 
            // genreColumn
            // 
            this.genreColumn.Text = "Genre";
            this.genreColumn.Width = 82;
            // 
            // trackColumn
            // 
            this.trackColumn.Text = "Track";
            this.trackColumn.Width = 61;
            // 
            // cdColumn
            // 
            this.cdColumn.Text = "Disc";
            this.cdColumn.Width = 53;
            // 
            // playCountColumn
            // 
            this.playCountColumn.Text = "Plays";
            this.playCountColumn.Width = 38;
            // 
            // timeColumn
            // 
            this.timeColumn.Text = "Time";
            this.timeColumn.Width = 54;
            // 
            // sizeColumn
            // 
            this.sizeColumn.Text = "Size";
            this.sizeColumn.Width = 61;
            // 
            // trackIDColumn
            // 
            this.trackIDColumn.Text = "Track ID";
            this.trackIDColumn.Width = 54;
            // 
            // libraryTree
            // 
            this.libraryTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.libraryTree.Location = new System.Drawing.Point(0, 0);
            this.libraryTree.Name = "libraryTree";
            treeNode1.Name = "artistsNode";
            treeNode1.Text = "Artists";
            treeNode2.Name = "compilationsNode";
            treeNode2.Text = "Compilations";
            treeNode3.Name = "libraryNode";
            treeNode3.Text = "Library";
            this.libraryTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.libraryTree.PathSeparator = "\\\\";
            this.libraryTree.Size = new System.Drawing.Size(196, 480);
            this.libraryTree.TabIndex = 8;
            this.libraryTree.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.libraryTree_BeforeCollapse);
            this.libraryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.libraryTree_BeforeExpand);
            this.libraryTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.libraryTree_NodeMouseDoubleClick);
            this.libraryTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.libraryTree_MouseDown);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(862, 418);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(130, 62);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save library";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // fileSaver
            // 
            this.fileSaver.DefaultExt = "xml";
            this.fileSaver.Filter = "XML files (*.xml)|*.xml|Text files (*.txt)|All files (*.*)";
            this.fileSaver.Title = "Select location to save library";
            // 
            // statusBar
            // 
            this.statusBar.AutoSize = false;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.loadingLabel,
            this.itemsLabel,
            this.sizeLabel,
            this.timeLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 539);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1016, 22);
            this.statusBar.TabIndex = 9;
            this.statusBar.Text = "statusBar";
            // 
            // progressBar
            // 
            this.progressBar.AutoSize = false;
            this.progressBar.Enabled = false;
            this.progressBar.Margin = new System.Windows.Forms.Padding(12, 3, 1, 3);
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(250, 16);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = false;
            this.loadingLabel.Enabled = false;
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(100, 17);
            // 
            // itemsLabel
            // 
            this.itemsLabel.AutoSize = false;
            this.itemsLabel.Margin = new System.Windows.Forms.Padding(200, 3, 0, 2);
            this.itemsLabel.Name = "itemsLabel";
            this.itemsLabel.Size = new System.Drawing.Size(120, 17);
            this.itemsLabel.Text = "0 of 0 song(s)";
            this.itemsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = false;
            this.sizeLabel.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(62, 17);
            this.sizeLabel.Text = "0 bytes";
            this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = false;
            this.timeLabel.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(76, 17);
            this.timeLabel.Text = "0:00";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuButton,
            this.optionsButton});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1016, 24);
            this.menu.TabIndex = 10;
            // 
            // menuButton
            // 
            this.menuButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutButton,
            this.exitButton});
            this.menuButton.Name = "menuButton";
            this.menuButton.Size = new System.Drawing.Size(50, 20);
            this.menuButton.Text = "Menu";
            // 
            // aboutButton
            // 
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.ShortcutKeyDisplayString = "";
            this.aboutButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.aboutButton.Size = new System.Drawing.Size(149, 22);
            this.aboutButton.Text = "About";
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Name = "exitButton";
            this.exitButton.ShortcutKeyDisplayString = "";
            this.exitButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitButton.Size = new System.Drawing.Size(149, 22);
            this.exitButton.Text = "Exit";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // optionsButton
            // 
            this.optionsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showCompilationsCheck,
            this.showTrackIDCheck,
            this.showCompInArtistsCheck});
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(61, 20);
            this.optionsButton.Text = "Options";
            // 
            // showCompilationsCheck
            // 
            this.showCompilationsCheck.Checked = true;
            this.showCompilationsCheck.CheckOnClick = true;
            this.showCompilationsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showCompilationsCheck.Name = "showCompilationsCheck";
            this.showCompilationsCheck.Size = new System.Drawing.Size(266, 22);
            this.showCompilationsCheck.Text = "Show compilations";
            this.showCompilationsCheck.CheckedChanged += new System.EventHandler(this.showCompilationsCheck_CheckedChanged);
            // 
            // showTrackIDCheck
            // 
            this.showTrackIDCheck.CheckOnClick = true;
            this.showTrackIDCheck.Name = "showTrackIDCheck";
            this.showTrackIDCheck.Size = new System.Drawing.Size(266, 22);
            this.showTrackIDCheck.Text = "Show Track ID";
            this.showTrackIDCheck.CheckedChanged += new System.EventHandler(this.showTrackIDCheck_CheckedChanged);
            // 
            // showCompInArtistsCheck
            // 
            this.showCompInArtistsCheck.CheckOnClick = true;
            this.showCompInArtistsCheck.Name = "showCompInArtistsCheck";
            this.showCompInArtistsCheck.Size = new System.Drawing.Size(266, 22);
            this.showCompInArtistsCheck.Text = "Show compilation tracks in artist tab";
            this.showCompInArtistsCheck.CheckedChanged += new System.EventHandler(this.showCompInArtistsCheck_CheckedChanged);
            // 
            // LibraryReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 561);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.urlText);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.viewPanel);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menu;
            this.Name = "LibraryReader";
            this.Text = "iTunes Library Reader";
            this.viewPanel.ResumeLayout(false);
            this.viewPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Button browseButton;
        private Button loadButton;
        public OpenFileDialog fileSearcher;
        public TextBox urlText;
        public Panel viewPanel;
        public ColumnHeader songCollumn;
        private ColumnHeader artistColumn;
        private ColumnHeader albumColumn;
        private ErrorProvider errorProvider;
        public ListView songsView;
        private Button saveButton;
        private ColumnHeader genreColumn;
        private ColumnHeader trackColumn;
        private ColumnHeader cdColumn;
        private ColumnHeader playCountColumn;
        private ColumnHeader yearColumn;
        private ColumnHeader trackIDColumn;
        private ColumnHeader sizeColumn;
        public SaveFileDialog fileSaver;
        public TreeView libraryTree;
        private StatusStrip statusBar;
        public ToolStripProgressBar progressBar;
        private ToolStripStatusLabel loadingLabel;
        private MenuStrip menu;
        private ToolStripMenuItem menuButton;
        private ToolStripMenuItem exitButton;
        private ToolStripMenuItem aboutButton;
        private ToolStripMenuItem optionsButton;
        private ToolStripMenuItem showCompilationsCheck;
        private ToolStripMenuItem showTrackIDCheck;
        private ToolStripMenuItem showCompInArtistsCheck;
        private NumericUpDown playCount;
        private Button setButton;
        private Label infoLabel;
        private ToolStripStatusLabel itemsLabel;
        private ToolStripStatusLabel sizeLabel;
        private ToolStripStatusLabel timeLabel;
        private ColumnHeader timeColumn;
    }
}

