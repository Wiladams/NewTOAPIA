using System.Collections.Generic;

namespace NewTOAPIA.GL
{
    public class GluInfo
    {
        static List<string> gExtensions;
        static bool gExtensionsFilled;

        static GluInfo()
        {
            gExtensions = new List<string>();
            gExtensionsFilled = false;
        }

        public static List<string> ExtensionList
        {
            get
            {
                if (!gExtensionsFilled)
                {
                    // Get the extensions string, then fill the dictionary
                    string extensions = Extensions;

                    // Now split is up for easier access
                    string[] splitextensions = extensions.Split(new char[] { ' ' });

                    foreach (string s in splitextensions)
                    {
                        gExtensions.Add(s);
                    }

                    gExtensionsFilled = true;
                }

                return gExtensions;
            }
        }

        public static string Extensions
        {
            get 
            {
                string extensions = Glu.GetString(GluStringName.Extensions);
                return extensions;
            }
        }

        public static string Version
        {
            get
            {
                return Glu.GetString(GluStringName.Version);
            }
        }
    }
}
