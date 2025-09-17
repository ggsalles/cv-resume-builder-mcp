using System;
using System.Runtime.InteropServices;

namespace APP.Classes
{
    static class NetworkHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct NETRESOURCE
        {
            public uint dwScope;
            public uint dwType;
            public uint dwDisplayType;
            public uint dwUsage;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpLocalName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpRemoteName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpComment;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpProvider;
        }

        [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
        static extern int WNetAddConnection2([In] ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, int dwFlags);

        [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
        static extern int WNetCancelConnection2(string lpName, int dwFlags, bool fForce);

        public static int MapNetworkDrive(string localDrive, string remoteShare, string username, string password)
        {
            NETRESOURCE nr = new NETRESOURCE
            {
                dwType = 1, // RESOURCETYPE_DISK
                lpLocalName = string.IsNullOrEmpty(localDrive) ? null : localDrive,
                lpRemoteName = remoteShare,
                lpProvider = null
            };

            return WNetAddConnection2(ref nr, password, username, 0);
        }

        public static int Unmap(string localDriveOrRemote, bool force = true)
        {
            return WNetCancelConnection2(localDriveOrRemote, 0, force);
        }
    }
}
