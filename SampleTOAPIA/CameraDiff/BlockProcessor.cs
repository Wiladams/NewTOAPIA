using System;

using NewTOAPIA;
using NewTOAPIA.GL;
using QuadVideo;

namespace NewTOAPIA.GL.Imaging
{
    public class BlockProcessor : UnaryTextureProcessor
    {
        int fBlockSize;
        XYAxesPointMesh fPointMesh;

        public BlockProcessor(GraphicsInterface gi, int width, int height, int blockSize)
            : base(gi, width, height, UnaryTextureProcessor.FixedFrag)
        {
            fBlockSize = 8;
            fPointMesh = new XYAxesPointMesh(gi, new Vector3D(width, height, 0), new Resolution(width / blockSize, height / blockSize), null);
        }

        public int BlockSize
        {
            get { return fBlockSize; }
            set { fBlockSize = value; }
        }


        public static string Block_Frag = @"
// beginning of program
uniform float BlockSize;
uniform float Width;
uniform float Height;

uniform sampler2D Tex0;

void main (void)
{
    vec2 blockIncrement = vec2(BlockSize / Width, BlockSize/Height);
    vec2 blockPosition = floor(gl_TexCoord[0].xy / blockIncrement);
    vec2 blockOffset = blockPosition * blockIncrement;

    vec2 texelIncrement = vec2(1/Width, 1/Height);

    float column, row;      // indexing counters
    vec4 sum = vec4(0.0);   // temporary to hold accumulation of color information for entire block

    // Calculate the average color value for the particular block that we're on
    for(row = 0; row < BlockSize; row+=1) 
        for(column = 0; column < BlockSize; column+=1) 
        {
            vec2 texelOffset = texelIncrement * vec2(column,row);
            vec2 coord = blockOffset + texelOffset;
            sum += texture2D(Tex0, coord.xy);
        }
    
    vec4 result = sum/(BlockSize * BlockSize);

    gl_FragColor = vec4(blockOffset.x, blockOffset.x, blockOffset.x, 1.0);
}
";
    }
}
