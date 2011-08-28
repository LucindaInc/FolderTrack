namespace Utilities
{
	using Native;

	public class ZlpDirectoryInfo
	{
		private readonly string _path;

		public ZlpDirectoryInfo(string path)
		{
			_path = path;
		}

		public bool Exists
		{
			get
			{
				return ZlpIOHelper.DirectoryExists(_path);
			}
		}

        public static bool  Exist(string path)
        {
            return ZlpIOHelper.DirectoryExists(path);
        }

		public void Delete(bool recursive)
		{
			ZlpIOHelper.DeleteDirectory(_path, recursive);
		}

		public void Create()
		{
			ZlpIOHelper.CreateDirectory(_path);
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
				return ZlpPathHelper.GetDirectoryNameFromFilePath(_path);
			}
		}

		public ZlpFileInfo[] GetFiles()
		{
			return ZlpIOHelper.GetFiles(_path);
		}

		public ZlpFileInfo[] GetFiles(string pattern)
		{
			return ZlpIOHelper.GetFiles(_path, pattern);
		}

		public ZlpFileInfo[] GetFiles(string pattern, System.IO.SearchOption searchOption)
		{
			return ZlpIOHelper.GetFiles(_path, pattern, searchOption);
		}

		public ZlpFileInfo[] GetFiles(System.IO.SearchOption searchOption)
		{
			return ZlpIOHelper.GetFiles(_path, searchOption);
		}

		public ZlpDirectoryInfo[] GetDirectories()
		{
			return ZlpIOHelper.GetDirectories(_path);
		}

		public ZlpDirectoryInfo[] GetDirectories(string pattern)
		{
			return ZlpIOHelper.GetDirectories(_path, pattern);
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