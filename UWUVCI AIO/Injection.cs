using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWUVCI_AIO
{
    class Injection
    {
        public enum Console { NDS, N64, GBA, NES, SNES }


        /*
         * Console: Can either be NDS, N64, GBA, NES or SNES
         * BaseRom = Name of the BaseRom, which is the folder name too (example: Super Metroid EU will be saved at the BaseRom path under the folder SMetroidEU, so the BaseRom is in this case SMetroidEU). 
         * CSTMN_Base_path = Path to the custom Base. Is null if no custom base is used.
         * INJCT_Rom_path = Path to the Rom to be injected into the Base Game
         * ini_path = Only used for N64. Path to the INI configuration. If null it will use the blank ini.
         * bootimages = String array containing the paths for
         *              bootTvTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 1280x720, and if its a TGA it needs a bit depth of 24. If null, the originial BootImage will be used.
         *              bootDrcTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 854x480, and if its a TGA it needs a bit depth of 24. If null, the originial BootImage will be used.
         *              iconTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 128x128, and if its a TGA it needs a bit depth of 32. If null, the originial BootImage will be used.
         *              bootDrcTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 170x42, and if its a TGA it needs a bit depth of 32. If null, the originial BootImage will be used.
         */

        public static void inject(Console console, string BaseRom, string CSTMN_Base_path, string INJCT_Rom_path, string ini_path, string[] bootimages, string GameName)
        {
            string temppath = Properties.Settings.Default.WorkingPath + "/temp";
            Directory.CreateDirectory(temppath); 

            switch (console)
            {
                case Console.NDS:
                    CopyBase(BaseRom, CSTMN_Base_path, temppath);
                    editXML(GameName, temppath);
                    break;

                case Console.N64:
                    CopyBase(BaseRom, CSTMN_Base_path, temppath);
                    editXML(GameName, temppath);
                    break;

                case Console.GBA:
                    CopyBase(BaseRom, CSTMN_Base_path, temppath);
                    editXML(GameName, temppath);
                    break;

                case Console.NES:
                    CopyBase(BaseRom, CSTMN_Base_path, temppath);
                    NESSNES(temppath, INJCT_Rom_path);
                    editXML(GameName, temppath);
                    break;

                case Console.SNES:
                    CopyBase(BaseRom, CSTMN_Base_path, temppath);
                    NESSNES(temppath, INJCT_Rom_path);
                    editXML(GameName, temppath);
                    break;
            }
        }

        // This function changes TitleID, ProductCode and GameName in app.xml (ID) and meta.xml (ID, ProductCode, Name)
        private static void editXML(string GameName, string workpath)  
        {

        }

        //This function copies the custom or normal Base to the working directory
        private static void CopyBase(string baserom, string custom_path, string output)
        {
            if (baserom == "Custom")
            {
                DirectoryCopy(custom_path, output, true);
            }
            else
            {
                DirectoryCopy(Properties.Settings.Default.BaseRomPath + "/" + baserom, output, true);
            }
        }



        private static void NESSNES(string workpath, string romtoinject)
        {
            string[] RPX = Directory.GetFiles(workpath, "*.rpx"); //To get the RPX path where the NES/SNES rom needs to be Injected in

            RPXedit(false, RPX[0]); //Decompresses the RPX to be able to write the game into it

            using(Process retroinject = new Process()) //This part uses retroinject to inject the rom into the RPX
            {
                retroinject.StartInfo.UseShellExecute = false;
                retroinject.StartInfo.CreateNoWindow = true;
                retroinject.StartInfo.FileName = "";
                retroinject.StartInfo.Arguments = RPX[0] +" "+romtoinject+" "+RPX[0];  
                retroinject.Start();
            }
            RPXedit(true, RPX[0]); //Compresses the RPX
        }




        //Compressed or decompresses the RPX using wiiurpxtool
        private static void RPXedit(bool compress, string rpxpath)
        {
            
            using (Process rpxtool = new Process())
            {
                //That the window stays hidden
                rpxtool.StartInfo.UseShellExecute = false;
                rpxtool.StartInfo.CreateNoWindow = true;


                rpxtool.StartInfo.FileName = ""; // Gotta add the path here, still dont know where i gotta put the tools
                
                if (compress)
                {
                    rpxtool.StartInfo.Arguments = "-c " + rpxpath;
                }
                else
                {
                    rpxtool.StartInfo.Arguments = "-d " + rpxpath;
                }
                rpxtool.Start();
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
