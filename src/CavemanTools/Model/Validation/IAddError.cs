namespace CavemanTools.Model.Validation
{
	/// <summary>
	///  Provide the functionality to collect multiple errors messages for the same key
	/// </summary>
	public interface IAddError
	{
		/// <summary>
		/// Adds error for key
		/// </summary>
		/// <param name="error"></param>
		void AddError(string error);
		
		/// <summary>
		/// Field Name
		/// </summary>
		string Key { get; }
	}
}