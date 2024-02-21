using System.ServiceModel.Syndication;

namespace RSS_Checker;

public static class PostDataBridge {
	public static PostData ToPostData(this SyndicationItem item) {
		DateTimeOffset lastUpdated = item.LastUpdatedTime;
		if (item.PublishDate > lastUpdated)
			lastUpdated = item.PublishDate;

		return new PostData(item.Title.Text, lastUpdated, item.Summary.Text, item.Links[0].Uri.ToString());
	}
}