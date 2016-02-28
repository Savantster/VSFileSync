using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System.Windows;
using VSFileSync.SupportClasses;
using EnvDTE;
using System.IO;

namespace Raydude.VSFileSync
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    // This attribute registers a tool window exposed by this package.
    [ProvideToolWindow(typeof(MyToolWindow))]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string)]
    [Guid(GuidList.guidVSFileSyncPkgString)]
    public sealed class VSFileSyncPackage : Package
    {
        #region Private Variables

        private MyDB m_oDB = null;
        private bool _Initializing = true;

        private DTE _MyDTE = null;
        private SolutionEvents _SolEvents = null;

        private FileSystemWatcher fswFiles = null;


        #endregion
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public VSFileSyncPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));

            //MessageBox.Show("Constructor");

            try
            {
                m_oDB = new MyDB();
            }
            catch (Exception e)
            {
                MessageBox.Show("Initialization Failure: " + e.Message);
            }
        }

        /// <summary>
        /// This function is called when the user clicks the menu item that shows the 
        /// tool window. See the Initialize method to see how the menu item is associated to 
        /// this function using the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void ShowToolWindow(object sender, EventArgs e)
        {
            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.

            //ToolWindowPane window = this.FindToolWindow(typeof(MyToolWindow), 0, true);
            //if ((null == window) || (null == window.Frame))
            //{
            //    throw new NotSupportedException(Resources.CanNotCreateWindow);
            //}
            //IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            //Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
            ControlForm frmConfig = new ControlForm(ref m_oDB, ref _MyDTE);
            frmConfig.ShowDialog();
        }


        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            //MessageBox.Show("Initialization");

            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the tool window
                CommandID toolwndCommandID = new CommandID(GuidList.guidVSFileSyncCmdSet, (int)PkgCmdIDList.cmdidFileSyncWindow);
                MenuCommand menuToolWin = new MenuCommand(ShowToolWindow, toolwndCommandID);
                mcs.AddCommand(menuToolWin);
            }

            // Hook up our Event Handlers, so we know when things have happened in the IDE.
            _MyDTE = (EnvDTE.DTE)GetService(typeof(EnvDTE.DTE));

            if (_MyDTE != null)
            {
                // need to preserve the event objects we pull, so they don't go out of scope.. These events/handlers live through
                // the entire scope of the IDE being open. We never have to change them or get rid of them (they will go out of scope
                // when the IDE closes and dumps our Package)
                _SolEvents = ((Events)_MyDTE.Events).SolutionEvents;

                _SolEvents.Opened += new _dispSolutionEvents_OpenedEventHandler(SolutionEvents_Opened);
                _SolEvents.AfterClosing += SolutionEvents_AfterClosing; // new _dispSolutionEvents_AfterClosingEventHandler(SolutionEvents_AfterClosing);

            }
            else
            {
                MessageBox.Show("Failed to get core Events Handler; Can't monitor opening and closing of Solutions..");
            }

        }

        #endregion

        #region Private methods


        private void SolutionEvents_Opened()
        {
            // we'll need to check and make sure this isn't called early.. shouldn't be, since we didn't hook up our
            // handler until AFTER the IDE fully loaded..

            //MessageBox.Show("Opened Solution...");
            _Initializing = false;
            vValidateProjects();

        }

        // SOLUTION level events. These fire at the Solution level; that is, when adding a "new project" (or existing)
        // to a SOLUTION, these are fired. This lets us know when something happens with Projects in our solution.
        private void SolutionEvents_AfterClosing()
        {
            //vRemoveHandlers();
            //MessageBox.Show("Closed Solution..");
            _Initializing = true; // seed for next opening of a Solution; triggers rebuilding internal tracking strucutres.
        }

        // This is where we look through our Dictionary and check it against our database. If we have entries, we're tracking.. if
        // we don't, ask the user if we SHOULD track these. If they say yes, bring up our configuration window so they can add
        // the local/remote filesystems that need to be kept in sync. Once that's done, we'll need to verify things are currently
        // in sync. If they are, we're done.. if not, we'll need to find out if we should push or pull for syncing.
        //
        // We will also create our File System Watcher object here, and hook it up to the Solution directory.
        private void vValidateProjects()
        {
            string SolutionFullName = _MyDTE.Solution.FullName;
            string SolutionName = Path.GetFileName(SolutionFullName);
            string SolutionDirectory = Path.GetDirectoryName(SolutionFullName);
            string ProjectName = null;
            string LocalStoredPath = null;
            string RemoteStoredPath = null;
            bool bOverride = false;
            bool bMissingSolution = false;

            foreach (Project oItems in _MyDTE.Solution.Projects)
            {
                ProjectName = oItems.Name;

                // Now that we have the Local Solution name and Project Name, we can see if they are in our database.
                m_oDB.getTrackingState(SolutionName, ProjectName, ref LocalStoredPath, ref RemoteStoredPath, ref bOverride);

                // not currently tracked, doesn't appear it's ever been noticed. 
                if (LocalStoredPath == null)
                {
                    MessageBox.Show("Solution/Project is not currently tracked.. Use the interface window and set this" + Environment.NewLine + 
                    "Solution/Project to either track or to be manually overridden so it is not tracked." + Environment.NewLine + Environment.NewLine +
                    "Solution: " + SolutionName + Environment.NewLine + "Project: " + ProjectName);
                    bMissingSolution = true;
                    continue;
                }

                if (SolutionDirectory.ToLower() != LocalStoredPath)
                {
                    // Solution not in the same place as last we tracked data for it. Prompt user to update our working info; if they
                    // say no, mark this solution as MANUALLY not tracked by the user in our database.
                }
            }

            if (bMissingSolution)
            {
                if (MessageBox.Show("Found one or more Solution/Project pairs missing from our tracking" + Environment.NewLine +
                     "system.. would you like to open the configuration window now?", "Missing Tracking Info", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ControlForm frmTemp = new ControlForm(ref m_oDB, ref _MyDTE);
                    frmTemp.ShowDialog();
                }
            }
        }

        #endregion
    }
}
