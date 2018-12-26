using System;
using System.Collections;
using System.IO;

namespace MetodosUteis
{
    /*********************************************************************************
     * 
     * Classe: Ini
     * Descrição: Realiza manipulação de arquivos .ini
     * 
     * Guilherme Alves
     * guiigos.alves@gmail.com
     * http://guiigos.com
     * 
     *********************************************************************************/

    public class Ini
    {
        private Hashtable hashtable = new Hashtable();
        private String iniFilePath;

        private struct SectionPair
        {
            public String Section;
            public String Key;
        }

        public Ini(string iniPath)
        {
            TextReader trFile = null;
            String[] arrKey = null;
            String strRoot = null;
            String strLine = null;

            iniFilePath = iniPath;

            if (File.Exists(iniPath))
            {
                try
                {
                    trFile = new StreamReader(iniPath);
                    strLine = trFile.ReadLine();

                    while (strLine != null)
                    {
                        strLine = strLine.Trim();

                        if (!String.IsNullOrEmpty(strLine))
                        {
                            if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                            {
                                strRoot = strLine.Substring(1, strLine.Length - 2);
                            }
                            else
                            {
                                arrKey = strLine.Split(new char[] { '=' }, 2);

                                SectionPair sectionPair;
                                String value = null;

                                if (strRoot == null) strRoot = "ROOT";

                                sectionPair.Section = strRoot;
                                sectionPair.Key = arrKey[0];

                                if (arrKey.Length > 1) value = arrKey[1];

                                hashtable.Add(sectionPair, value);
                            }
                        }

                        strLine = trFile.ReadLine();
                    }

                }
                catch (Exception error)
                {
                    throw error;
                }
                finally
                {
                    if (trFile != null) trFile.Close();
                }
            }
        }

        public string GetSetting(string section, string setting)
        {
            SectionPair sectionPair;
            sectionPair.Section = section;
            sectionPair.Key = setting;

            return (String)hashtable[sectionPair];
        }

        public string[] GetSection(string section)
        {
            ArrayList arrSelections = new ArrayList();

            foreach (SectionPair pair in hashtable.Keys)
                if (pair.Section.Equals(section))
                    arrSelections.Add(pair.Key);

            return (String[])arrSelections.ToArray(typeof(String));
        }

        public void AddSetting(string section, string setting, string value)
        {
            SectionPair sectionPair;
            sectionPair.Section = section;
            sectionPair.Key = setting;

            if (hashtable.ContainsKey(sectionPair)) hashtable.Remove(sectionPair);

            hashtable.Add(sectionPair, value);
        }

        public void DeleteSetting(string section, string setting)
        {
            SectionPair sectionPair;
            sectionPair.Section = section;
            sectionPair.Key = setting;

            if (hashtable.ContainsKey(sectionPair)) hashtable.Remove(sectionPair);
        }

        public void SaveSettings(string path)
        {
            ArrayList sections = new ArrayList();
            String strToSave = String.Empty;
            String strTemp = String.Empty;

            foreach (SectionPair sectionPair in hashtable.Keys)
                if (!sections.Contains(sectionPair.Section))
                    sections.Add(sectionPair.Section);

            foreach (String section in sections)
            {
                strToSave += String.Format("[{0}]\r\n", section);

                foreach (SectionPair sectionPair in hashtable.Keys)
                {
                    if (sectionPair.Section == section)
                    {
                        strTemp = (String)hashtable[sectionPair];
                        if (strTemp != null) strTemp = String.Format("={0}", strTemp);
                        strToSave += (sectionPair.Key + strTemp + "\r\n");
                    }
                }

                strToSave += "\r\n";
            }

            try
            {
                TextWriter tw = new StreamWriter(path);
                tw.Write(strToSave);
                tw.Close();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void SaveSettings()
        {
            SaveSettings(iniFilePath);
        }
    }
}
