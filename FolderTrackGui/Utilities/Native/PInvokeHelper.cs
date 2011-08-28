﻿namespace Utilities.Native
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Text;
	using Microsoft.Win32.SafeHandles;

	internal static class PInvokeHelper
	{
		internal const int MAX_PATH = 260;

		// http://msdn.microsoft.com/en-us/library/ms681382(VS.85).aspx
		internal const int ERROR_FILE_NOT_FOUND = 2;
		internal const int ERROR_NO_MORE_FILES = 18;

		// http://www.dotnet247.com/247reference/msgs/21/108780.aspx
		[DllImportAttribute(@"advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern int GetNamedSecurityInfo(
			string pObjectName,
			int objectType,
			int securityInfo,
			out IntPtr ppsidOwner,
			out IntPtr ppsidGroup,
			out IntPtr ppDacl,
			out IntPtr ppSacl,
			out IntPtr ppSecurityDescriptor);

		[DllImport(@"advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int LookupAccountSid(
			string systemName,
			IntPtr psid,
			StringBuilder accountName,
			ref int cbAccount,
			[Out] StringBuilder domainName,
			ref int cbDomainName,
			out int use);

		public const int OwnerSecurityInformation = 1;
		public const int SeFileObject = 1;

		[StructLayout(LayoutKind.Sequential)]
		internal struct FILETIME
		{
			internal uint dwLowDateTime;
			internal uint dwHighDateTime;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WIN32_FIND_DATA
		{
			internal FileAttributes dwFileAttributes;
			internal FILETIME ftCreationTime;
			internal FILETIME ftLastAccessTime;
			internal FILETIME ftLastWriteTime;
			internal int nFileSizeHigh;
			internal int nFileSizeLow;
			internal int dwReserved0;
			internal int dwReserved1;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
			internal string cFileName;
			// not using this
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			internal string cAlternate;
		}


		[StructLayout(LayoutKind.Sequential)]
		public struct SECURITY_ATTRIBUTES
		{
			public int nLength;
			public IntPtr lpSecurityDescriptor;
			public int bInheritHandle;
		}

		//[DllImport(@"kernel32.dll", CharSet = CharSet.Unicode)]
		//[return: MarshalAs(UnmanagedType.Bool)]
		//internal static extern bool CopyFile(string lpExistingFileName, string lpNewFileName, bool bFailIfExists);

		[DllImport(@"kernel32.dll",
				   CharSet = CharSet.Unicode,
				   CallingConvention = CallingConvention.StdCall,
				   SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CopyFile(
						   [MarshalAs(UnmanagedType.LPTStr)] string lpExistingFileName,
						   [MarshalAs(UnmanagedType.LPTStr)] string lpNewFileName,
						   [MarshalAs(UnmanagedType.Bool)] bool bFailIfExists);

		[DllImport(@"kernel32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CreateDirectory(
			[MarshalAs(UnmanagedType.LPTStr)]string lpPathName,
		   IntPtr lpSecurityAttributes);

		[DllImport(@"kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern uint GetFileAttributes(
			[MarshalAs(UnmanagedType.LPTStr)]string lpFileName);

		[DllImport(@"kernel32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetFileAttributes(
			 [MarshalAs(UnmanagedType.LPTStr)]string lpFileName,
			 [MarshalAs(UnmanagedType.U4)] FileAttributes dwFileAttributes);

		[DllImport(@"kernel32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RemoveDirectory(
			[MarshalAs(UnmanagedType.LPTStr)]string lpPathName);

		[DllImport(@"kernel32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteFile(
			[MarshalAs(UnmanagedType.LPTStr)]string lpFileName);

		[DllImport(@"kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr FindFirstFile(
			[MarshalAs(UnmanagedType.LPTStr)]string lpFileName,
			out WIN32_FIND_DATA lpFindFileData);

		[DllImport(@"kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern bool FindNextFile(
			IntPtr hFindFile,
			out WIN32_FIND_DATA lpFindFileData);

		[DllImport(@"kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FindClose(
			IntPtr hFindFile);

		[DllImport(@"kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern int GetFullPathName(
			[MarshalAs(UnmanagedType.LPTStr)]string lpFileName,
			int nBufferLength,
			/*[MarshalAs(UnmanagedType.LPTStr), Out]*/StringBuilder lpBuffer,
			IntPtr mustBeZero);
		//internal static extern uint GetFullPathName(
		//    string lpFileName,
		//    uint nBufferLength,
		//    [Out] StringBuilder lpBuffer,
		//    out StringBuilder lpFilePart);

		internal static int FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
		internal static uint INVALID_FILE_ATTRIBUTES = 0xFFFFFFFF;

		internal static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		// Assume dirName passed in is already prefixed with \\?\
		public static List<string> FindFilesAndDirectories(
			string directoryPath)
		{
			var results = new List<string>();
			WIN32_FIND_DATA findData;
			var findHandle = FindFirstFile(directoryPath.TrimEnd('\\') + @"\*", out findData);

			if (findHandle != INVALID_HANDLE_VALUE)
			{
				bool found;
				do
				{
					var currentFileName = findData.cFileName;

					// if this is a directory, find its contents
					if (((int)findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) != 0)
					{
						if (currentFileName != @"." && currentFileName != @"..")
						{
                            
							var childResults = FindFilesAndDirectories(Path.Combine(directoryPath, currentFileName));
							// add children and self to results
							results.AddRange(childResults);
							results.Add(Path.Combine(directoryPath, currentFileName));
						}
					}

					// it's a file; add it to the results
					else
					{
						results.Add(Path.Combine(directoryPath, currentFileName));
					}

					// find next
					found = FindNextFile(findHandle, out findData);
				}
				while (found);
			}

			// close the find handle
			FindClose(findHandle);
			return results;
		}

		[DllImport(@"kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern SafeFileHandle CreateFile(
			[MarshalAs(UnmanagedType.LPTStr)]string lpFileName,
			FileAccess dwDesiredAccess,
			FileShare dwShareMode,
			IntPtr lpSecurityAttributes,
			CreationDisposition dwCreationDisposition,
			FileAttributes dwFlagsAndAttributes,
			IntPtr hTemplateFile);
	}
}