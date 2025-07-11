namespace SnakeVSBlocks.Save
{
	public static class SaveUtility
	{
		private const string PlayerDataFile = "PlayerData.es3";

		public static T LoadValue<T>(string key, T defaultValue) => IsKeyExist(key) == false ? defaultValue : ES3.Load<T>(key, PlayerDataFile);

		public static void SaveValue<T>(string key, T value) => ES3.Save(key, value, PlayerDataFile);

		public static void DeleteValue(string key) => ES3.DeleteKey(key, PlayerDataFile);

		public static bool IsKeyExist(string key) => ES3.KeyExists(key, PlayerDataFile);
	}
}
