using System.ComponentModel.DataAnnotations;

namespace Core.Validation
{
	public sealed class NoThreeSpaces : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			string input = value.ToString();

			//Not valid if contains more than 3 spaces in a row (for slugging?)
			return !UtilityFunctions.ContainsSpaceThreeTimesInARow(input);
		}
	}
}
