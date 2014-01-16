using System;

namespace Cats.Service.TemplateEditor
{
	public delegate void FileEventHandler(object sender, FileEventArgs e);

	public class FileEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the virtual path.
		/// </summary>
		public string VirtualPath
		{
			get { return _VirtualPath; }
		}
		string _VirtualPath = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileEventArgs"/> class.
		/// </summary>
		/// <param name="vPath">The v path.</param>
		public FileEventArgs(string vPath)
		{
			this._VirtualPath = vPath;
		}
	}
}
