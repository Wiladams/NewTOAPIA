YCrCbVideo
This application demonstrates how to take a captured video
image and do a YCrCb separation using the GPU and a fragment shader.

The video is captured into a texture object, then that object
is rendered as a quad into an offscreen image.  During the rendering
the separation shader is bound, so the Y, Cr, and Cb components
are output to three separate texture objects.

Finally, those three texture objects are displayed on the screen
along with the original image.

