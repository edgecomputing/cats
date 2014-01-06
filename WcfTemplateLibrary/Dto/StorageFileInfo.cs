using System;

namespace Cats.Service.TemplateEditor
{
	[Serializable]
	public class StorageFileInfo
	{
		/// <summary>
		/// Gets or sets the virtual path to the file
		/// </summary>
		public string VirtualPath { get; set; }

		/// <summary>
		/// Gets or sets the size of the file (in bytes)
		/// </summary>
		public long Size { get; set; }

	}
}
