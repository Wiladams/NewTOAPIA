DirectShow is spread through various libraries in the system
Type libraries can be found in different forms from .dll files, 
or constructed from IDL sources.

A couple of System libraries contain some TypeLib information.  In these
cases, the TlbImp tool can be used to access them and create interop assemblies
directly.

%windir%\System32\quartz.dll	QuartzTypeLib.dll
%windir%\System32\qedit.dll		DexterTypeLib.dll

The Quartz library has typically been used by VB to access DirectShow at a very
high level.  The most notable interface is the IMediaControl interface.  With 
this single interface, an application can open a media stream and render it
on the local machine.  It is also the library that contains the FilGraphManagerClass,
which is commonly used in filter graph construction.

The other library, QEdit.dll, contains the vast majority of other interfaces that
are commonly used for filter graph creation, including many of the DirectShow provided
filters.

Between these two basic libraries, you have all you need to perform 90% of the directshow
tasks typically found in Media applications.

Core - The core of the DirectShow filter graph system

BDA - Broadcast Driver Architecture (Television)

DES - DirectShow Editing Services

DVD - Digital Video Disc

MMStreaming - Multimedia Streaming

Quartz - Scripting

SBE - Stream Buffer Engine 


