using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using UWUVCI_AIO.Properties;

namespace UWUVCI_AIO
{
    internal static class Injection
    {
        public enum Console { NDS, N64, GBA, NES, SNES }

        private static readonly string tempPath = Path.Combine(Directory.GetCurrentDirectory(), "temp");
        private static readonly string baseRomPath = Path.Combine(tempPath, "baserom");
        private static readonly string imgPath = Path.Combine(tempPath, "img");
        private static readonly string toolsPath = Path.Combine(Directory.GetCurrentDirectory(), "Tools");

        /*
         * Console: Can either be NDS, N64, GBA, NES or SNES
         * baseRom = Name of the BaseRom, which is the folder name too (example: Super Metroid EU will be saved at the BaseRom path under the folder SMetroidEU, so the BaseRom is in this case SMetroidEU).
         * customBasePath = Path to the custom Base. Is null if no custom base is used.
         * injectRomPath = Path to the Rom to be injected into the Base Game.
         * bootImages = String array containing the paths for
         *              bootTvTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 1280x720 and have a bit depth of 24. If null, the original BootImage will be used.
         *              bootDrcTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 854x480 and have a bit depth of 24. If null, the original BootImage will be used.
         *              iconTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 128x128 and have a bit depth of 32. If null, the original BootImage will be used.
         *              bootLogoTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 170x42 and have a bit depth of 32. If null, the original BootImage will be used.
         * gameName = The name of the final game to be entered into the .xml files.
         * iniPath = Only used for N64. Path to the INI configuration. If "blank", a blank ini will be used.
         * darkRemoval = Only used for N64. Indicates whether the dark filter should be removed.
         */
        public static void Inject(Console console, string baseRom, string customBasePath, string injectRomPath, string[] bootImages, string gameName, string iniPath = null, bool darkRemoval = false)
        {
            CopyBase(baseRom, customBasePath);
            switch (console)
            {
                case Console.NDS:
                    NDS(injectRomPath);
                    break;

                case Console.N64:
                    N64(injectRomPath, iniPath, darkRemoval);
                    break;

                case Console.GBA:
                    GBA(injectRomPath);
                    break;

                case Console.NES:
                    NESSNES(injectRomPath);
                    break;
                case Console.SNES:
                    NESSNES(RemoveHeader(injectRomPath));
                    break;
            }

            EditXML(gameName);
            Images(bootImages);
            MessageBox.Show(Resources.InjectionFinishedText, Resources.InjectionFinishedCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Clean()
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }

        public static void Loadiine(string gameName)
        {
            string outputPath = Path.Combine(Properties.Settings.Default.InjectionPath, gameName);
            int i = 0;
            while (Directory.Exists(outputPath))
            {
                outputPath = Path.Combine(Properties.Settings.Default.InjectionPath, $"{gameName}_{i}");
                i++;
            }

            Directory.Move(baseRomPath,outputPath);
            MessageBox.Show(string.Format(Resources.InjectCreatedText, outputPath), Resources.InjectCreatedCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Clean();
        }

        public static void Packing(string gameName)
        {
            string outputPath = Path.Combine(Properties.Settings.Default.InjectionPath, gameName);
            int i = 0;
            while (Directory.Exists(outputPath))
            {
                outputPath = Path.Combine(Properties.Settings.Default.InjectionPath, $"{gameName}_{i}");
                i++;
            }

            CNUSPACKER.Program.Main(new[] { "-in", baseRomPath, "-out", outputPath, "-encryptKeyWith", Properties.Settings.Default.CommonKey });
            MessageBox.Show(string.Format(Resources.InjectCreatedText, outputPath), Resources.InjectCreatedCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Clean();
        }

        public static void Download(string baseRom)
        {
            string TID = null;
            string TK = null;
            string folderName = "";

            switch (baseRom)
            {
                #region NDS
                case "ZSTEU":
                    TID = "00050000101b8d00";
                    TK = Properties.Settings.Default.ZSTEU;
                    folderName = "The Legend of Zelda Spirit Tracks [DARP01]";
                    break;
                case "ZSTUS":
                    TID = "00050000101b8c00";
                    TK = Properties.Settings.Default.ZSTUS;
                    folderName = "The Legend of Zelda Spirit Tracks [DARE01]";
                    break;
                case "ZPHEU":
                    TID = "00050000101c3800";
                    TK = Properties.Settings.Default.ZPHEU;
                    folderName = "The Legend of Zelda Phantom Hourglass [DATP01]";
                    break;
                case "ZPHUS":
                    TID = "00050000101c3700";
                    TK = Properties.Settings.Default.ZPHUS;
                    folderName = "The Legend of Zelda Phantom Hourglass [DATE01]";
                    break;
                case "WWEU":
                    TID = "00050000101a2000";
                    TK = Properties.Settings.Default.WWEU;
                    folderName = "WarioWare Touched! [DAGP01]";
                    break;
                case "WWUS":
                    TID = "00050000101a1f00";
                    TK = Properties.Settings.Default.WWUS;
                    folderName = "WarioWare Touched! [DAGE01]";
                    break;
                #endregion
                #region N64
                case "PMEU":
                    TID = "0005000010199800";
                    TK = Properties.Settings.Default.PMEU;
                    folderName = "Paper Mario [NACP01]";
                    break;
                case "PMUS":
                    TID = "0005000010199700";
                    TK = Properties.Settings.Default.PMUS;
                    folderName = "Paper Mario [NACE01]";
                    break;
                case "FZXUS":
                    TID = "00050000101ebc00";
                    TK = Properties.Settings.Default.FZXUS;
                    folderName = "F-Zero X [NAWE01]";
                    break;
                case "FZXJP":
                    TID = "00050000101ebb00";
                    TK = Properties.Settings.Default.FZXJP;
                    folderName = "F-Zero X [NAWJ01]";
                    break;
                case "DK64EU":
                    TID = "0005000010199300";
                    TK = Properties.Settings.Default.DK64EU;
                    folderName = "Donkey Kong 64 [NAAP01]";
                    break;
                case "DK64US":
                    TID = "0005000010199200";
                    TK = Properties.Settings.Default.DK64US;
                    folderName = "Donkey Kong 64 [NAAE01]";
                    break;
                #endregion
                #region GBA
                case "ZMCEU":
                    TID = "000500001015e500";
                    TK = Properties.Settings.Default.ZMCEU;
                    folderName = "The Legend of Zelda The Minish Cap [PAKP01]";
                    break;
                case "ZMCUS":
                    TID = "000500001015e400";
                    TK = Properties.Settings.Default.ZMCUS;
                    folderName = "The Legend of Zelda The Minish Cap [PAKE01]";
                    break;
                case "MKCEU":
                    TID = "000500001017d200";
                    TK = Properties.Settings.Default.MKCEU;
                    folderName = "Mario Kart Super Circuit [PBDP01]";
                    break;
                case "MKCUS":
                    TID = "000500001017d300";
                    TK = Properties.Settings.Default.MKCUS;
                    folderName = "Mario Kart Super Circuit [PBDE01]";
                    break;
                #endregion
                #region NES
                case "POEU":
                    TID = "0005000010108c00";
                    TK = Properties.Settings.Default.POEU;
                    folderName = "Punch-Out!! [FAKP01]";
                    break;
                case "POUS":
                    TID = "0005000010108b00";
                    TK = Properties.Settings.Default.POUS;
                    folderName = "Punch-Out!! Featuring Mr. Dream [FAKE01]";
                    break;
                case "SMBEU":
                    TID = "0005000010106e00";
                    TK = Properties.Settings.Default.SMBEU;
                    folderName = "Super Mario Bros. [FAAP01]";
                    break;
                case "SMBUS":
                    TID = "0005000010106d00";
                    TK = Properties.Settings.Default.SMBUS;
                    folderName = "Super Mario Bros. [FAAE01]";
                    break;
                #endregion
                #region SNES
                case "SMetroidEU":
                    TID = "000500001010a700";
                    TK = Properties.Settings.Default.SMetroidEU;
                    folderName = "Super Metroid [JAJP01]";
                    break;
                case "SMetroidUS":
                    TID = "000500001010a600";
                    TK = Properties.Settings.Default.SMetroidUS;
                    folderName = "Super Metroid [JAJE01]";
                    break;
                case "SMetroidJP":
                    TID = "000500001010a500";
                    TK = Properties.Settings.Default.SMetroidJP;
                    folderName = "スーパーメトロイド [JAJJ01]";
                    break;
                case "EarthboundEU":
                    TID = "0005000010133500";
                    TK = Properties.Settings.Default.EarthboundEU;
                    folderName = "EarthBound [JBBP01]";
                    break;
                case "EarthboundUS":
                    TID = "0005000010133400";
                    TK = Properties.Settings.Default.EarthboundJP;
                    folderName = "EarthBound [JBBE01]";
                    break;
                case "EarthboundJP":
                    TID = "0005000010133000";
                    TK = Properties.Settings.Default.EarthboundJP;
                    folderName = "MOTHER [FBDJ01]";
                    break;
                case "DKCEU":
                    TID = "0005000010109600";
                    TK = Properties.Settings.Default.DKCEU;
                    folderName = "Donkey Kong Country [JACP01]";
                    break;
                case "DKCUS":
                    TID = "0005000010109500";
                    TK = Properties.Settings.Default.DKCUS;
                    folderName = "Donkey Kong Country [JACE01]";
                    break;
                #endregion
            }

            using (Process download = new Process())
            {
                download.StartInfo.WorkingDirectory = Path.Combine(toolsPath, "JNUSTOOL");
                download.StartInfo.FileName = "java";
                download.StartInfo.Arguments = $"-jar JNUSTOOL.jar {TID} {TK} -file .*";
                download.Start();
                download.WaitForExit();
            }

            Directory.Move(Path.Combine(toolsPath, "JNUSTOOL", folderName), Path.Combine(Properties.Settings.Default.BaseRomPath, baseRom));
        }

        // This function changes TitleID, ProductCode and GameName in app.xml (ID) and meta.xml (ID, ProductCode, Name)
        private static void EditXML(string gameName)
        {
            string metaXml = Path.Combine(baseRomPath, "meta", "meta.xml");
            string appXml = Path.Combine(baseRomPath, "code", "app.xml");
            Random random = new Random();
            string ID = $"{random.Next(0x10000):X4}";

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(metaXml);
                doc.SelectSingleNode("menu/longname_ja").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_en").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_fr").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_de").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_it").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_es").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_zhs").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_ko").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_nl").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_pt").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_ru").InnerText = gameName;
                doc.SelectSingleNode("menu/longname_zht").InnerText = gameName;

                doc.SelectSingleNode("menu/product_code").InnerText = $"WUP-N-{ID}";
                doc.SelectSingleNode("menu/title_id").InnerText = $"0005000020{ID}00";

                doc.SelectSingleNode("menu/shortname_ja").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_fr").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_de").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_en").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_it").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_es").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_zhs").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_ko").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_nl").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_pt").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_ru").InnerText = gameName;
                doc.SelectSingleNode("menu/shortname_zht").InnerText = gameName;
                doc.Save(metaXml);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Error when editing the meta.xml: Values seem to be missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                doc.Load(appXml);
                doc.SelectSingleNode("app/title_id").InnerText = $"0005000020{ID}00";
                doc.Save(appXml);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Error when editing the app.xml: Values seem to be missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //This function copies the custom or normal Base to the working directory
        private static void CopyBase(string baserom, string customPath)
        {
            if (Directory.Exists(baseRomPath)) // sanity check
            {
                Directory.Delete(baseRomPath, true);
            }
            if (baserom == "Custom")
            {
                DirectoryCopy(customPath, baseRomPath, true);
            }
            else
            {
                DirectoryCopy(Path.Combine(Properties.Settings.Default.BaseRomPath, baserom), baseRomPath, true);
            }
        }

        private static void NESSNES(string injectRomPath)
        {
            string rpxFile = Directory.GetFiles(Path.Combine(baseRomPath, "code"), "*.rpx")[0]; //To get the RPX path where the NES/SNES rom needs to be Injected in

            RPXdecomp(rpxFile); //Decompresses the RPX to be able to write the game into it

            using (Process retroinject = new Process())
            {
                retroinject.StartInfo.UseShellExecute = false;
                retroinject.StartInfo.CreateNoWindow = true;
                retroinject.StartInfo.FileName = Path.Combine(toolsPath, "retroinject.exe");
                retroinject.StartInfo.Arguments = $"\"{rpxFile}\" \"{injectRomPath}\" \"{rpxFile}\"";

                retroinject.Start();
                retroinject.WaitForExit();
            }

            RPXcomp(rpxFile); //Compresses the RPX
        }

        private static void GBA(string injectRomPath)
        {
            using (Process psb = new Process())
            {
                psb.StartInfo.UseShellExecute = false;
                psb.StartInfo.CreateNoWindow = true;
                psb.StartInfo.FileName = Path.Combine(toolsPath, "psb.exe");
                psb.StartInfo.Arguments = $"\"{Path.Combine(baseRomPath, "content", "alldata.psb.m")}\" \"{injectRomPath}\" \"{Path.Combine(baseRomPath, "content", "alldata.psb.m")}\"";
                psb.Start();

                psb.WaitForExit();
            }
        }

        private static void NDS(string injectRomPath)
        {
            using (ZipArchive archive = ZipFile.Open(Path.Combine(baseRomPath, "content", "0010", "rom.zip"), ZipArchiveMode.Update))
            {
                string romname = archive.Entries[0].FullName;
                archive.Entries[0].Delete();
                archive.CreateEntryFromFile(injectRomPath, romname);
            }
        }

        private static void N64(string injectRomPath, string iniPath, bool darkRemoval)
        {
            string mainRomPath = Directory.GetFiles(Path.Combine(baseRomPath, "content", "rom"))[0];
            string mainIni = Path.Combine(baseRomPath, "content", "config", $"{Path.GetFileName(mainRomPath)}.ini");
            using (Process n64convert = new Process())
            {
                n64convert.StartInfo.UseShellExecute = false;
                n64convert.StartInfo.CreateNoWindow = true;
                n64convert.StartInfo.FileName = Path.Combine(toolsPath, "N64Converter.exe");
                n64convert.StartInfo.Arguments = $"\"{injectRomPath}\" \"{mainRomPath}\"";

                n64convert.Start();
                n64convert.WaitForExit();
            }

            if (iniPath != null)
            {
                File.Delete(mainIni);
                File.Copy((iniPath == "blank") ? Path.Combine(toolsPath, "blank.ini") : iniPath, mainIni);
            }

            if (darkRemoval)
            {
                string filePath = Path.Combine(baseRomPath, "content", "FrameLayout.arc");
                using (BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.Open)))
                {
                    writer.Seek(0x1AD8, SeekOrigin.Begin);
                    writer.Write(0L);
                }
            }
        }

        //Compressed or decompresses the RPX using wiiurpxtool
        private static void RPXdecomp(string rpxpath)
        {
            using (Process rpxtool = new Process())
            {
                rpxtool.StartInfo.UseShellExecute = false;
                rpxtool.StartInfo.CreateNoWindow = true;
                rpxtool.StartInfo.FileName = Path.Combine(toolsPath, "wiiurpxtool.exe");
                rpxtool.StartInfo.Arguments = $"-d \"{rpxpath}\"";

                rpxtool.Start();
                rpxtool.WaitForExit();
            }
        }
        private static void RPXcomp(string rpxpath)
        {
            using (Process rpxtool = new Process())
            {
                rpxtool.StartInfo.UseShellExecute = false;
                rpxtool.StartInfo.CreateNoWindow = true;
                rpxtool.StartInfo.FileName = Path.Combine(toolsPath, "wiiurpxtool.exe");
                rpxtool.StartInfo.Arguments = $"-c \"{rpxpath}\"";

                rpxtool.Start();
                rpxtool.WaitForExit();
            }
        }

        private static void Images(string[] paths)
        {
            bool tv = false;
            bool drc = false;
            bool icon = false;
            bool logo = false;
            bool fixup = false;

            #region check if file exists and convert if needed

            if (paths[0] != null)
            {
                tv = true;

                if (paths[0].EndsWith(".png")) //convert png to tga
                {
                    paths[0] = ConvertPngToTga(paths[0]);
                }
            }

            if (paths[1] != null)
            {
                drc = true;

                if (paths[1].EndsWith(".png")) //convert png to tga
                {
                    paths[1] = ConvertPngToTga(paths[1]);
                }
            }

            if (paths[2] != null)
            {
                icon = true;

                if (paths[2].EndsWith(".png")) //convert png to tga
                {
                    paths[2] = ConvertPngToTga(paths[2]);
                }
            }

            if (paths[3] != null)
            {
                logo = true;

                if (paths[3].EndsWith(".png")) //convert png to tga
                {
                    paths[3] = ConvertPngToTga(paths[3]);
                }
            }
            #endregion

            #region Check if files are correct and then copy them into the work dir

            if (Directory.Exists(imgPath)) // sanity check
            {
                Directory.Delete(imgPath, true);
            }

            Directory.CreateDirectory(imgPath);

            if (tv)
            {
                File.Copy(paths[0], Path.Combine(imgPath, "bootTvTex.tga"));
            }
            if (drc)
            {
                File.Copy(paths[1], Path.Combine(imgPath, "bootDrcTex.tga"));
            }
            if (icon)
            {
                File.Copy(paths[2], Path.Combine(imgPath, "iconTex.tga"));
            }
            if (logo)
            {
                File.Copy(paths[3], Path.Combine(imgPath, "bootLogoTex.tga"));
            }

            if (tv||drc||icon||logo) {
                using (Process tgaverify = new Process())
                {
                    tgaverify.StartInfo.UseShellExecute = false;
                    tgaverify.StartInfo.CreateNoWindow = true;
                    tgaverify.StartInfo.FileName = Path.Combine(toolsPath, "tga_verify.exe");
                    tgaverify.StartInfo.Arguments = $"\"{imgPath}\"";
                    tgaverify.StartInfo.RedirectStandardError = true;

                    tgaverify.Start();
                    if (!tgaverify.StandardError.EndOfStream)
                    {
                        fixup = true;
                    }

                    tgaverify.WaitForExit();
                }

                if (fixup)
                {
                    using (Process tgaverifyFixup = new Process())
                    {
                        tgaverifyFixup.StartInfo.UseShellExecute = false;
                        tgaverifyFixup.StartInfo.CreateNoWindow = true;
                        tgaverifyFixup.StartInfo.FileName = Path.Combine(toolsPath, "tga_verify.exe");
                        tgaverifyFixup.StartInfo.Arguments = $"--fixup \"{imgPath}\"";

                        tgaverifyFixup.Start();
                        tgaverifyFixup.WaitForExit();
                    }
                }

                if (tv)
                {
                    File.Delete(Path.Combine(baseRomPath, "meta", "bootTvTex.tga"));
                    File.Move(Path.Combine(imgPath, "bootTvTex.tga"), Path.Combine(baseRomPath, "meta", "bootTvTex.tga"));
                }
                if (drc)
                {
                    File.Delete(Path.Combine(baseRomPath, "meta", "bootDrcTex.tga"));
                    File.Move(Path.Combine(imgPath, "bootDrcTex.tga"), Path.Combine(baseRomPath, "meta", "bootDrcTex.tga"));
                }
                if (icon)
                {
                    File.Delete(Path.Combine(baseRomPath, "meta", "iconTex.tga"));
                    File.Move(Path.Combine(imgPath, "iconTex.tga"), Path.Combine(baseRomPath, "meta", "iconTex.tga"));
                }
                if (logo)
                {
                    File.Delete(Path.Combine(baseRomPath, "meta", "bootLogoTex.tga"));
                    File.Move(Path.Combine(imgPath, "bootLogoTex.tga"), Path.Combine(baseRomPath, "meta", "bootLogoTex.tga"));
                }
            }
            #endregion
        }

        private static string ConvertPngToTga(string pngPath)
        {
            string tgaPath = Path.ChangeExtension(pngPath, ".tga");
            using (Process png2tga = new Process())
            {
                png2tga.StartInfo.UseShellExecute = false;
                png2tga.StartInfo.CreateNoWindow = true;
                png2tga.StartInfo.FileName = Path.Combine(toolsPath, "png2tga.exe");
                png2tga.StartInfo.Arguments = $"\"{pngPath}\" \"{tgaPath}\"";

                png2tga.Start();
                png2tga.WaitForExit();
            }

            return tgaPath;
        }

        private static string RemoveHeader(string filePath)
        {
            // logic taken from snesROMUtil
            using (FileStream inStream = new FileStream(filePath, FileMode.Open))
            {
                byte[] header = new byte[512];
                inStream.Read(header, 0, 512);
                string string1 = BitConverter.ToString(header, 8, 3);
                string string2 = Encoding.ASCII.GetString(header, 0, 11);
                string string3 = BitConverter.ToString(header, 30, 16);
                if (string1 != "AA-BB-04" && string2 != "GAME DOCTOR" && string3 != "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00")
                    return filePath;

                string newFilePath = Path.Combine(tempPath, Path.GetFileName(filePath));
                using (FileStream outStream = new FileStream(newFilePath, FileMode.OpenOrCreate))
                {
                    inStream.CopyTo(outStream);
                }

                return newFilePath;
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDirName}");
            }

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            foreach (FileInfo file in dir.EnumerateFiles())
            {
                file.CopyTo(Path.Combine(destDirName, file.Name), false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dir.EnumerateDirectories())
                {
                    DirectoryCopy(subdir.FullName,  Path.Combine(destDirName, subdir.Name), copySubDirs);
                }
            }
        }
    }
}
