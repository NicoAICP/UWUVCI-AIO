using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CNUSPACKER;

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
                    NDS(temppath, INJCT_Rom_path);
                    editXML(GameName, temppath);
                    Images(bootimages, temppath);
                    break;

                case Console.N64:
                    CopyBase(BaseRom, CSTMN_Base_path, temppath);
                    N64(temppath, INJCT_Rom_path);
                    editXML(GameName, temppath);
                    Images(bootimages, temppath);
                    break;

                case Console.GBA:
                    CopyBase(BaseRom, CSTMN_Base_path, temppath);
                    GBA(temppath, INJCT_Rom_path);
                    editXML(GameName, temppath);
                    Images(bootimages, temppath);
                    break;

                case Console.NES:
                    CopyBase(BaseRom, CSTMN_Base_path, temppath);
                    NESSNES(temppath, INJCT_Rom_path);
                    editXML(GameName, temppath);
                    Images(bootimages, temppath);
                    break;

                case Console.SNES:
                    CopyBase(BaseRom, CSTMN_Base_path, temppath);
                    NESSNES(temppath, INJCT_Rom_path);
                    editXML(GameName, temppath);
                    Images(bootimages, temppath);
                    break;
            }
        }
        public static void packing(string Gamename)
        {
            CNUSPACKER.Program.Main(args: new[] { "-in", Properties.Settings.Default.WorkingPath + "/temp", "-out", Properties.Settings.Default.InjectionPath + "/"+Gamename, "-encryptKeyWith", Properties.Settings.Default.CommonKey });
        }
        public static void download(string BaseRom)
        {
            string TID = null;
            string TK = null; 
            #region NDS
            #endregion
            #region N64
            #endregion
            #region GBA
            #endregion
            #region NES
            #endregion
            #region SNES
            if(BaseRom == "SMetroidEU")
            {
                TID = "000500001010a700";
                TK = Properties.Settings.Default.SMetroidEU;
                
            }
            else if(BaseRom == "SMetroidUS")
            {
                TID = "000500001010a600";
                TK = Properties.Settings.Default.SMetroidUS;
            }

            else if (BaseRom == "SMetroidJP")
            {
                TID = "000500001010a500";
                TK = Properties.Settings.Default.SMetroidJP;
            }
            else if (BaseRom == "EarthboundEU")
            {
                TID = "0005000010133500";
                TK = Properties.Settings.Default.EarthboundEU;
            }
            else if (BaseRom == "EarthboundUS")
            {
                TID = "0005000010133400";
                TK = Properties.Settings.Default.EarthboundJP;
            }
            else if (BaseRom == "EarthboundJP")
            {
                TID = "0005000010133000";
                TK = Properties.Settings.Default.EarthboundJP;

            }
            else if (BaseRom == "DKCEU")
            {
                TID = "0005000010109600";
                TK = Properties.Settings.Default.DKCEU;
            }
            else if (BaseRom == "DKCUS")
            {
                TID = "0005000010109500";
                TK = Properties.Settings.Default.DKCUS;
            }
            #endregion
            Directory.SetCurrentDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/JNUSTOOL/"));
            using (Process download = new Process())
            {
                download.StartInfo.FileName = "java";
                download.StartInfo.Arguments = "-jar JNUSTOOL.jar "+ TID + " " + TK + " -file .*";
                download.Start();
                download.WaitForExit();
                switch (BaseRom)
                {
                    #region NDS
                    #endregion
                    #region N64
                    #endregion
                    #region GBA
                    #endregion
                    #region NES
                    #endregion
                    #region SNES
                    case "SMetroidEU":
                        DirectoryCopy("Super Metroid [JAJP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Super Metroid [JAJP01]", true);
                        break;
                    case "SMetroidUS":
                        DirectoryCopy("Super Metroid [JAJE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Super Metroid [JAJ301]", true);
                        break;
                    case "SMetroidJP":
                        DirectoryCopy("スーパーメトロイド [JAJJ01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("スーパーメトロイド [JAJJ01]", true);
                        break;
                    case "EarthboundEU":
                        DirectoryCopy("EarthBound [JBBP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("EarthBound [JBBP01]", true);
                        break;
                    case "EarthboundUS":
                        DirectoryCopy("EarthBound [JBBE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("EarthBound [JBBE01]", true);
                        break;
                    case "EarthboundJP":
                        DirectoryCopy("MOTHER [FBDJ01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("MOTHER [FBDJ01]", true);
                        break;
                    case "DKCEU":
                        DirectoryCopy("Donkey Kong Country [JACP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Donkey Kong Country [JACP01]", true);
                        break;
                    case "DKCUS":
                        DirectoryCopy("Donkey Kong Country [JACE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Donkey Kong Country [JACE01]", true);
                        break;
                        #endregion

                }
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            }
            
        }
        // This function changes TitleID, ProductCode and GameName in app.xml (ID) and meta.xml (ID, ProductCode, Name)
        private static void editXML(string GameName, string workpath)  
        {
            string xmlFile = workpath + "/meta/meta.xml";
            string xmlFile2 = workpath + "/code/app.xml";
            var random = new Random();
            var ID = String.Format("{0:X4}", random.Next(0x10000));
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);
            XmlNode node = doc.SelectSingleNode("menu/longname_ja");
            XmlNode node2 = doc.SelectSingleNode("menu/longname_en");
            XmlNode node3 = doc.SelectSingleNode("menu/longname_fr");
            XmlNode node4 = doc.SelectSingleNode("menu/longname_de");
            XmlNode node5 = doc.SelectSingleNode("menu/longname_it");
            XmlNode node6 = doc.SelectSingleNode("menu/longname_es");
            XmlNode node7 = doc.SelectSingleNode("menu/longname_zhs");
            XmlNode node8 = doc.SelectSingleNode("menu/longname_ko");
            XmlNode node9 = doc.SelectSingleNode("menu/longname_nl");
            XmlNode node10 = doc.SelectSingleNode("menu/longname_pt");
            XmlNode node11 = doc.SelectSingleNode("menu/longname_ru");
            XmlNode node12 = doc.SelectSingleNode("menu/longname_zht");
            XmlNode node13 = doc.SelectSingleNode("menu/product_code");
            XmlNode node14 = doc.SelectSingleNode("menu/title_id");
            node13.InnerText = "WUP-N-" + ID;
            node14.InnerText = "0005000010" + ID + "00";
            node.InnerText = GameName;
            node2.InnerText = GameName;
            node3.InnerText = GameName;
            node4.InnerText = GameName;
            node5.InnerText = GameName;
            node6.InnerText = GameName;
            node7.InnerText = GameName;
            node8.InnerText = GameName;
            node9.InnerText = GameName;
            node10.InnerText = GameName;
            node11.InnerText = GameName;
            node12.InnerText = GameName;
            XmlNode mode = doc.SelectSingleNode("menu/shortname_ja");
            XmlNode mode2 = doc.SelectSingleNode("menu/shortname_en");
            XmlNode mode3 = doc.SelectSingleNode("menu/shortname_fr");
            XmlNode mode4 = doc.SelectSingleNode("menu/shortname_de");
            XmlNode mode5 = doc.SelectSingleNode("menu/shortname_it");
            XmlNode mode6 = doc.SelectSingleNode("menu/shortname_es");
            XmlNode mode7 = doc.SelectSingleNode("menu/shortname_zhs");
            XmlNode mode8 = doc.SelectSingleNode("menu/shortname_ko");
            XmlNode mode9 = doc.SelectSingleNode("menu/shortname_nl");
            XmlNode mode10 = doc.SelectSingleNode("menu/shortname_pt");
            XmlNode mode11 = doc.SelectSingleNode("menu/shortname_ru");
            XmlNode mode12 = doc.SelectSingleNode("menu/shortname_zht");
            mode.InnerText = GameName;
            mode2.InnerText = GameName;
            mode3.InnerText = GameName;
            mode4.InnerText = GameName;
            mode5.InnerText = GameName;
            mode6.InnerText = GameName;
            mode7.InnerText = GameName;
            mode8.InnerText = GameName;
            mode9.InnerText = GameName;
            mode10.InnerText = GameName;
            mode11.InnerText = GameName;
            mode12.InnerText = GameName;
            doc.Save(xmlFile);
            XmlDocument doc2 = new XmlDocument();
            doc.Load(xmlFile2);
            XmlNode n2ode = doc.SelectSingleNode("app/title_id");
            n2ode.InnerText = "0005000010" + ID + "00";
            doc.Save(xmlFile2);
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
            string[] RPX = Directory.GetFiles(workpath+"/code", "*.rpx"); //To get the RPX path where the NES/SNES rom needs to be Injected in

            RPXedit(false, RPX[0]); //Decompresses the RPX to be able to write the game into it

            Process retroinject = new Process();
                retroinject.StartInfo.UseShellExecute = false;
                retroinject.StartInfo.CreateNoWindow = true;
                retroinject.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/retroinject.exe");
                retroinject.StartInfo.Arguments = RPX[0] +" "+romtoinject+" "+RPX[0];  
                retroinject.Start();
                retroinject.WaitForExit();
            
            RPXedit(true, RPX[0]); //Compresses the RPX
        }
        private static void GBA (string workpath, string romtoinject)
        {

        }
        private static void NDS(string workpath, string romtoinject)
        {

        }
        private static void N64(string workpath, string romtoinject)
        {

        }

        //Compressed or decompresses the RPX using wiiurpxtool
        private static void RPXedit(bool compress, string rpxpath)
        {

           Process rpxtool = new Process();
                //That the window stays hidden
                rpxtool.StartInfo.UseShellExecute = false;
                rpxtool.StartInfo.CreateNoWindow = true;


                rpxtool.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/wiiurpxtool.exe"); ; //
                
                if (compress)
                {
                    rpxtool.StartInfo.Arguments = "-c " + rpxpath;
                }
                else
                {
                    rpxtool.StartInfo.Arguments = "-d " + rpxpath;
                }
                rpxtool.Start();
            rpxtool.WaitForExit();
        }
        private static void Images(string[] paths, string workpath)
        {
            bool tv = false;
            bool drc = false;
            bool icon = false;
            bool logo = false;
            bool fixup = false;
            List<string> Tgaverifyoutput = new List<string>();
            #region check if file exists and convert if needed
            if (paths[0] != null)
            {
                tv = true;

                if (paths[0].EndsWith(".png")) //covnert png to tga
                {
                    Process png2tga = new Process();
                        //That the window stays hidden
                        png2tga.StartInfo.UseShellExecute = false;
                        png2tga.StartInfo.CreateNoWindow = true;


                        png2tga.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/png2tga.exe"); // Gotta add the path here, still dont know where i gotta put the tools

                        png2tga.StartInfo.Arguments = paths[0] + paths[0].Replace(".png", ".tga");

                        png2tga.Start();
                    png2tga.WaitForExit();
                    paths[0] = paths[0].Replace(".png", ".tga");
                }
            }

            if (paths[1] != null)
            {
                drc = true;
                if (paths[1].EndsWith(".png")) 
                {
                    if (paths[1].EndsWith(".png")) //covnert png to tga
                    {
                        Process png2tga = new Process();
                        
                            //That the window stays hidden
                            png2tga.StartInfo.UseShellExecute = false;
                            png2tga.StartInfo.CreateNoWindow = true;


                            png2tga.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/png2tga.exe"); // Gotta add the path here, still dont know where i gotta put the tools

                            png2tga.StartInfo.Arguments = paths[1] + paths[1].Replace(".png", ".tga");

                            png2tga.Start();
                        png2tga.WaitForExit();
                        paths[1] = paths[1].Replace(".png", ".tga");
                    }
                }
               
            }
           
            if (paths[2] != null)
            {
                icon = true;
                if (paths[2].EndsWith(".png"))
                {
                    if (paths[2].EndsWith(".png")) //covnert png to tga
                    {
                        Process png2tga = new Process();
                        
                            //That the window stays hidden
                            png2tga.StartInfo.UseShellExecute = false;
                            png2tga.StartInfo.CreateNoWindow = true;


                            png2tga.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/png2tga.exe"); // Gotta add the path here, still dont know where i gotta put the tools

                            png2tga.StartInfo.Arguments = paths[2] + paths[2].Replace(".png", ".tga");

                            png2tga.Start();
                        png2tga.WaitForExit();
                        paths[2] = paths[2].Replace(".png", ".tga");
                    }
                }

            }
           
            
            if (paths[3] != null)
            {
                logo = true;
                if (paths[3].EndsWith(".png"))
                {
                    if (paths[3].EndsWith(".png")) //covnert png to tga
                    {
                        Process png2tga = new Process();
                        
                            //That the window stays hidden
                            png2tga.StartInfo.UseShellExecute = false;
                            png2tga.StartInfo.CreateNoWindow = true;


                            png2tga.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/png2tga.exe"); // Gotta add the path here, still dont know where i gotta put the tools

                            png2tga.StartInfo.Arguments = paths[3] + paths[3].Replace(".png", ".tga");

                            png2tga.Start();
                        png2tga.WaitForExit();
                        paths[3] = paths[3].Replace(".png", ".tga");
                    }
                }

            }
            #endregion
            #region Check if files are correct and then copy them into the work dir
            Directory.CreateDirectory(Properties.Settings.Default.WorkingPath + "/img");
            if (tv)
            {
                File.Copy(paths[0], Properties.Settings.Default.WorkingPath + "/img/bootTvTex.tga");
            }
            if (drc)
            {
                File.Copy(paths[1], Properties.Settings.Default.WorkingPath + "/img/bootDrcTex.tga");
            }
            if (icon)
            {
                File.Copy(paths[2], Properties.Settings.Default.WorkingPath + "/img/iconTex.tga");
            }
            if (logo)
            {
                File.Copy(paths[3], Properties.Settings.Default.WorkingPath + "/img/bootLogoTex.tga");
            }
            if (tv||drc||icon||logo) {
                Process tgaverify = new Process();
                
                    //That the window stays hidden
                    tgaverify.StartInfo.UseShellExecute = false;
                    tgaverify.StartInfo.CreateNoWindow = true;


                    tgaverify.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/tga_verify.exe"); // Gotta add the path here, still dont know where i gotta put the tools


                    tgaverify.StartInfo.Arguments = Properties.Settings.Default.WorkingPath + "/img";
                    tgaverify.StartInfo.RedirectStandardOutput = true;

                    tgaverify.Start();
                    while (!tgaverify.StandardOutput.EndOfStream)
                    {
                        string line = tgaverify.StandardOutput.ReadLine();
                        Tgaverifyoutput.Add(line);
                    }
                    tgaverify.WaitForExit();
                
                for (int i = 0; i < Tgaverifyoutput.Count(); i++)
                {
                    if (tv)
                    {
                        if (Tgaverifyoutput[i].Contains("bootTvTex"))
                        {
                            if (Tgaverifyoutput[i].Contains("Error"))
                            {
                                fixup = true;
                            }
                        }
                    }
                    if (drc)
                    {
                        if (Tgaverifyoutput[i].Contains("bootDrcTex"))
                        {
                            if (Tgaverifyoutput[i].Contains("Error"))
                            {
                                fixup = true;
                            }
                        }
                    }
                    if (icon)
                    {
                        if (Tgaverifyoutput[i].Contains("iconTex"))
                        {
                            if (Tgaverifyoutput[i].Contains("Error"))
                            {
                                fixup = true;
                            }
                        }
                    }
                    if (logo)
                    {
                        if (Tgaverifyoutput[i].Contains("bootLogoTex"))
                        {
                            if (Tgaverifyoutput[i].Contains("Error"))
                            {
                                fixup = true;
                            }
                        }
                    }
                }
                if (fixup)
                {
                    Process tgaverify2 = new Process();
                    
                        //That the window stays hidden
                        tgaverify2.StartInfo.UseShellExecute = false;
                        tgaverify2.StartInfo.CreateNoWindow = true;


                        tgaverify2.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/tga_verify.exe");


                        tgaverify2.StartInfo.Arguments = "--fixup " + Properties.Settings.Default.WorkingPath + "/img";


                        tgaverify2.Start();
                    tgaverify2.WaitForExit();
                    
                }
                if (tv)
                {
                    File.Delete(workpath + "/meta/bootTvTex.tga");
                    File.Move(Properties.Settings.Default.WorkingPath + "/img/bootTvTex.tga", workpath + "/meta/bootTvTex.tga");
                }
                if (drc)
                {
                    File.Delete(workpath + "/meta/bootDrcTex.tga");
                    File.Move(Properties.Settings.Default.WorkingPath + "/img/bootDrcTex.tga", workpath + "/meta/bootDrcTex.tga");
                }
                if (icon)
                {
                    File.Delete(workpath + "/meta/iconTex.tga");
                    File.Move(Properties.Settings.Default.WorkingPath + "/img/iconTex.tga", workpath + "/meta/iconTex.tga");
                }
                if (logo)
                {
                    File.Delete(workpath + "/meta/bootLogoTex.tga");
                    File.Move(Properties.Settings.Default.WorkingPath + "/img/bootLogoTex.tga", workpath + "/meta/bootLogoTex.tga");
                }
                #endregion
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
