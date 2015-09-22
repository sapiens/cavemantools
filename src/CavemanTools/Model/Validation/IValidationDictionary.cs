using System.Collections.Generic;

namespace CavemanTools.Model.Validation
{
	/// <summary>
	/// Provide functionality to hold validation errors messages
	/// </summary>
	public interface IValidationDictionary
	{
		/// <summary>
		/// Adds error message for key
		/// </summary>
		/// <param name="key">field identifier</param>
		/// <param name="errorMessage">Error message</param>
		void AddError(string key, string errorMessage);

		/// <summary>
		/// Returns true if there are errors messages
		/// </summary>
		bool HasErrors { get; }

        /// <summary>
        /// Gets if object state is valid
        /// </summary>
        bool IsValid { get; }

	    void CopyTo(IValidationDictionary other);
	}
}