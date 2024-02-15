namespace RSS_Checker;

public class RssRepository(Dictionary<string, DateTimeOffset> feeds) {
	public readonly Dictionary<string, DateTimeOffset> Feeds = feeds;

	private static readonly string DataPath = AppDomain.CurrentDomain.BaseDirectory + "/data.txt";
	private const char FeedFileDelimiter = '|';

	public void Save() {
		using StreamWriter writer = new(DataPath);
		
		foreach (KeyValuePair<string,DateTimeOffset> feed in Feeds) 
			writer.WriteLine($"{feed.Key}{FeedFileDelimiter}{feed.Value}");
		
		writer.Close();
	}
	
	public static RssRepository Load() {
		if (!File.Exists(DataPath)) {
			return new RssRepository(new Dictionary<string, DateTimeOffset>());
		}
		
		using StreamReader reader = new(DataPath);
		string? line = reader.ReadLine();
		Dictionary<string, DateTimeOffset> prevFeed = new();
		while (line != null) {
			string[] parsedLine = line.Split(FeedFileDelimiter);
			string url = parsedLine[0];
			DateTimeOffset lastChecked = DateTimeOffset.Parse(parsedLine[1]);
			
			prevFeed.Add(url, lastChecked);
			
			line = reader.ReadLine();
		}
		
		reader.Close();

		return new RssRepository(prevFeed);
	}
}