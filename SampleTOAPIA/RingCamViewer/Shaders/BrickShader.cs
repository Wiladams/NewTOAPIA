using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;

    public class BrickShader : GLSLShaderProgram
    {

        // These are uniform
        int BrickColor_Pos = 0;
        int MortarColor_Pos = 0;
        int BrickSize_Pos = 0;
        int BrickPct_Pos = 0;
        int LightPosition_Pos = 0;

        public BrickShader(GraphicsInterface gi)
            : base(gi)
        {
            //Console.WriteLine("BrickShader Link Log: \n{0}", InfoLog);
            GLSLVertexShader vShader = new GLSLVertexShader(gi, Brick_VertexSource);
            AttachShader(vShader);


            GLSLFragmentShader fShader = new GLSLFragmentShader(gi, Brick_FragmentSource);
            AttachShader(fShader);

            Link();

            // Do an initial selection so that all the variables can be located
            Bind();

            BrickColor_Pos = GetUniformLocation("BrickColor");
            MortarColor_Pos = GetUniformLocation("MortarColor");
            BrickSize_Pos = GetUniformLocation("BrickSize");
            BrickPct_Pos = GetUniformLocation("BrickPct");
            
            // From the Vertex Shader
            LightPosition_Pos = GetUniformLocation("LightPosition");

            // Set some initial values
            BrickColor = new ColorRGBA(1.0f, 0.3f, 0.2f);
            MortarColor = new ColorRGBA(0.85f, 0.86f, 0.84f);
            BrickSize = new float2(0.30f, 0.15f);
            BrickPct = new float2(0.90f, 0.85f);
            LightPosition = new Vector3f(0.0f, 0.0f, 4.0f);

            // Unselect so we start in an unselected state
            Unbind();
        }

        #region Properties
        public ColorRGBA BrickColor
        {
            set
            {
                GI.Uniform3f(BrickColor_Pos, value.R, value.G, value.B);
            }
        }

        public ColorRGBA MortarColor
        {
            set
            {
                GI.Uniform3f(MortarColor_Pos, value.R, value.G, value.B);
            }
        }

        public float2 BrickSize
        {
            set
            {
                GI.Uniform2f(BrickSize_Pos, value.x, value.y);
            }
        }

        public float2 BrickPct
        {
            set
            {
                GI.Uniform2f(BrickPct_Pos, value.x, value.y);
            }
        }

        public Vector3f LightPosition
        {
            set
            {
                GI.Uniform3f(LightPosition_Pos, value.x, value.y, value.z);
            }
        }

        #endregion

        #region Shader Source Strings
        public static string Brick_FragmentSource =
    @"
//
// Fragment shader for procedural bricks
//
// Authors: Dave Baldwin, Steve Koren, Randi Rost
//          based on a shader by Darwyn Peachey
//
// Copyright (c) 2002-2006 3Dlabs Inc. Ltd. 
//
// See 3Dlabs-License.txt for license information
//

uniform vec3  BrickColor, MortarColor;
uniform vec2  BrickSize;
uniform vec2  BrickPct;

varying vec2  MCposition;
varying float LightIntensity;

void main()
{
    vec3  color;
    vec2  position, useBrick;
    
    position = MCposition / BrickSize;

    if (fract(position.y * 0.5) > 0.5)
        position.x += 0.5;

    position = fract(position);

    useBrick = step(position, BrickPct);

    color  = mix(MortarColor, BrickColor, useBrick.x * useBrick.y);
    color *= LightIntensity;
    gl_FragColor = vec4(color, 1.0);
}
";

        public static string Brick_VertexSource =
            @"
//
// Vertex shader for procedural bricks
//
// Authors: Dave Baldwin, Steve Koren, Randi Rost
//          based on a shader by Darwyn Peachey
//
// Copyright (c) 2002-2006 3Dlabs Inc. Ltd. 
//
// See 3Dlabs-License.txt for license information
//

uniform vec3 LightPosition;

const float SpecularContribution = 0.3;
const float DiffuseContribution  = 1.0 - SpecularContribution;

varying float LightIntensity;
varying vec2  MCposition;

void main()
{
    vec3 ecPosition = vec3(gl_ModelViewMatrix * gl_Vertex);
    vec3 tnorm      = normalize(gl_NormalMatrix * gl_Normal);
    vec3 lightVec   = normalize(LightPosition - ecPosition);
    vec3 reflectVec = reflect(-lightVec, tnorm);
    vec3 viewVec    = normalize(-ecPosition);
    float diffuse   = max(dot(lightVec, tnorm), 0.0);
    float spec      = 0.0;

    if (diffuse > 0.0)
    {
        spec = max(dot(reflectVec, viewVec), 0.0);
        spec = pow(spec, 16.0);
    }

    LightIntensity  = DiffuseContribution * diffuse +
                      SpecularContribution * spec;

    MCposition      = gl_Vertex.xy;
    gl_Position     = ftransform();
}
";
        #endregion
    }

