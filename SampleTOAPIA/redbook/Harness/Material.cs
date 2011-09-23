
using TOAPI.OpenGL;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.GLU.Shapes;
using NewTOAPIA.UI;

public class Material : GLModel
{
    ColorRGBA ambient = new ColorRGBA(0.0f, 0.0f, 0.0f, 1.0f);
    ColorRGBA diffuse = new ColorRGBA(1.0f, 1.0f, 1.0f, 1.0f);
    float4 position = new float4( 0.0f, 3.0f, 2.0f, 0.0f );
    float []lmodel_ambient = { 0.4f, 0.4f, 0.4f, 1.0f };
    float []local_view = { 0.0f };

    float[] no_mat = { 0.0f, 0.0f, 0.0f, 1.0f };
    float[] mat_ambient = { 0.7f, 0.7f, 0.7f, 1.0f };
    float[] mat_ambient_color = { 0.8f, 0.8f, 0.2f, 1.0f };
    float[] mat_diffuse = { 0.1f, 0.5f, 0.8f, 1.0f };
    float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
    float[] no_shininess = { 0.0f };
    float[] low_shininess = { 5.0f };
    float[] high_shininess = { 100.0f };
    float[] mat_emission = { 0.3f, 0.2f, 0.2f, 0.0f };
    
    GLUSphere sphere;

    protected override void OnSetContext()
    {
        if (sphere == null)
        {
            sphere = new GLUSphere(GI, 1, 32, 32);
            sphere.DrawingStyle = QuadricDrawStyle.Fill;
        }

        GI.Features.DepthTest.Enable();
        gl.glDepthFunc(gl.GL_LESS);

        GI.Features.Lighting.Enable();
        GI.Features.Lighting.Light0.Enable();

        GI.Features.Lighting.Light0.Ambient = ambient;
        GI.Features.Lighting.Light0.Diffuse = diffuse;
        GI.Features.Lighting.Light0.Location = position;

        gl.glLightModelfv(gl.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);
        gl.glLightModelfv(gl.GL_LIGHT_MODEL_LOCAL_VIEWER, local_view);

        
        GI.Buffers.ColorBuffer.Color = new ColorRGBA(0.0f, 0.1f, 0.1f, 0.0f);
    }

    protected override void DrawBegin()
    {
        int w = ViewportWidth;
        int h = ViewportHeight;

        gl.glViewport(0, 0, w, h);

        gl.glMatrixMode(gl.GL_PROJECTION);
        gl.glLoadIdentity();

        if (w <= (h * 2))
            gl.glOrtho(-6.0, 6.0, -3.0 * ((float)h * 2) / (float)w,
                3.0 * ((float)h * 2) / (float)w, -10.0, 10.0);
        else
            gl.glOrtho(-6.0 * (float)w / ((float)h * 2),
                6.0 * (float)w / ((float)h * 2), -3.0, 3.0, -10.0, 10.0);


        GI.Buffers.ColorBuffer.Clear();
        GI.Buffers.DepthBuffer.Clear();

        GI.ShadeModel(ShadingModel.Smooth);
    }

    protected override void DrawContent()
    {
        GI.MatrixMode(MatrixMode.Modelview);
        GI.LoadIdentity();

    /*  draw sphere in first row, first column
     *  diffuse reflection only; no ambient or specular  
     */
        GI.PushMatrix();
        GI.Translate (-3.75f, 3.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, no_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, no_mat);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in first row, second column
    // *  diffuse and specular reflection; low shininess; no ambient
    // */
        GI.PushMatrix();
        GI.Translate(-1.25f, 3.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, mat_specular);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, low_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, no_mat);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in first row, third column
    // *  diffuse and specular reflection; high shininess; no ambient
    // */
        GI.PushMatrix();
        GI.Translate(1.25f, 3.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, mat_specular);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, high_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, no_mat);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in first row, fourth column
    // *  diffuse reflection; emission; no ambient or specular reflection
    // */
        GI.PushMatrix();
        GI.Translate(3.75f, 3.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, no_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, mat_emission);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in second row, first column
    // *  ambient and diffuse reflection; no specular  
    // */
        GI.PushMatrix();
        GI.Translate(-3.75f, 0.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, mat_ambient);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, no_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, no_mat);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in second row, second column
    // *  ambient, diffuse and specular reflection; low shininess
    // */
        GI.PushMatrix();
        GI.Translate(-1.25f, 0.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, mat_ambient);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, mat_specular);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, low_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, no_mat);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in second row, third column
    // *  ambient, diffuse and specular reflection; high shininess
    // */
        GI.PushMatrix();
        GI.Translate(1.25f, 0.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, mat_ambient);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, mat_specular);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, high_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, no_mat);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in second row, fourth column
    // *  ambient and diffuse reflection; emission; no specular
    // */
        GI.PushMatrix();
        GI.Translate(3.75f, 0.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, mat_ambient);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse); 
        GI.Material(GLFace.Front, MaterialParameter.Specular, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, no_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, mat_emission);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in third row, first column
    // *  colored ambient and diffuse reflection; no specular  
    // */
        GI.PushMatrix();
        GI.Translate(-3.75f, -3.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, mat_ambient_color);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, no_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, no_mat);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in third row, second column
    // *  colored ambient, diffuse and specular reflection; low shininess
    // */
        GI.PushMatrix();
        GI.Translate(-1.25f, -3.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, mat_ambient_color);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, mat_specular);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, low_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, no_mat);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in third row, third column
    // *  colored ambient, diffuse and specular reflection; high shininess
    // */
        GI.PushMatrix();
        GI.Translate(1.25f, -3.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, mat_ambient_color);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, mat_specular);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, high_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, no_mat);
        sphere.Render(GI);
        GI.PopMatrix();

    ///*  draw sphere in third row, fourth column
    // *  colored ambient and diffuse reflection; emission; no specular
    // */
        GI.PushMatrix();
        GI.Translate(3.75f, -3.0f, 0.0f);
        GI.Material(GLFace.Front, MaterialParameter.Ambient, mat_ambient_color);
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        GI.Material(GLFace.Front, MaterialParameter.Specular, no_mat);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, no_shininess);
        GI.Material(GLFace.Front, MaterialParameter.Emission, mat_emission);
        sphere.Render(GI);
        GI.PopMatrix();

        GI.Flush();
    }
}

