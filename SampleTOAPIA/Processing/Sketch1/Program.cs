using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sketch1
{
    using Processing;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PSketch sketch = new Chapter6();
            //PSketch sketch = new Chapter7();
            //PSketch sketch = new Chapter10();
            //PSketch sketch = new Chapter11();
            //PSketch sketch = new Chapter11_15();

            //PSketch sketch = new DocRefSamples();
            //PSketch sketch = new Doc_mouse();

            sketch.Run();
        }
    }
}
