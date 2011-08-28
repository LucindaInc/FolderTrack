namespace Utilities
{
	using System;
	using Microsoft.Win32.SafeHandles;
	using Native;

	public class ZlpFileInfo
	{
		private readonly string _path;

		public ZlpFileInfo(string path)
		{
			_path = path;
		}

		/// <summary>
		/// Pass the file handle to the <see cref="System.IO.FileStream"/> constructor. 
		/// The <see cref="System.IO.FileStream"/> will close the handle.
		/// </summary>
		public SafeFileHandle CreateHandle(
			CreationDisposition creationDisposition,
			FileAccess fileAccess,
			FileShare fileShare)
		{
			return ZlpIOHelper.CreateFileHandle(_path, creationDisposition, fileAccess, fileShare);
		}

		public void CopyTo(
			string destinationFilePath,
			bool overwriteExisting)
		{
			ZlpIOHelper.CopyFile(_path, destinationFilePath, overwriteExisting);
		}

		public void CopyTo(
			ZlpFileInfo destinationFilePath,
			bool overwriteExisting)
		{
			ZlpIOHelper.CopyFile(_path, destinationFilePath._path, overwriteExisting);
		}

		public void Delete()
		{
			ZlpIOHelper.DeleteFile(_path);
		}

		public string Owner
		{
			get
			{
				return ZlpIOHelper.GetFileOwner(_path);
			}
		}

		public bool Exists
		{
			get
			{
				return ZlpIOHelper.DirectoryExists(_path);
			}
		}

		public DateTime LastWriteTime
		{
			get
			{
				return ZlpIOHelper.GetFileLastWriteTime(_path);
			}
		}

		public DateTime LastAccessTime
		{
			get
			{
				return ZlpIOHelper.GetFileLastAccessTime(_path);
			}
		}

		public DateTime CreationTime
		{
			get
			{
				return ZlpIOHelper.GetFileCreationTime(_path);
			}
		}

		public string FullName
		{
			get
			{
				return _path;
			}
		}

		public string Name
		{
			get
			{
				return ZlpPathHelper.GetFileNameFromFilePath(_path);
			}
		}

		public string Extension
		{
			get
			{
				return ZlpPathHelper.GetExtension(_path);
			}
		}

		public long Length
		{
			get
			{
				return ZlpIOHelper.GetFileLength(_path);
			}
		}

		public FileAttributes Attributes
		{
			get
			{
				return ZlpIOHelper.GetFileAttributes(_path);
			}
			set
			{
				ZlpIOHelper.SetFileAttributes(_path, value);
			}
		}
	}
}