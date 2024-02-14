namespace RSS_Checker;

class Program {
	private static void Main() {
		IEnumerable<PostData> posts = Parser.Run("https://visualstudiomagazine.com/rss-feeds/news.aspx");

		foreach (PostData data in posts) {
			Console.WriteLine(data.ToString());
			Console.WriteLine();
		}
	}
}