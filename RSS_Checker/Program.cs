namespace RSS_Checker;

class Program {
	private static void Main() {
		RssFetcher fetcher = new();

		RssRepository repository = RssRepository.Load();

		foreach (KeyValuePair<string,IEnumerable<PostData>> feed in fetcher.Feeds) {
			if (repository.Feeds.TryGetValue(feed.Key, out DateTimeOffset lastFetch)) {
				if (feed.Value.Max().LastUpdated > lastFetch) {
					PrintNewPosts(feed, lastFetch);
					PrintFeedUpdate(feed.Key, true);
				} else {
					PrintFeedUpdate(feed.Key, false);
				}
				repository.Feeds[feed.Key] = DateTimeOffset.Now;
			} else {
				PrintNewPosts(feed, DateTimeOffset.MinValue);
				PrintFeedUpdate(feed.Key, true);
				repository.Feeds.Add(feed.Key, DateTimeOffset.Now);
			}
		}
		
		repository.Save();

		Console.WriteLine("\nAll feeds have been fetched.");
		Console.WriteLine("Press Any Key To Finish...");
		Console.ReadKey();
	}

	private static void PrintNewPosts(KeyValuePair<string,IEnumerable<PostData>> feed, DateTimeOffset lastFetch) {
		foreach (PostData postData in feed.Value) {
			if (postData.LastUpdated > lastFetch) {
				Console.WriteLine($"\x1b[1m{postData.Title}\x1b[0m {postData.LastUpdated.Date.ToShortDateString()} ({postData.Link})");
			}
		}
	}

	private static void PrintFeedUpdate(string feed, bool newPosts) {
		Uri myUri = new(feed);
		string host = myUri.Host;

		if (newPosts) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"New Posts Available! (https://{host})");
			Console.ForegroundColor = ConsoleColor.White;
		} else {
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"https://{host} is up to date!");
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}