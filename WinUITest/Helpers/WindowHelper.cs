﻿using System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinUITest.Helpers;

// Taken from Winui3 gallery demo
// Helper class to allow the app to find the Window that contains an
// arbitrary UIElement (GetWindowForElement).  To do this, we keep track
// of all active Windows.  The app code must call WindowHelper.CreateWindow
// rather than "new Window" so we can keep track of all the relevant
// windows.  In the future, we would like to support this in platform APIs.
public class WindowHelper
{
    static public Window CreateWindow()
    {
        Window newWindow = new Window();
        TrackWindow(newWindow);
        return newWindow;
    }

    static public void TrackWindow(Window window)
    {
        window.Closed += (sender, args) =>
        {
            _activeWindows.Remove(window);
        };
        _activeWindows.Add(window);
    }

    static public Window GetWindowForElement(UIElement element)
    {
        if (element.XamlRoot != null)
        {
            foreach (Window window in _activeWindows)
            {
                if (element.XamlRoot == window.Content.XamlRoot)
                {
                    return window;
                }
            }
        }
        return null;
    }

    static public List<Window> ActiveWindows { get { return _activeWindows; } }

    static private List<Window> _activeWindows = new List<Window>();
}
