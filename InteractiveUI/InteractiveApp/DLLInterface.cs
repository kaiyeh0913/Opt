﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;

namespace UIWindow
{
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct IVBitmapInfo
    {
        [MarshalAs(UnmanagedType.U4)]
        public int width;
        [MarshalAs(UnmanagedType.U4)]
        public int height;
        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr colorData;
    }

    public class DLLInterface
    {
        const string InteractiveDLL = "Interactive.dll";
        [DllImport(InteractiveDLL, CharSet = CharSet.Unicode)]
        public static extern IntPtr IVInit();
        [DllImport(InteractiveDLL)]
        private static extern UInt32 IVRunApp(IntPtr context);
        [DllImport(InteractiveDLL)]
        private static extern UInt32 IVProcessCommand(IntPtr context, [In, MarshalAs(UnmanagedType.LPStr)] String command);
        [DllImport(InteractiveDLL)]
        public static extern IntPtr IVGetStringByName(IntPtr context, [In, MarshalAs(UnmanagedType.LPStr)] String stringName);
        [DllImport(InteractiveDLL)]
        public static extern Int32 IVGetIntegerByName(IntPtr context, [In, MarshalAs(UnmanagedType.LPStr)] String integerName);
        [DllImport(InteractiveDLL)]
        public static extern float IVGetFloatByName(IntPtr context, [In, MarshalAs(UnmanagedType.LPStr)] String floatName);
        [DllImport(InteractiveDLL)]
        private static extern UInt32 IVMoveWindow(IntPtr context, int x, int y, int width, int height);
        
        public IntPtr interactiveDLLContext = (IntPtr)0;

        public DLLInterface()
        {
            if (interactiveDLLContext == (IntPtr)0)
            {
                interactiveDLLContext = IVInit();
            }
        }

        public UInt32 ProcessCommand(String command)
        {
            return IVProcessCommand(interactiveDLLContext, command);
        }

        public UInt32 RunApp()
        {
            return IVRunApp(interactiveDLLContext);
        }

        public UInt32 MoveWindow(int x, int y, int width, int height)
        {
            return IVMoveWindow(interactiveDLLContext, x, y, width, height);
        }

        public String GetString(String stringName)
        {
            IntPtr stringPtr = IVGetStringByName(interactiveDLLContext, stringName);
            if (stringPtr == (IntPtr)0)
            {
                return null;
            }
            else
            {
                return Marshal.PtrToStringAnsi(stringPtr);
            }
        }

        public int GetInt(String stringName)
        {
            return IVGetIntegerByName(interactiveDLLContext, stringName);
            
        }

        public float GetFloat(String stringName)
        {
            return IVGetFloatByName(interactiveDLLContext, stringName);

        }
    }
}