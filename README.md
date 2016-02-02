I was installing Windows 10 Technical Preview on my machine last month, and I was welcomed by this nice installer screen:

![2560_3000](http://tareqateik.com/content/images/2016/02/pb1.jpg)

The first thing I noticed in this screen is the nice progress ring in the middle. Unfortunately, non of the Winrt native controls we have support this behavior (progress bar is a linear bar, progress ring is just a simple animation). so I wanted to implement something similar.

I did a very simple implementation, this how you can use it:

Add the following namespace to your xaml page:

    xmlns:controls="using:ExtendedProgressRing"

Add this xaml snippet to your grid:

    <controls:ExtendedProgressRing Width="250" Value="75"  Thickness="5" RingBackground="#ff292D30" RingForeground="#ff1976BB" /> 

And heres the end results:

![image](http://tareqateik.com/content/images/2016/02/pb2.jpg)

You can find the control by searching for “[ExtendedProgressRing](http://www.nuget.org/packages/ExtendedProgressRing)” on Nuget, or by typing the following command in the Nuget console:

    Install-Package ExtendedProgressRing 

You’re free to use this control for any free or commercial project

Please reach me for any feature request (or bugs report) on Twitter [@TareqAteik](https://twitter.com/TareqAteik)
