using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace UWUVCI_AIO
{
    internal static class Injection
    {
        public enum Console { NDS, N64, GBA, NES, SNES }

        private static readonly string tempPath = Path.Combine(Properties.Settings.Default.WorkingPath, "temp");
        private static readonly string imgPath = Path.Combine(Properties.Settings.Default.WorkingPath, "img");

        /*
         * Console: Can either be NDS, N64, GBA, NES or SNES
         * BaseRom = Name of the BaseRom, which is the folder name too (example: Super Metroid EU will be saved at the BaseRom path under the folder SMetroidEU, so the BaseRom is in this case SMetroidEU).
         * CSTMN_Base_path = Path to the custom Base. Is null if no custom base is used.
         * INJCT_Rom_path = Path to the Rom to be injected into the Base Game.
         * ini_path = Only used for N64. Path to the INI configuration. If "blank", a blank ini will be used.
         * bootimages = String array containing the paths for
         *              bootTvTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 1280x720 and have a bit depth of 24. If null, the original BootImage will be used.
         *              bootDrcTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 854x480 and have a bit depth of 24. If null, the original BootImage will be used.
         *              iconTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 128x128 and have a bit depth of 32. If null, the original BootImage will be used.
         *              bootLogoTex: PNG or TGA (PNG gets converted to TGA using UPNG). Needs to be in the dimensions 170x42 and have a bit depth of 32. If null, the original BootImage will be used.
         * darkremoval = Only used for N64. Indicates whether the dark filter should be removed.
         */
        public static void inject(Console console, string BaseRom, string CSTMN_Base_path, string INJCT_Rom_path, string ini_path, string[] bootimages, string GameName, bool darkremoval)
        {
            CopyBase(BaseRom, CSTMN_Base_path);
            switch (console)
            {
                case Console.NDS:
                    NDS(INJCT_Rom_path);
                    break;

                case Console.N64:
                    N64(INJCT_Rom_path, ini_path, darkremoval);
                    break;

                case Console.GBA:
                    GBA(INJCT_Rom_path);
                    break;

                case Console.NES:
                case Console.SNES:
                    NESSNES(INJCT_Rom_path);
                    break;
            }

            editXML(GameName);
            Images(bootimages);
        }

        public static void clean()
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }

            if (Directory.Exists(imgPath))
            {
                Directory.Delete(imgPath, true);
            }
        }

        public static void loadiine(string Gamename)
        {
            if (Directory.Exists(Path.Combine(Properties.Settings.Default.InjectionPath, Gamename)))
            {
                MessageBox.Show("The entered output directory already exists. Consider choosing a different one.");
            }
            else
            {
                Directory.Move(tempPath,Path.Combine(Properties.Settings.Default.InjectionPath, Gamename));
                MessageBox.Show("Inject successfully created.\nYou can find your inject here:\n" + Path.Combine(Properties.Settings.Default.InjectionPath, Gamename));

                clean();
            }
        }

        public static void packing(string Gamename)
        {
            CNUSPACKER.Program.Main(new[] { "-in", tempPath, "-out", Path.Combine(Properties.Settings.Default.InjectionPath, Gamename), "-encryptKeyWith", Properties.Settings.Default.CommonKey });
            MessageBox.Show("Inject successfully created.\nYou can find your inject here:\n" + Path.Combine(Properties.Settings.Default.InjectionPath, Gamename));

            clean();
        }

        public static void download(string BaseRom)
        {
            string TID = null;
            string TK = null;
            #region NDS
            switch (BaseRom)
            {
                case "ZSTEU":
                    TID = "00050000101b8d00";
                    TK = Properties.Settings.Default.ZSTEU;
                    break;
                case "ZSTUS":
                    TID = "00050000101b8c00";
                    TK = Properties.Settings.Default.ZSTUS;
                    break;
                case "ZPHEU":
                    TID = "00050000101c3800";
                    TK = Properties.Settings.Default.ZPHEU;
                    break;
                case "ZPHUS":
                    TID = "00050000101c3700";
                    TK = Properties.Settings.Default.ZPHUS;
                    break;
                case "WWEU":
                    TID = "00050000101a2000";
                    TK = Properties.Settings.Default.WWEU;
                    break;
                case "WWUS":
                    TID = "00050000101a1f00";
                    TK = Properties.Settings.Default.WWUS;
                    break;
            }
            #endregion
            #region N64
            switch (BaseRom)
            {
                case "PMEU":
                    TID = "0005000010199800";
                    TK = Properties.Settings.Default.PMEU;
                    break;
                case "PMUS":
                    TID = "0005000010199700";
                    TK = Properties.Settings.Default.PMUS;
                    break;
                case "FZXUS":
                    TID = "00050000101ebc00";
                    TK = Properties.Settings.Default.FZXUS;
                    break;
                case "FZXJP":
                    TID = "00050000101ebb00";
                    TK = Properties.Settings.Default.FZXJP;
                    break;
                case "DK64EU":
                    TID = "0005000010199300";
                    TK = Properties.Settings.Default.DK64EU;
                    break;
                case "DK64US":
                    TID = "0005000010199200";
                    TK = Properties.Settings.Default.DK64US;
                    break;
            }
            #endregion
            #region GBA
            switch (BaseRom)
            {
                case "ZMCEU":
                    TID = "000500001015e500";
                    TK = Properties.Settings.Default.ZMCEU;
                    break;
                case "ZMCUS":
                    TID = "000500001015e400";
                    TK = Properties.Settings.Default.ZMCUS;
                    break;
                case "MKCEU":
                    TID = "000500001017d200";
                    TK = Properties.Settings.Default.MKCEU;
                    break;
                case "MKCUS":
                    TID = "000500001017d300";
                    TK = Properties.Settings.Default.MKCUS;
                    break;
            }
            #endregion
            #region NES
            switch (BaseRom)
            {
                case "POEU":
                    TID = "0005000010108c00";
                    TK = Properties.Settings.Default.POEU;
                    break;
                case "POUS":
                    TID = "0005000010108b00";
                    TK = Properties.Settings.Default.POUS;
                    break;
                case "SMBEU":
                    TID = "0005000010106e00";
                    TK = Properties.Settings.Default.SMBEU;
                    break;
                case "SMBUS":
                    TID = "0005000010106d00";
                    TK = Properties.Settings.Default.SMBUS;
                    break;
            }
            #endregion
            #region SNES
            switch (BaseRom)
            {
                case "SMetroidEU":
                    TID = "000500001010a700";
                    TK = Properties.Settings.Default.SMetroidEU;
                    break;
                case "SMetroidUS":
                    TID = "000500001010a600";
                    TK = Properties.Settings.Default.SMetroidUS;
                    break;
                case "SMetroidJP":
                    TID = "000500001010a500";
                    TK = Properties.Settings.Default.SMetroidJP;
                    break;
                case "EarthboundEU":
                    TID = "0005000010133500";
                    TK = Properties.Settings.Default.EarthboundEU;
                    break;
                case "EarthboundUS":
                    TID = "0005000010133400";
                    TK = Properties.Settings.Default.EarthboundJP;
                    break;
                case "EarthboundJP":
                    TID = "0005000010133000";
                    TK = Properties.Settings.Default.EarthboundJP;
                    break;
                case "DKCEU":
                    TID = "0005000010109600";
                    TK = Properties.Settings.Default.DKCEU;
                    break;
                case "DKCUS":
                    TID = "0005000010109500";
                    TK = Properties.Settings.Default.DKCUS;
                    break;
            }
            #endregion
            Directory.SetCurrentDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/JNUSTOOL/"));
            using (Process download = new Process())
            {
                download.StartInfo.FileName = "java";
                download.StartInfo.Arguments = $"-jar JNUSTOOL.jar {TID} {TK} -file .*";
                download.Start();
                download.WaitForExit();
                switch (BaseRom)
                {
                    #region NDS
                    case "ZSTEU":
                        DirectoryCopy("The Legend of Zelda Spirit Tracks [DARP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("The Legend of Zelda Spirit Tracks [DARP01]", true);
                        break;
                    case "ZSTUS":
                        DirectoryCopy("The Legend of Zelda Spirit Tracks [DARE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("The Legend of Zelda Spirit Tracks [DARE01]", true);
                        break;
                    case "ZPHEU":
                        DirectoryCopy("The Legend of Zelda Phantom Hourglass [DATP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("The Legend of Zelda Phantom Hourglass [DATP01]", true);
                        break;
                    case "ZPHUS":
                        DirectoryCopy("The Legend of Zelda Phantom Hourglass [DATE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("The Legend of Zelda Phantom Hourglass [DATE01]", true);
                        break;
                    case "WWEU":
                        DirectoryCopy("WarioWare Touched! [DAGP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("WarioWare Touched! [DAGP01]", true);
                        break;
                    case "WWUS":
                        DirectoryCopy("WarioWare Touched! [DAGE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("WarioWare Touched! [DAGE01]", true);
                        break;
                    #endregion
                    #region N64
                    case "PMEU":
                        DirectoryCopy("Paper Mario [NACP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Paper Mario [NACP01]", true);
                        break;
                    case "PMUS":
                        DirectoryCopy("Paper Mario [NACE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Paper Mario [NAEP01]", true);
                        break;
                    case "FZXUS":
                        DirectoryCopy("F-Zero X [NAWE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("F-Zero X [NAWE01]", true);
                        break;
                    case "FZXJP":
                        DirectoryCopy("F-Zero X [NAWJ01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("F-Zero X [NAWJ01]", true);
                        break;
                    case "DK64EU":
                        DirectoryCopy("Donkey Kong 64 [NAAP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Donkey Kong 64 [NAAP01]", true);
                        break;
                    case "DK64US":
                        DirectoryCopy("Donkey Kong 64 [NAAE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Donkey Kong 64 [NAAE01]", true);
                        break;
                    #endregion
                    #region GBA
                    case "ZMCEU":
                        DirectoryCopy("The Legend of Zelda The Minish Cap [PAKP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("The Legend of Zelda The Minish Cap [PAKP01]", true);
                        break;
                    case "ZMCUS":
                        DirectoryCopy("The Legend of Zelda The Minish Cap [PAKE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("The Legend of Zelda The Minish Cap [PAKE01]", true);
                        break;
                    case "MKCEU":
                        DirectoryCopy("Mario Kart Super Circuit [PBDP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Mario Kart Super Circuit [PBDP01]", true);
                        break;
                    case "MKCUS":
                        DirectoryCopy("Mario Kart Super Circuit [PBDE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Mario Kart Super Circuit [PBDE01]", true);
                        break;
                    #endregion
                    #region NES
                    case "POEU":
                        DirectoryCopy("Punch-Out!! [FAKP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Punch-Out!! [FAKP01]", true);
                        break;
                    case "POUS":
                        DirectoryCopy("Punch-Out!! Featuring Mr. Dream [FAKE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Punch-Out!! Featuring Mr. Dream [FAKE01]", true);
                        break;
                    case "SMBEU":
                        DirectoryCopy("Super Mario Bros. [FAAP01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Super Mario Bros. [FAAP01]", true);
                        break;
                    case "SMBUS":
                        DirectoryCopy("Super Mario Bros. [FAAE01]", Properties.Settings.Default.BaseRomPath + "/" + BaseRom, true);
                        Directory.Delete("Super Mario Bros. [FAAE01]", true);
                        break;
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
        private static void editXML(string GameName)
        {
            string metaXml = Path.Combine(tempPath, "meta/meta.xml");
            string appXml = Path.Combine(tempPath, "code/app.xml");
            Random random = new Random();
            string ID = $"{random.Next(0x10000):X4}";

            XmlDocument doc = new XmlDocument();
            doc.Load(metaXml);
            doc.SelectSingleNode("menu/longname_ja").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_en").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_fr").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_de").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_it").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_es").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_zhs").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_ko").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_nl").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_pt").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_ru").InnerText = GameName;
            doc.SelectSingleNode("menu/longname_zht").InnerText = GameName;

            doc.SelectSingleNode("menu/product_code").InnerText = $"WUP-N-{ID}";
            doc.SelectSingleNode("menu/title_id").InnerText = $"0005000020{ID}00";

            doc.SelectSingleNode("menu/shortname_ja").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_fr").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_de").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_en").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_it").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_es").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_zhs").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_ko").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_nl").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_pt").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_ru").InnerText = GameName;
            doc.SelectSingleNode("menu/shortname_zht").InnerText = GameName;
            doc.Save(metaXml);

            doc.Load(appXml);
            doc.SelectSingleNode("app/title_id").InnerText = $"0005000020{ID}00";
            doc.Save(appXml);
        }

        //This function copies the custom or normal Base to the working directory
        private static void CopyBase(string baserom, string custom_path)
        {
            if (Directory.Exists(tempPath)) // sanity check
            {
                Directory.Delete(tempPath, true);
            }
            if (baserom == "Custom")
            {
                DirectoryCopy(custom_path, tempPath, true);
            }
            else
            {
                DirectoryCopy(Path.Combine(Properties.Settings.Default.BaseRomPath, baserom), tempPath, true);
            }
        }

        private static void NESSNES(string romtoinject)
        {
            string rpxFile = Directory.GetFiles(Path.Combine(tempPath, "code"), "*.rpx")[0]; //To get the RPX path where the NES/SNES rom needs to be Injected in

            RPXdecomp(rpxFile); //Decompresses the RPX to be able to write the game into it

            using (Process retroinject = new Process())
            {
                retroinject.StartInfo.UseShellExecute = false;
                retroinject.StartInfo.CreateNoWindow = true;
                retroinject.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/retroinject.exe");
                retroinject.StartInfo.Arguments = $"\"{rpxFile}\" \"{romtoinject}\" \"{rpxFile}\"";

                retroinject.Start();
                retroinject.WaitForExit();
            }

            RPXcomp(rpxFile); //Compresses the RPX
        }

        private static void GBA (string romtoinject)
        {
            using (Process psb = new Process())
            {
                psb.StartInfo.UseShellExecute = false;
                psb.StartInfo.CreateNoWindow = true;
                psb.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/psb.exe");
                psb.StartInfo.Arguments = $"\"{tempPath}/content/alldata.psb.m\" \"{romtoinject}\" \"{tempPath}/content/alldata.psb.m\"";
                psb.Start();

                psb.WaitForExit();
            }
        }

        private static void NDS(string romtoinject)
        {
            using (ZipArchive archive = ZipFile.Open(Path.Combine(tempPath, "content/0010/rom.zip"), ZipArchiveMode.Update))
            {
                string romname = archive.Entries[0].FullName;
                archive.Entries[0].Delete();
                archive.CreateEntryFromFile(romtoinject, romname);
            }
        }

        private static void N64(string romtoinject, string ini_path, bool darkremoval)
        {
            string mainRomPath = Directory.GetFiles(Path.Combine(tempPath, "content/rom"))[0];
            string mainIni = Path.Combine(tempPath, $"content/config/{Path.GetFileName(mainRomPath)}.ini");
            using (Process n64convert = new Process())
            {
                n64convert.StartInfo.UseShellExecute = false;
                n64convert.StartInfo.CreateNoWindow = true;
                n64convert.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/N64Converter.exe");
                n64convert.StartInfo.Arguments = $"\"{romtoinject}\" \"{mainRomPath}\"";

                n64convert.Start();
                n64convert.WaitForExit();
            }

            if (ini_path != null)
            {
                File.Delete(mainIni);
                if (ini_path == "blank")
                {
                    File.Copy(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/blank.ini"), mainIni);
                }
                else
                {
                    File.Copy(ini_path, mainIni);
                }
            }

            if (darkremoval)
            {
                string filePath = Path.Combine(tempPath, "content/FrameLayout.arc");
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
                rpxtool.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/wiiurpxtool.exe");
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
                rpxtool.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/wiiurpxtool.exe");
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
                    tgaverify.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/tga_verify.exe");
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
                        tgaverifyFixup.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/tga_verify.exe");
                        tgaverifyFixup.StartInfo.Arguments = $"--fixup \"{imgPath}\"";

                        tgaverifyFixup.Start();
                        tgaverifyFixup.WaitForExit();
                    }
                }

                if (tv)
                {
                    File.Delete(Path.Combine(tempPath, "meta/bootTvTex.tga"));
                    File.Move(Path.Combine(imgPath, "bootTvTex.tga"), Path.Combine(tempPath, "meta/bootTvTex.tga"));
                }
                if (drc)
                {
                    File.Delete(Path.Combine(tempPath, "meta/bootDrcTex.tga"));
                    File.Move(Path.Combine(imgPath, "bootDrcTex.tga"), Path.Combine(tempPath, "meta/bootDrcTex.tga"));
                }
                if (icon)
                {
                    File.Delete(Path.Combine(tempPath, "meta/iconTex.tga"));
                    File.Move(Path.Combine(imgPath, "iconTex.tga"), Path.Combine(tempPath, "meta/iconTex.tga"));
                }
                if (logo)
                {
                    File.Delete(Path.Combine(tempPath, "meta/bootLogoTex.tga"));
                    File.Move(Path.Combine(imgPath, "bootLogoTex.tga"), Path.Combine(tempPath, "meta/bootLogoTex.tga"));
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
                png2tga.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/png2tga.exe");
                png2tga.StartInfo.Arguments = $"\"{pngPath}\" \"{tgaPath}\"";

                png2tga.Start();
                png2tga.WaitForExit();
            }

            return tgaPath;
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
