using TOAPI.GDI32;

namespace NewTOAPIA.Drawing.GDI
{
    public class GDITextCapabilities
    {
        int fCapFlags;

        public GDITextCapabilities(int capFlags)
        {
            fCapFlags = capFlags;
        }

        public int CapabilityFlags
        {
            get { return fCapFlags; } 
        }
 
        public bool SupportsCharacterOutputPrecision
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_OP_CHARACTER)>0);
                return result;
            }
        }

        public bool SupportsStrokeOutputPrecision
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_OP_STROKE)>0);
                return result;
            }
        }

        public bool SupportsStrokeClipPrecision
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_CP_STROKE)>0);
                return result;
            }
        }

        public bool Supports90DegreeCharacterRotation
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_CR_90)>0);
                return result;
            }
        }

        public bool SupportsAnyCharacterRotation
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_CR_ANY)>0);
                return result;
            }
        }

        public bool SupportsIndividualHorizontalAndVerticalScaling
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_SF_X_YINDEP)>0);
                return result;
            }
        }

        public bool SupportsDoubledCharacterScaling
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_SA_DOUBLE)>0);
                return result;
            }
        }

        public bool SupportsOnlyIntegerScaling
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_SA_INTEGER)>0);
                return result;
            }
        }

        public bool SupportsContinuousScaling
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_SA_CONTIN)>0);
                return result;
            }
        }

        public bool CanRenderDoubleWeightedCharacters
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_EA_DOUBLE)>0);
                return result;
            }
        }


        public bool CanRenderItalicCharacters
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_IA_ABLE)>0);
                return result;
            }
        }

        public bool CanRenderUnderlinedCharacters
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_UA_ABLE)>0);
                return result;
            }
        }

        public bool CanRenderStrikeoutCharacters
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_SO_ABLE)>0);
                return result;
            }
        }


        public bool CanRenderRasterFonts
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_RA_ABLE)>0);
                return result;
            }
        }


        public bool CanRenderVectorFonts
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_VA_ABLE)>0);
                return result;
            }
        }

        public bool CannotScrollUsingBitBlt
        {
            get {
                bool result = ((fCapFlags & GDI32.TC_SCROLLBLT)>0);
                return result;
            }
        }





    }
}
