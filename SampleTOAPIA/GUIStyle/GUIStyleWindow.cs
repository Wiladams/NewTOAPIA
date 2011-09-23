

namespace GUIStyleTest
{
    using System;
    using System.Drawing;

    using NewTOAPIA;
    using NewTOAPIA.Drawing;
    using NewTOAPIA.UI;
    
    class GUIStyleWindow : GraphicWindow
	{
        GraphicGroup rootGroup;

		public GUIStyleWindow()
			: base("GUIStyleWindow", 10, 10, 800, 600)
		{
            CreateComponents();
            
            BackgroundColor = RGBColor.LtGray;       
        }


        void CreateComponents()
        {
            rootGroup = new GraphicGroup("root");
            rootGroup.Debug = true;
            AddGraphic(rootGroup);

            // Use a layout handler so all our graphics line up vertically
            rootGroup.LayoutHandler = new LinearLayout(rootGroup, 4, 4, Orientation.Vertical);

            StringLabel lookLabel = new StringLabel("Look Here:", 0, 0);

            StringLabel boxLabel = new StringLabel("Box", 0, 0);

            Caption cap = new Caption(lookLabel, boxLabel, Position.Right, 4);
            rootGroup.AddGraphic(cap, null);


        //    Switch aSwitch = new Switch("switch1", 100, 200, 100, 20, "Big Switch");
        //    aSwitch.Debug = true;
        //    rootGroup.AddGraphic(aSwitch);

            GraphicGroup controlGroup = new GraphicGroup("controlGroup", 10, 300, 400, 20);
            controlGroup.LayoutHandler = new LinearLayout(controlGroup, 4, 0, Orientation.Vertical);

            GraphicGroup layout1 = new GraphicGroup("layout1", 0, 0, 400, 40);
            layout1.LayoutHandler = new LinearLayout(layout1, 2, 0, Orientation.Horizontal);

            PushButton button1 = new PushButton("Pushy1", 0, 0, 100, 24, new StringLabel("Button1", 0, 0));
            button1.MouseUpEvent += new MouseEventHandler(this.ButtonActivity);
            layout1.AddGraphic(button1);

            PushButton button2 = new PushButton("Play", 104, 0, 100, 24, new StringLabel("Button2", 0, 0));
            button2.MouseDownEvent += new MouseEventHandler(this.PlayActivity);
            layout1.AddGraphic(button2);


            TrackSlider slider = new TrackSlider("slider",
                new GrayTrack("track", 0, 0, 200, 20),
                new GrayBox("box", 0, 0, 20, 16),
                Orientation.Horizontal,
                0, 255, 255);
            slider.PositionChangedEvent += new EventHandler(slider_PositionChangedEvent);

            controlGroup.AddGraphic(slider);
            controlGroup.AddGraphic(layout1);

            rootGroup.AddGraphic(controlGroup);

        }

        void slider_PositionChangedEvent(object sender, EventArgs e)
        {
            //Console.WriteLine("Value: {0}",((TrackSlider)sender).Value);
            // Change the opacity of the window based on 
            // the position of the slider
            double op = ((TrackSlider)sender).Position;
            Opacity = op;
        }

		private void ButtonActivity(object sender, MouseActivityArgs e)
		{
            Console.WriteLine("GUIStyleWindow.ButtonActivity: {0}", e.ToString());
		}

        private void PlayActivity(object sender, MouseActivityArgs e)
        {
            Console.WriteLine("GUIStyleWindow.PlayActivity: {0}", e.ToString());
        }
	}
}
