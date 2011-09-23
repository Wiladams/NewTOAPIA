For this example, we use the GLDraw object to draw pixels to the display area.

This method uses DrawBitmap which copies directly into the framebuffer.  It is 
probably the slowest drawing mechanism.  We also use Pixel Zooming to scale.  We
avoid using the rendering pipeline, and do not benefit from any of the nice
filtering that is offered when the pipeline is in use.

Still, it shows how to access the framebuffer most directly.

It could be made faster and smoother by drawing into a custom off screen frame buffer
and then copying that frame buffer to the screen.
