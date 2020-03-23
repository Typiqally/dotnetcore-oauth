namespace NETCore.OAuth.Core
{
	public interface IEnumConverter<T>
	{
		public string ConvertToString(T type);

		public T FromString(string str);
	}
}