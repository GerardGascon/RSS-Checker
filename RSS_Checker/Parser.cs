using System.Xml;
using System.ServiceModel.Syndication;

namespace RSS_Checker;

public static class Parser {
	public static IEnumerable<PostData> Run(string url) {
		using XmlReader reader = XmlReader.Create(url);
		SyndicationFeed feed = SyndicationFeed.Load(reader);

		if (feed == null) return Enumerable.Empty<PostData>();

		List<PostData> parsedResult = [];
		parsedResult.AddRange(feed.Items.Select(element => element.ToPostData()));
		return parsedResult;
	}
}