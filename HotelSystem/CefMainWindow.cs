using System;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CefSharp;
using CefSharp.WinForms;
using FarsiLibrary.Win;
using Timer = System.Windows.Forms.Timer;
using System.Drawing;
using System.Reflection;
using CCWin;
using SharpBrowser;
using HotelSystem.Handlers;
using System.Text;

namespace HotelSystem
{
    internal partial class CefMainWindow : Skin_Mac
    {
      

        public static CefMainWindow Instance;

        public static string Branding = "QL-LOGO";
        public static string UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Safari/537.36";
        public static string HomepageURL = "https://www.baidu.com";
        public static string NewTabURL = "about:blank";

        public static string FileNotFoundURL = "sharpbrowser://storage/errors/notFound.html";
        public static string CannotConnectURL = "sharpbrowser://storage/errors/cannotConnect.html";
      
        public ICookieManager cookieManager=null;
        public bool WebSecurity = true;
        public bool CrossDomainSecurity = true;
        public bool WebGL = true;
        public string cookies = null;
        public CookieVisitor visitor;
        public CefMainWindow()
        {
            Instance = this;
            InitializeComponent();
            InitBrowser();

            SetFormTitle(null);
            cookieManager=CefSharp.Cef.GetGlobalCookieManager();
            visitor = new CookieVisitor();
            visitor.SendCookie += visitor_SendCookie;

        }

        private void visitor_SendCookie(CefSharp.Cookie obj)
        {
            cookies += obj.Domain.TrimStart('.') + "->" + obj.Name + "=" + obj.Value + ";";
        }

        #region App Icon

        /// <summary>
        /// embedding the resource using the Visual Studio designer results in a blurry icon.
        /// the best way to get a non-blurry icon for Windows 7 apps.
        /// </summary>
        private void InitAppIcon()
        {
            assembly = Assembly.GetAssembly(typeof(CefMainWindow));
            Icon = new Icon(GetResourceStream("sharpbrowser.ico"), new Size(64, 64));
        }

        public static Assembly assembly = null;
        public Stream GetResourceStream(string filename, bool withNamespace = true)
        {
            try
            {
                return assembly.GetManifestResourceStream("SharpBrowser.Resources." + filename);
            }
            catch (System.Exception ex) { }
            return null;
        }

        #endregion

        #region Tooltips & Hotkeys

        /// <summary>
        /// these hotkeys work when the user is focussed on the .NET form and its controls,
        /// AND when the user is focussed on the browser (CefSharp portion)
        /// </summary>


        /// <summary>
        /// we activate all the tooltips stored in the Tag property of the buttons
        /// </summary>
        public void InitTooltips(System.Windows.Forms.Control.ControlCollection parent)
        { 
            foreach (Control ui in parent)
            {
                Button btn = ui as Button;
                if (btn != null)
                {
                    if (btn.Tag != null)
                    {
                        ToolTip tip = new ToolTip();
                        tip.ReshowDelay = tip.InitialDelay = 200;
                        tip.ShowAlways = true;
                        tip.SetToolTip(btn, btn.Tag.ToString());
                    }
                }
                Panel panel = ui as Panel;
                if (panel != null)
                {
                    InitTooltips(panel.Controls);
                }
            }
        }

        #endregion

        #region Web Browser & Tabs

        private FATabStripItem newStrip;
        private FATabStripItem downloadsStrip;

        private string currentFullURL;
        private string currentCleanURL;
        private string currentTitle;

        public HostHandler host;
        private DownloadHandler dHandler;
        private ContextMenuHandler mHandler;
        private LifeSpanHandler lHandler;
        private KeyboardHandler kHandler;
        private RequestHandler rHandler;
 
        /// <summary>
        /// this is done just once, to globally initialize CefSharp/CEF
        /// </summary>
        private void InitBrowser()
        {

            CefSettings settings = new CefSettings();
            settings.Locale = "zh_CN";
            settings.CachePath = System.IO.Directory.GetCurrentDirectory() + @"/BrowserCache";
            settings.AcceptLanguageList = "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3";
            settings.LocalesDirPath = System.IO.Directory.GetCurrentDirectory() + @"/localeDir";
            settings.LogFile = System.IO.Directory.GetCurrentDirectory() + @"/LogData";
            settings.PersistSessionCookies = true;
            settings.UserDataPath = System.IO.Directory.GetCurrentDirectory() + @"/UserData";
            settings.UserAgent = UserAgent;

            settings.IgnoreCertificateErrors = true;

           

            Cef.Initialize(settings);

            dHandler = new DownloadHandler(this);
            lHandler = new LifeSpanHandler(this);
            mHandler = new ContextMenuHandler(this);
            kHandler = new KeyboardHandler(this);
            rHandler = new RequestHandler(this);

           

            host = new HostHandler(this);

            AddNewBrowser(CefFaTabStripItem, HomepageURL);

        }

        /// <summary>
        /// this is done every time a new tab is openede
        /// </summary>
        private void ConfigureBrowser(ChromiumWebBrowser browser)
        {

            BrowserSettings config = new BrowserSettings();

            config.FileAccessFromFileUrls = (!CrossDomainSecurity).ToCefState();
            config.UniversalAccessFromFileUrls = (!CrossDomainSecurity).ToCefState();
            config.WebSecurity = WebSecurity.ToCefState();
            config.WebGl = WebGL.ToCefState();

            browser.BrowserSettings = config;

        }


       

        private void LoadURL(string url)
        {
            Uri outUri;
            string newUrl = url;
            string urlLower = url.Trim().ToLower();

            // UI
            SetTabTitle(CurBrowser, "Loading...");

            // load page
            if (urlLower == "localhost")
            {

                newUrl = "about:blank";

            }
            else if (url.CheckIfFilePath() || url.CheckIfFilePath2())
            {

                newUrl = url.PathToURL();

            }
            else
            {

                Uri.TryCreate(url, UriKind.Absolute, out outUri);

                if (!(urlLower.StartsWith("http") || urlLower.StartsWith("sharpbrowser")))
                {
                    if (outUri == null || outUri.Scheme != Uri.UriSchemeFile) newUrl = "http://" + url;
                }

              

            }


            // load URL
            CurBrowser.Load(newUrl);

            // set URL in UI
            SetFormURL(newUrl);


        } 

        private void SetFormTitle(string tabName)
        {

            if (tabName.CheckIfValid())
            {

                this.Text = tabName + " - " + Branding;
                currentTitle = tabName;

            }
            else
            {

                this.Text = Branding;
                currentTitle = "New Tab";
            }

        }

        private void SetFormURL(string URL)
        {

            currentFullURL = URL;
            currentCleanURL = CleanURL(URL);
            CurTab.CurURL = currentFullURL;



        }

        private string CleanURL(string url)
        {
            if (url.BeginsWith("about:"))
            {
                return "";
            }
            url = url.RemovePrefix("http://");
            url = url.RemovePrefix("https://");
            url = url.RemovePrefix("file://");
            url = url.RemovePrefix("/");
            return url.DecodeURL();
        }
        private bool IsBlank(string url)
        {
            return (url == "" || url == "about:blank");
        }
        private bool IsBlankOrSystem(string url)
        {
            return (url == "" || url.BeginsWith("about:") || url.BeginsWith("chrome:") || url.BeginsWith("sharpbrowser:"));
        }

        public void AddBlankWindow()
        {

            // open a new instance of the browser

            ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath, "");
            //info.WorkingDirectory = workingDir ?? exePath.GetPathDir(true);
            info.LoadUserProfile = true;

            info.UseShellExecute = false;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardInput = true;

            Process.Start(info);
        }
        public void AddBlankTab()
        {
            AddNewBrowserTab("");

        }

        public ChromiumWebBrowser AddNewBrowserTab(string url, bool focusNewTab = true, string refererUrl = null)
        {
            return (ChromiumWebBrowser)this.Invoke((Func<ChromiumWebBrowser>)delegate {

                // check if already exists
                foreach (FATabStripItem tab in CefFATabStrip.Items)
                {
                    SharpTab tab2 = (SharpTab)tab.Tag;
                    if (tab2 != null && (tab2.CurURL == url))
                    {
                        CefFATabStrip.SelectedItem = tab;
                        return tab2.Browser;
                    }
                }

                FATabStripItem tabStrip = new FATabStripItem();
                tabStrip.Title = "New Tab";
                CefFATabStrip.Items.Insert(CefFATabStrip.Items.Count - 1, tabStrip);
                newStrip = tabStrip;

                SharpTab newTab = AddNewBrowser(newStrip, url);
                newTab.RefererURL = refererUrl;
                if (focusNewTab) timer1.Enabled = true;
                return newTab.Browser;
            });
        }
        private SharpTab AddNewBrowser(FATabStripItem tabStrip, String url)
        {
            if (url == "") url = NewTabURL;
            ChromiumWebBrowser browser = new ChromiumWebBrowser(url);

            // set config
            ConfigureBrowser(browser);

            // set layout
            browser.Dock = DockStyle.Fill;
            tabStrip.Controls.Add(browser);
            browser.BringToFront();

            // add events
            browser.StatusMessage += Browser_StatusMessage;
            browser.LoadingStateChanged += Browser_LoadingStateChanged;
            browser.TitleChanged += Browser_TitleChanged;
            browser.LoadError += Browser_LoadError;

            browser.DownloadHandler = dHandler;
            browser.MenuHandler = mHandler;
            browser.LifeSpanHandler = lHandler;
            browser.KeyboardHandler = kHandler;
            browser.RequestHandler = rHandler;

            // new tab obj
            SharpTab tab = new SharpTab
            {
                IsOpen = true,
                Browser = browser,
                Tab = tabStrip,
                OrigURL = url,
                CurURL = url,
                Title = "New Tab",
                DateCreated = DateTime.Now
            };

            // save tab obj in tabstrip
            tabStrip.Tag = tab;

            if (url.StartsWith("sharpbrowser:"))
            {
                browser.RegisterAsyncJsObject("host", host, true);
            }
            return tab;
        }

        public SharpTab GetTabByBrowser(IWebBrowser browser)
        {
            foreach (FATabStripItem tab2 in CefFATabStrip.Items)
            {
                SharpTab tab = (SharpTab)(tab2.Tag);
                if (tab != null && tab.Browser == browser)
                {
                    return tab;
                }
            }
            return null;
        }

        public void RefreshActiveTab()
        {
            CurBrowser.Load(CurBrowser.Address);
        }

        public void CloseActiveTab()
        {
            if (CurTab != null/* && TabPages.Items.Count > 2*/)
            {

                // remove tab and save its index
                int index = CefFATabStrip.Items.IndexOf(CefFATabStrip.SelectedItem);
                CefFATabStrip.RemoveTab(CefFATabStrip.SelectedItem);

                // keep tab at same index focussed
                if ((CefFATabStrip.Items.Count - 1) > index)
                {
                    CefFATabStrip.SelectedItem = CefFATabStrip.Items[index];
                }
            }
        }

      


        public ChromiumWebBrowser CurBrowser
        {
            get
            {
                if (CefFATabStrip.SelectedItem != null && CefFATabStrip.SelectedItem.Tag != null)
                {
                    return ((SharpTab)CefFATabStrip.SelectedItem.Tag).Browser;
                }
                else
                {
                    return null;
                }
            }
        }

        public SharpTab CurTab
        {
            get
            {
                if (CefFATabStrip.SelectedItem != null && CefFATabStrip.SelectedItem.Tag != null)
                {
                    return ((SharpTab)CefFATabStrip.SelectedItem.Tag);
                }
                else
                {
                    return null;
                }
            }
        }



        private void Browser_LoadError(object sender, LoadErrorEventArgs e)
        {
            // ("Load Error:" + e.ErrorCode + ";" + e.ErrorText);
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            InvokeIfNeeded(() => {

                ChromiumWebBrowser browser = (ChromiumWebBrowser)sender;

                SetTabTitle(browser, e.Title);

            });
        }

        private void SetTabTitle(ChromiumWebBrowser browser, string text)
        {

            text = text.Trim();
            if (IsBlank(text))
            {
                text = "New Tab";
            }

            // save text
            browser.Tag = text;

            // get tab of given browser
            FATabStripItem tabStrip = (FATabStripItem)browser.Parent;
            if (tabStrip != null)
            {
                tabStrip.Title = text;
            }

            // if current tab
            if (browser == CurBrowser)
            {

                SetFormTitle(text);

            }
        }

        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (sender == CurBrowser)
            {



                if (e.IsLoading)
                {

                    // set title
                    //SetTabTitle();

                }
            }
        }

        public void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private void Browser_StatusMessage(object sender, StatusMessageEventArgs e)
        {
        }

        public void WaitForBrowserToInitialize(ChromiumWebBrowser browser)
        {
            while (!browser.IsBrowserInitialized)
            {
                Thread.Sleep(100);
            }
        }




        private void OnTabsChanged(TabStripItemChangedEventArgs e)
        {


            ChromiumWebBrowser browser = null;
            try
            {
                browser = ((ChromiumWebBrowser)e.Item.Controls[0]);
            }
            catch (System.Exception ex) { }


            if (e.ChangeType == FATabStripItemChangeTypes.SelectionChanged)
            {
                if (CefFATabStrip.SelectedItem == tabStripAdd)
                {
                    AddBlankTab();
                }
                else
                {

                    browser = CurBrowser;

                    SetFormURL(browser.Address);
                    SetFormTitle(browser.Tag.ConvertToString() ?? "New Tab");




                }
            }

            if (e.ChangeType == FATabStripItemChangeTypes.Removed)
            {
                if (e.Item == downloadsStrip) downloadsStrip = null;
                if (browser != null)
                {
                  
                       if (CefFATabStrip.Items.Count>2)
                    {
                        browser.Dispose();
                    }
                    
                }
            }

            if (e.ChangeType == FATabStripItemChangeTypes.Changed)
            {
                if (browser != null)
                {
                    if (currentFullURL != "about:blank")
                    {
                        browser.Focus();
                    }
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CefFATabStrip.SelectedItem = newStrip;
            timer1.Enabled = false;
        }
         
        private void menuCloseTab_Click(object sender, EventArgs e)
        {
            CloseActiveTab();
        }

        private void menuCloseOtherTabs_Click(object sender, EventArgs e)
        {
            List<FATabStripItem> listToClose = new List<FATabStripItem>();
            foreach (FATabStripItem tab in CefFATabStrip.Items)
            {
                if (tab != tabStripAdd && tab != CefFATabStrip.SelectedItem) listToClose.Add(tab);
            }
            foreach (FATabStripItem tab in listToClose)
            {
                CefFATabStrip.RemoveTab(tab);
            }

        }

        public List<int> CancelRequests
        {
            get
            {
                return downloadCancelRequests;
            }
        }


        private void TxtURL_KeyDown(object sender, KeyEventArgs e)
        {

            // if ENTER or CTRL+ENTER pressed
            if (e.IsHotkey(Keys.Enter) || e.IsHotkey(Keys.Enter, true))
            {


                LoadURL(TxtURL.Text);

                // im handling this
                e.Handled = true;
                e.SuppressKeyPress = true;

                // defocus from url textbox
                this.Focus();
            }

            // if full URL copied
            if (e.IsHotkey(Keys.C, true) && Utils.IsFullySelected(TxtURL))
            {

                // copy the real URL, not the pretty one
                Clipboard.SetText(CurBrowser.Address, TextDataFormat.UnicodeText);

                // im handling this
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtUrl_Click(object sender, EventArgs e)
        {
            if (!Utils.HasSelection(TxtURL))
            {
                TxtURL.SelectAll();
            }
        }

        private void OpenDeveloperTools()
        {
            CurBrowser.ShowDevTools();
        }

        private void tabPages_MouseClick(object sender, MouseEventArgs e)
        {
            /*if (e.Button == System.Windows.Forms.MouseButtons.Right) {
				tabPages.GetTabItemByPoint(this.mouse
			}*/
        }

        #endregion

        #region Download Queue

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            // ask user if they are sure
            if (DownloadsInProgress())
            {
                if (MessageBox.Show("Downloads are in progress. Cancel those and exit?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                { 
                    e.Cancel = true;
                    return;
                }
            }

            // dispose all browsers
            try
            {
                foreach (TabPage tab in CefFATabStrip.Items)
                {
                    ChromiumWebBrowser browser = (ChromiumWebBrowser)tab.Controls[0];
                    browser.Dispose();
                }
            }
            catch (System.Exception ex) { }

        }

        public Dictionary<int, DownloadItem> downloads;
        public Dictionary<int, string> downloadNames;
        public List<int> downloadCancelRequests;

        /// <summary>
        /// we must store download metadata in a list, since CefSharp does not
        /// </summary>
        private void InitDownloads()
        {

            downloads = new Dictionary<int, DownloadItem>();
            downloadNames = new Dictionary<int, string>();
            downloadCancelRequests = new List<int>();

        }

        public Dictionary<int, DownloadItem> Downloads
        {
            get
            {
                return downloads;
            }
        }

        public void UpdateDownloadItem(DownloadItem item)
        {
            lock (downloads)
            {

                // SuggestedFileName comes full only in the first attempt so keep it somewhere
                if (item.SuggestedFileName != "")
                {
                    downloadNames[item.Id] = item.SuggestedFileName;
                }

                // Set it back if it is empty
                if (item.SuggestedFileName == "" && downloadNames.ContainsKey(item.Id))
                {
                    item.SuggestedFileName = downloadNames[item.Id];
                }

                downloads[item.Id] = item;

                //UpdateSnipProgress();
            }
        }

        public string CalcDownloadPath(DownloadItem item)
        {

            string itemName = item.SuggestedFileName != null ? item.SuggestedFileName.GetAfterLast(".") + " file" : "downloads";

            string path = null;
            if (path != null)
            {
                return path;
            }

            return null;
        }

        public bool DownloadsInProgress()
        {
            foreach (DownloadItem item in downloads.Values)
            {
                if (item.IsInProgress)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// open a new tab with the downloads URL
        /// </summary>


     



        #endregion

        #region Search Bar



        #endregion

             

        private void xcButton_Click(object sender, EventArgs e)
        {
           
            LoadURL("https://www.ctrip.com");

          
        }

        private void cookie_Click(object sender, EventArgs e)
        {

            //cookieManager.VisitUrlCookies("https://www.ctrip.com",true,visitor);
            //MessageBox.Show(cookies);

            var visitor = new CookieMonster(all_cookies => {
                var sb = new StringBuilder();
                foreach (var nameValue in all_cookies)
                    sb.AppendLine(nameValue.Item1 + " = " + nameValue.Item2);
                BeginInvoke(new MethodInvoker(() => {
                    MessageBox.Show(sb.ToString());
                }));
            });
            cookieManager.VisitUrlCookies("https://www.ctrip.com",true,visitor);

        }
    }
}

/// <summary>
/// POCO created for holding data per tab
/// </summary>
internal class SharpTab
{

    public bool IsOpen;

    public string OrigURL;
    public string CurURL;
    public string Title;

    public string RefererURL;

    public DateTime DateCreated;

    public FATabStripItem Tab;
    public ChromiumWebBrowser Browser;

}

/// <summary>
/// POCO for holding hotkey data
/// </summary>
internal class SharpHotKey
{

    public Keys Key;
    public int KeyCode;
    public bool Ctrl;
    public bool Shift;
    public bool Alt;

    public Action Callback;

    public SharpHotKey(Action callback, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
    {
        Callback = callback;
        Key = key;
        KeyCode = (int)key;
        Ctrl = ctrl;
        Shift = shift;
        Alt = alt;
    }

}