using System;
using System.Collections.Generic;
using System.Text;

   public class GDIObject
    {
        int fGDIObjectType;        // one of the OBJ_XXX constants from GDI32.

        protected GDIObject(int aType)
        {
            fGDIObjectType = aType;
        }

        public int GDIObjectType
        {
            get { return fGDIObjectType; }
        }
   }
