using System;

namespace Utilities
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Text;
    using Native;
    using System.IO;

    public static class ZlpPathHelper
    {
        public static string GetFileNameFromFilePath(string filePath)
        {
            var ls = filePath.LastIndexOf(Path.DirectorySeparatorChar);

            if (ls < 0)
            {
                return filePath;
            }
            else
            {
                return filePath.Substring(ls + 1);
            }
        }

        public static string GetDirectoryNameFromFilePath(string filePath)
        {
            var ls = filePath.LastIndexOf('\\');

            if (ls < 0)
            {
                return filePath;
            }
            else
            {
                return filePath.Substring(0, ls);
            }
        }

        public static string GetFullPath(string path)
        {
            path = ZlpIOHelper.CheckAddLongPathPrefix(path);

            /*
            var bufsz = 1;                            // We'll grow this as necessary
            var sRel = path;       // Note relative path
            var sbFull = new StringBuilder(bufsz);          // Full resolved path will go here
            StringBuilder sbFile;          // Filename will go here
            var u = PInvokeHelper.GetFullPathNameW(sRel, (uint)bufsz, sbFull, out sbFile);  // 1st call: Get necessary bufsz
            if (u > bufsz)                            // 'u' should be >1
            {
                bufsz = (int)u + 10;                       // Required size plus a few
                sbFull = new StringBuilder(bufsz);                 // Re-create objects w/ proper size
                new StringBuilder(bufsz);                 // "
                PInvokeHelper.GetFullPathNameW(sRel, (uint)bufsz, sbFull, out sbFile);    // Try again, this should succeed
                // 'sbFull' should now contain "c:\windows\system32\desktop.ini"
                //    and 'sbFile' should contain "desktop.ini"
            }

            return sbFull.ToString();
            */

            // --
            // Determine length.

            var sb = new StringBuilder();

            var realLength = PInvokeHelper.GetFullPathName(path, 0, sb, IntPtr.Zero);

            // --

            sb.Length = realLength;
            realLength = PInvokeHelper.GetFullPathName(path, sb.Length, sb, IntPtr.Zero);

            if (realLength <= 0)
            {
            	var lastWin32Error = Marshal.GetLastWin32Error();
            	throw new Win32Exception(
            		lastWin32Error,
            		string.Format(
            			"Error {0} getting full path for '{1}': {2}",
            			lastWin32Error,
            			path,
            			ZlpIOHelper.CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
            }
            else
            {
                return sb.ToString();
            }
        }

        public static string GetExtension(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            else
            {
                var pos = path.LastIndexOf('.');
                if (pos < 0)
                {
                    return string.Empty;
                }
                else
                {
                    return path.Substring(pos);
                }
            }
        }

        public static string Combine(
            string path1,
            string path2)
        {
            if (string.IsNullOrEmpty(path1))
            {
                return path2;
            }
            else if (string.IsNullOrEmpty(path2))
            {
                return path1;
            }
            else
            {
                path1 = path1.TrimEnd('\\', '/').Replace('/', '\\');
                path2 = path2.TrimStart('\\', '/').Replace('/', '\\');

                return path1 + @"\" + path2;
            }
        }

        public static string Combine(
            string path1,
            string path2,
            string path3,
            params string[] paths)
        {
            var resultPath = Combine(path1, path2);
            resultPath = Combine(resultPath, path3);

            if (paths != null)
            {
                foreach (var path in paths)
                {
                    resultPath = Combine(resultPath, path);
                }
            }

            return resultPath;
        }
    }
}