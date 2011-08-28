﻿namespace Utilities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Runtime.InteropServices;
	using System.Text;
	using Microsoft.Win32.SafeHandles;
	using Native;

	public static class ZlpIOHelper
	{
		/// <summary>
		/// Pass the file handle to the <see cref="System.IO.FileStream"/> constructor. 
		/// The <see cref="System.IO.FileStream"/> will close the handle.
		/// </summary>
		public static SafeFileHandle CreateFileHandle(
			string filePath,
			CreationDisposition creationDisposition,
			FileAccess fileAccess,
			FileShare fileShare)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			// Create a file with generic write access
			var fileHandle =
				PInvokeHelper.CreateFile(
					filePath,
					fileAccess,
					fileShare,
					IntPtr.Zero,
					creationDisposition,
					0,
					IntPtr.Zero);

			// Check for errors.
			var lastWin32Error = Marshal.GetLastWin32Error();
			if (fileHandle.IsInvalid)
			{
				throw new Win32Exception(
					lastWin32Error,
					string.Format(
						"Error {0} creating file handle for file path '{1}': {2}",
						lastWin32Error,
						filePath,
						CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
			}

			// Pass the file handle to FileStream. FileStream will close the handle.
			return fileHandle;
		}

		public static void CopyFile(
			string sourceFilePath,
			string destinationFilePath,
			bool overwriteExisting)
		{
			sourceFilePath = CheckAddLongPathPrefix(sourceFilePath);
			destinationFilePath = CheckAddLongPathPrefix(destinationFilePath);

			if (!PInvokeHelper.CopyFile(sourceFilePath, destinationFilePath, !overwriteExisting))
			{
				// http://msdn.microsoft.com/en-us/library/ms681382(VS.85).aspx.

				var lastWin32Error = Marshal.GetLastWin32Error();
				throw new Win32Exception(
					lastWin32Error,
					string.Format(
						"Error {0} copying file '{1}' to '{2}': {3}",
						lastWin32Error,
						sourceFilePath,
						destinationFilePath,
						CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
			}
		}

		// http://www.dotnet247.com/247reference/msgs/21/108780.aspx
		public static string GetFileOwner(
			string filePath)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			IntPtr pZero;
			IntPtr pSid;
			IntPtr psd; // Not used here

			var errorReturn =
				PInvokeHelper.GetNamedSecurityInfo(
					filePath,
					PInvokeHelper.SeFileObject,
					PInvokeHelper.OwnerSecurityInformation,
					out pSid,
					out pZero,
					out pZero,
					out pZero,
					out psd);

			if (errorReturn == 0)
			{
				const int bufferSize = 64;
				var buffer = new StringBuilder();
				var accounLength = bufferSize;
				var domainLength = bufferSize;
				int sidNameUse;
				var account = new StringBuilder(bufferSize);
				var domain = new StringBuilder(bufferSize);
				
				errorReturn =
					PInvokeHelper.LookupAccountSid(
						null,
						pSid,
						account,
						ref accounLength,
						domain,
						ref domainLength,
						out sidNameUse);

				if (errorReturn == 0)
				{
					// http://msdn.microsoft.com/en-us/library/ms681382(VS.85).aspx.

					var lastWin32Error = Marshal.GetLastWin32Error();
					throw new Win32Exception(
						lastWin32Error,
						string.Format(
							"Error {0} looking up account SID while getting file owner for file '{1}': {2}",
							lastWin32Error,
							filePath,
							CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
				}
				else
				{
					buffer.Append(domain);
					buffer.Append(@"\");
					buffer.Append(account);
					return buffer.ToString();
				}
			}
			else
			{
				// http://msdn.microsoft.com/en-us/library/ms681382(VS.85).aspx.

				var lastWin32Error = Marshal.GetLastWin32Error();
				throw new Win32Exception(
					lastWin32Error,
					string.Format(
						"Error {0} getting names security info while getting file owner for file '{1}': {2}",
						lastWin32Error,
						filePath,
						CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
			}
		}

		public static void SetFileAttributes(string filePath, FileAttributes attributes)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			if (!PInvokeHelper.SetFileAttributes(filePath, attributes))
			{
				// http://msdn.microsoft.com/en-us/library/ms681382(VS.85).aspx.

				var lastWin32Error = Marshal.GetLastWin32Error();
				throw new Win32Exception(
					lastWin32Error,
					string.Format(
						"Error {0} setting file attribute of file '{1}' to '{2}': {3}",
						lastWin32Error,
						filePath,
						attributes,
						CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
			}
		}

		public static FileAttributes GetFileAttributes(string filePath)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			return (FileAttributes)PInvokeHelper.GetFileAttributes(filePath);
		}

		public static void DeleteFile(string filePath)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			if (!PInvokeHelper.DeleteFile(filePath))
			{
				// http://msdn.microsoft.com/en-us/library/ms681382(VS.85).aspx.

				var lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != PInvokeHelper.ERROR_NO_MORE_FILES &&
					lastWin32Error != PInvokeHelper.ERROR_FILE_NOT_FOUND)
				{
					throw new Win32Exception(
						lastWin32Error,
						string.Format(
							"Error {0} deleting file '{1}': {2}",
							lastWin32Error,
							filePath,
						CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
				}
			}
		}

		public static void DeleteDirectory(string folderPath, bool recursive)
		{
			folderPath = CheckAddLongPathPrefix(folderPath);

			if (DirectoryExists(folderPath))
			{
				if (recursive)
				{
					var files = GetFiles(folderPath);
					var dirs = GetDirectories(folderPath);

					foreach (var file in files)
					{
						DeleteFile(file.FullName);
					}

					foreach (var dir in dirs)
					{
						DeleteDirectory(dir.FullName, true);
					}
				}

				if (!PInvokeHelper.RemoveDirectory(folderPath))
				{
					{
						// http://msdn.microsoft.com/en-us/library/ms681382(VS.85).aspx.

						var lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error != PInvokeHelper.ERROR_NO_MORE_FILES)
						{
							throw new Win32Exception(
								lastWin32Error,
								string.Format(
									"Error {0} deleting folder '{1}': {2}",
									lastWin32Error,
									folderPath,
									CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
						}
					}
				}
			}
		}

		public static bool FileExists(string filePath)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			var a = PInvokeHelper.GetFileAttributes(filePath);
			if ((a & PInvokeHelper.INVALID_FILE_ATTRIBUTES) == PInvokeHelper.INVALID_FILE_ATTRIBUTES)
			{
				return false;
			}
			else
			{
				return (a & PInvokeHelper.FILE_ATTRIBUTE_DIRECTORY) == 0;
			}

			//filePath = CheckAddLongPathPrefix(filePath);

			//PInvokeHelper.WIN32_FIND_DATA fd;
			//var result = PInvokeHelper.FindFirstFile(filePath.TrimEnd('\\'), out fd);

			//if (result.ToInt32() == PInvokeHelper.ERROR_FILE_NOT_FOUND || result == PInvokeHelper.INVALID_HANDLE_VALUE)
			//{
			//    return false;
			//}
			//else
			//{
			//    return ((int)fd.dwFileAttributes & PInvokeHelper.FILE_ATTRIBUTE_DIRECTORY) == 0;
			//}
		}

		public static void CreateDirectory(string directoryPath)
		{
			if (string.IsNullOrEmpty(directoryPath))
			{
				throw new ArgumentNullException(@"directoryPath");
			}

			string basePart;
			string[] childParts;

			splitFolderPath(directoryPath, out basePart, out childParts);

			var path = basePart;

			foreach (var childPart in childParts)
			{
				path = combine(path, childPart);

				if (!DirectoryExists(path))
				{
					doCreateDirectory(path);
				}
			}
		}

		private static string combine(
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

		private static void splitFolderPath(
			string directoryPath,
			out string basePart,
			out string[] childParts)
		{
			directoryPath = forceRemoveLongPathPrefix(directoryPath);

			basePart = getDriveOrShare(directoryPath);

			var remaining = directoryPath.Substring(basePart.Length);
			childParts = remaining.Trim('\\').Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
		}

		private static string getDrive(
			string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			else
			{
				var colonPos = path.IndexOf(':');
				var slashPos = path.IndexOf('\\');

				if (colonPos <= 0)
				{
					return string.Empty;
				}
				else
				{
					if (slashPos < 0 || slashPos > colonPos)
					{
						return path.Substring(0, colonPos + 1);
					}
					else
					{
						return string.Empty;
					}
				}
			}
		}

		private static string getShare(
			string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			else
			{
				var str = path;

				// Nach Doppel-Slash suchen.
				// Kann z.B. "\\server\share\" sein,
				// aber auch "http:\\www.xyz.com\".
				const string dblslsh = @"\\";
				var n = str.IndexOf(dblslsh);
				if (n < 0)
				{
					return string.Empty;
				}
				else
				{
					// Übernehme links von Doppel-Slash alles in Rückgabe
					// (inkl. Doppel-Slash selbst).
					var ret = str.Substring(0, n + dblslsh.Length);
					str = str.Remove(0, n + dblslsh.Length);

					// Jetzt nach Slash nach Server-Name suchen.
					// Dieser Slash darf nicht unmittelbar nach den 2 Anfangsslash stehen.
					n = str.IndexOf('\\');
					if (n <= 0)
					{
						return string.Empty;
					}
					else
					{
						// Wiederum übernehmen in Rückgabestring.
						ret += str.Substring(0, n + 1);
						str = str.Remove(0, n + 1);

						// Jetzt nach Slash nach Share-Name suchen.
						// Dieser Slash darf ebenfalls nicht unmittelbar 
						// nach dem jetzigen Slash stehen.
						n = str.IndexOf('\\');
						if (n < 0)
						{
							n = str.Length;
						}
						else if (n == 0)
						{
							return string.Empty;
						}

						// Wiederum übernehmen in Rückgabestring, 
						// aber ohne letzten Slash.
						ret += str.Substring(0, n);

						// The last item must not be a slash.
						if (ret[ret.Length - 1] == '\\')
						{
							return string.Empty;
						}
						else
						{
							return ret;
						}
					}
				}
			}
		}

		private static string getDriveOrShare(
			string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			else
			{
				if (!string.IsNullOrEmpty(getDrive(path)))
				{
					return getDrive(path);
				}
				else if (!string.IsNullOrEmpty(getShare(path)))
				{
					return getShare(path);
				}
				else
				{
					return string.Empty;
				}
			}
		}

		private static void doCreateDirectory(string directoryPath)
		{
			directoryPath = CheckAddLongPathPrefix(directoryPath);

			if (!PInvokeHelper.CreateDirectory(directoryPath, IntPtr.Zero))
			{
				// http://msdn.microsoft.com/en-us/library/ms681382(VS.85).aspx.

				var lastWin32Error = Marshal.GetLastWin32Error();
				throw new Win32Exception(
					lastWin32Error,
					string.Format(
						"Error {0} creating directory '{1}': {2}",
						lastWin32Error,
						directoryPath,
						CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
			}
		}

		public static bool DirectoryExists(string directoryPath)
		{
			directoryPath = CheckAddLongPathPrefix(directoryPath);

			var a = PInvokeHelper.GetFileAttributes(directoryPath);
			if ((a & PInvokeHelper.INVALID_FILE_ATTRIBUTES) == PInvokeHelper.INVALID_FILE_ATTRIBUTES)
			{
				return false;
			}
			else
			{
				return (a & PInvokeHelper.FILE_ATTRIBUTE_DIRECTORY) == PInvokeHelper.FILE_ATTRIBUTE_DIRECTORY;
			}

			//PInvokeHelper.WIN32_FIND_DATA fd;
			//var result = PInvokeHelper.FindFirstFile(directoryPath.TrimEnd('\\') /*+ @"\*"*/, out fd);

			//if (result.ToInt32() == PInvokeHelper.ERROR_FILE_NOT_FOUND || result == PInvokeHelper.INVALID_HANDLE_VALUE)
			//{
			//    return false;
			//}
			//else
			//{
			//    return ((int)fd.dwFileAttributes & PInvokeHelper.FILE_ATTRIBUTE_DIRECTORY) != 0;
			//}
		}

		public static DateTime GetFileLastWriteTime(
			string filePath)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			PInvokeHelper.WIN32_FIND_DATA fd;
			var result = PInvokeHelper.FindFirstFile(filePath.TrimEnd('\\'), out fd);

			if (result.ToInt32() == PInvokeHelper.ERROR_FILE_NOT_FOUND || result == PInvokeHelper.INVALID_HANDLE_VALUE)
			{
				return DateTime.MinValue;
			}
			else
			{
				var ft = fd.ftLastWriteTime;

				var hft2 = (((long)ft.dwHighDateTime) << 32) + ft.dwLowDateTime;
				return DateTime.FromFileTimeUtc(hft2);
			}
		}

		public static DateTime GetFileLastAccessTime(
			string filePath)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			PInvokeHelper.WIN32_FIND_DATA fd;
			var result = PInvokeHelper.FindFirstFile(filePath.TrimEnd('\\'), out fd);

			if (result.ToInt32() == PInvokeHelper.ERROR_FILE_NOT_FOUND || result == PInvokeHelper.INVALID_HANDLE_VALUE)
			{
				return DateTime.MinValue;
			}
			else
			{
				var ft = fd.ftLastAccessTime;

				var hft2 = (((long)ft.dwHighDateTime) << 32) + ft.dwLowDateTime;
				return DateTime.FromFileTimeUtc(hft2);
			}
		}

		public static DateTime GetFileCreationTime(
			string filePath)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			PInvokeHelper.WIN32_FIND_DATA fd;
			var result = PInvokeHelper.FindFirstFile(filePath.TrimEnd('\\'), out fd);

			if (result.ToInt32() == PInvokeHelper.ERROR_FILE_NOT_FOUND || result == PInvokeHelper.INVALID_HANDLE_VALUE)
			{
				return DateTime.MinValue;
			}
			else
			{
				var ft = fd.ftCreationTime;

				var hft2 = (((long)ft.dwHighDateTime) << 32) + ft.dwLowDateTime;
				return DateTime.FromFileTimeUtc(hft2);
			}
		}

		public static long GetFileLength(string filePath)
		{
			filePath = CheckAddLongPathPrefix(filePath);

			PInvokeHelper.WIN32_FIND_DATA fd;
			var result = PInvokeHelper.FindFirstFile(filePath.TrimEnd('\\'), out fd);

			if (result.ToInt32() == PInvokeHelper.ERROR_FILE_NOT_FOUND || result == PInvokeHelper.INVALID_HANDLE_VALUE)
			{
				return 0;
			}
			else
			{
				var low = (long)fd.nFileSizeLow;
				var high = (long)fd.nFileSizeHigh;

				return (high << 32) + low;
			}
		}

		public static ZlpFileInfo[] GetFiles(string directoryPath)
		{
			return GetFiles(directoryPath, @"*.*");
		}

		public static ZlpFileInfo[] GetFiles(string directoryPath, string pattern)
		{
			return GetFiles(directoryPath, pattern, System.IO.SearchOption.TopDirectoryOnly);
		}

		public static ZlpFileInfo[] GetFiles(string directoryPath, System.IO.SearchOption searchOption)
		{
			return GetFiles(directoryPath, @"*.*", searchOption);
		}

		public static ZlpFileInfo[] GetFiles(string directoryPath, string pattern, System.IO.SearchOption searchOption)
		{
			directoryPath = CheckAddLongPathPrefix(directoryPath);

			var results = new List<ZlpFileInfo>();
			PInvokeHelper.WIN32_FIND_DATA findData;
			var findHandle = PInvokeHelper.FindFirstFile(directoryPath.TrimEnd('\\') + "\\" + pattern, out findData);

			if (findHandle != PInvokeHelper.INVALID_HANDLE_VALUE)
			{
				try
				{
					bool found;
					do
					{
						var currentFileName = findData.cFileName;

						// if this is a file, find its contents
						if (((int)findData.dwFileAttributes & PInvokeHelper.FILE_ATTRIBUTE_DIRECTORY) == 0)
						{
							results.Add(new ZlpFileInfo(ZlpPathHelper.Combine(directoryPath, currentFileName)));
						}

						// find next
						found = PInvokeHelper.FindNextFile(findHandle, out findData);
					}
					while (found);
				}
				finally
				{
					// close the find handle
					PInvokeHelper.FindClose(findHandle);
				}
			}

			if (searchOption == System.IO.SearchOption.AllDirectories)
			{
				foreach (var dir in GetDirectories(directoryPath))
				{
					results.AddRange(GetFiles(dir.FullName, pattern, searchOption));
				}
			}

			return results.ToArray();
		}

		public static ZlpDirectoryInfo[] GetDirectories(string directoryPath)
		{
			return GetDirectories(directoryPath, @"*");
		}

		public static ZlpDirectoryInfo[] GetDirectories(string directoryPath, string pattern)
		{
			directoryPath = CheckAddLongPathPrefix(directoryPath);

			var results = new List<ZlpDirectoryInfo>();
			PInvokeHelper.WIN32_FIND_DATA findData;
			var findHandle = PInvokeHelper.FindFirstFile(directoryPath.TrimEnd('\\') + @"\" + pattern, out findData);

			if (findHandle != PInvokeHelper.INVALID_HANDLE_VALUE)
			{
				try
				{
					bool found;
					do
					{
						var currentFileName = findData.cFileName;

						// if this is a directory, find its contents
						if (((int)findData.dwFileAttributes & PInvokeHelper.FILE_ATTRIBUTE_DIRECTORY) != 0)
						{
							if (currentFileName != @"." && currentFileName != @"..")
							{
								results.Add(new ZlpDirectoryInfo(ZlpPathHelper.Combine(directoryPath, currentFileName)));
							}
						}

						// find next
						found = PInvokeHelper.FindNextFile(findHandle, out findData);
					} while (found);
				}
				finally
				{
					// close the find handle
					PInvokeHelper.FindClose(findHandle);
				}
			}

			return results.ToArray();
		}

		internal static string CheckAddLongPathPrefix(string path)
		{
			if (string.IsNullOrEmpty(path) || path.StartsWith(@"\\?\"))
			{
				return path;
			}
			else if (path.Length > PInvokeHelper.MAX_PATH)
			{
				return ForceAddLongPathPrefix(path);
			}
			else
			{
				return path;
			}
		}

		private static string forceRemoveLongPathPrefix(string path)
		{
			if (string.IsNullOrEmpty(path) || !path.StartsWith(@"\\?\"))
			{
				return path;
			}
			else if (path.StartsWith(@"\\?\UNC\", StringComparison.InvariantCultureIgnoreCase))
			{
				return path.Substring(@"\\?\UNC\".Length);
			}
			else
			{
				return path.Substring(@"\\?\".Length);
			}
		}

		internal static string ForceAddLongPathPrefix(string path)
		{
			if (string.IsNullOrEmpty(path) || path.StartsWith(@"\\?\"))
			{
				return path;
			}
			else
			{
				// http://msdn.microsoft.com/en-us/library/aa365247.aspx

				if (path.StartsWith(@"\\"))
				{
					// UNC.
					return @"\\?\UNC\" + path.Substring(2);
				}
				else
				{
					return @"\\?\" + path;
				}
			}
		}

		internal static string CheckAddDotEnd(
			string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return @".";
			}
			else
			{
				text = text.Trim();
				if (text.EndsWith(@"."))
				{
					return text;
				}
				else
				{
					return text + @".";
				}
			}
		}
	}
}