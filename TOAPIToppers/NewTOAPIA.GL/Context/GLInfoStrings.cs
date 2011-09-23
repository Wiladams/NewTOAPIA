using System.Collections.Generic;

namespace NewTOAPIA.GL
{
    public class GLInfoStrings
    {
        private GraphicsInterface fGraphicInterface;

        List<string> gExtensions;
        bool gExtensionsFilled;
        
        private GLInfoStrings()
        {
        }

        public GLInfoStrings(GraphicsInterface graphicinterface)
        {
            fGraphicInterface = graphicinterface;
            gExtensions = new List<string>();
            gExtensionsFilled = false;            
        }

        public GraphicsInterface GI
        {
            get { return fGraphicInterface;}
        }

        public List<string> ExtensionList
        {
            get
            {
                if (!gExtensionsFilled)
                {
                    // Get the extensions string, then fill the dictionary
                    string extensions = Extensions;

                    // Now split is up for easier access
                    string [] splitextensions = extensions.Split(new char[]{' '});

                    foreach(string s in splitextensions)
                    {
                        gExtensions.Add(s);
                    }

                    gExtensionsFilled = true;
                }

                return gExtensions;
            }
        }

        public string Extensions
        {
            get { return GI.GetString(StringName.Extensions); }
        }

        public string Version
        {
            get {
                return GI.GetString(StringName.Version);
            }
        }

        public string Vendor
        {
            get {return GI.GetString(StringName.Vendor);}
        }

        public string Renderer
        {
            get {return GI.GetString(StringName.Renderer);}
        }

        public string ShaderVersion
        {
            get { return GI.GetString(StringName.ShadingLanguageVersion); }   
        }
    }
}
