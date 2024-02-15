namespace RSS_Checker;

public class RssFetcher {
	private static readonly string FeedsPath = AppDomain.CurrentDomain.BaseDirectory + "/rssFeeds.txt";

	public readonly Dictionary<string, IEnumerable<PostData>> Feeds;

	public RssFetcher() {
		Feeds = new Dictionary<string, IEnumerable<PostData>>();

		if (!File.Exists(FeedsPath)) {
			File.Create(FeedsPath);
			Console.WriteLine($"A feed container file has been created at: {FeedsPath}");
			return;
		}
		
		using StreamReader reader = new(FeedsPath);
		string? feedUrl = reader.ReadLine();
		while (feedUrl != null) {
			Feeds.Add(feedUrl, Parser.Run(feedUrl));
			
			feedUrl = reader.ReadLine();
		}
		
		reader.Close();
	}
}