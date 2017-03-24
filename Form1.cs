/* Tiger OpenGL External Library Installer: free open-source library installer for game engine development in C++ & OpenGL
Copyright(C) 2017  Cyprian Tayrien, Interactive Games and Media, Rochester Institute of Technology
GNU General Public License <http://www.gnu.org/licenses/>./**/
// Uses 6 lines of code from http://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tiger_OpenGL_Lib_Installer
{
    public partial class Form1 : Form
    {
        //  Distributed: in a .zip: .exe, readme.txt, license.txt, 4 32-bit VS2015 libraries
        //
        //  Program Outline / Form Use
        //
        //  With checkboxes, toggle which of the 4 libraries to install
        //      GLEW, GLFW, FreeImage, glm
        //
        //  With chose path button, open select folder dialog to get install path.
        //      Display the Directory and the example on the form.
        //      (For example: C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\ )
        //      (It contains bin\ include\ and lib\.)
        //      If it does not contain those dirs, report error.
        //
        //  With install button, copy all dir contents, for checked dirs, where "from dir" does exist, and report progress (some do not, for example glm is header-only):
        //      frompath\*\bin\ -> installpath\bin\
        //      frompath\*\include\ -> installpath\include\
        //      frompath\*\lib\ -> installpath\lib\
        //
        //  Remind the user to set some or all of the following project properties (apply to All Platforms - both Debug and Release):
        //      Add Linker Input Additional Dependencies: opengl32.lib, glew32.lib, glfw3.lib, FreeImage.lib.
        //      Use Visual Studio 2015. (It may be possible to allow user to compile a new version of the installer and libraries given my and their source?)
        //      Use 32-bit (x86) and not 64-bit (x64) mode.
        //
        //      #include headers: GL/glew.h, GLFW/glfw3.h, FreeImage.h, glm/glm.hpp.
        //      Include GLEW before GLFW.

        string installpath;

        string frompath = Directory.GetCurrentDirectory() + "\\OpenGL External Libraries\\";

        string FreeImage = "FreeImage\\";
        bool FreeImageb = true;

        string GLEW = "GLEW\\";
        bool GLEWb = true;

        string GLFW = "GLFW\\";
        bool GLFWb = true;

        string glm = "glm\\";
        bool glmb = true;

        string bin = "bin\\";
        string include = "include\\";
        string lib = "lib\\";

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Select Install Directory            
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            installpath = fbd.SelectedPath + "\\";
            textBox1.Text = installpath;
        }

        private void copyDirContents(string SourcePath, string DestinationPath)
        {
            // If source path doesnt exist, don't copy
            if (!Directory.Exists(SourcePath)) return;

            // Code from http://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
        }

        private void copy3dirs(string library)
        {
            copyDirContents(frompath + library + include, installpath + include);
            copyDirContents(frompath + library + lib, installpath + lib);
            copyDirContents(frompath + library + bin, installpath + bin);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                if (!Directory.Exists(installpath + include))
                    Directory.CreateDirectory(installpath + include);

                if (!Directory.Exists(installpath + lib))
                    Directory.CreateDirectory(installpath + lib);

                if (!Directory.Exists(installpath + bin))
                    Directory.CreateDirectory(installpath + bin);

                // Install
                if (GLEWb)
                {
                    copy3dirs(GLEW);
                }

                if (GLFWb)
                {
                    copy3dirs(GLFW);
                }

                if (FreeImageb)
                {
                    copy3dirs(FreeImage);
                }

                if (glmb)
                {
                    copy3dirs(glm);
                }

                MessageBox.Show("Libraries Installed", "Tiger Installer");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Exception Caught");
            }
            button1.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Do nothing, this shouldn't change.
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // GLEW
            GLEWb = !GLEWb;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // GLFW
            GLFWb = !GLFWb;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            // glm
            glmb = !glmb;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            // FreeImage
            FreeImageb = !FreeImageb;
        }
    }
}
