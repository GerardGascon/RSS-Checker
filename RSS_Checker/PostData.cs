namespace RSS_Checker;

public struct PostData(string title, DateTimeOffset lastUpdated, string summary) {
	public readonly string Title = title;
	public readonly DateTimeOffset LastUpdated = lastUpdated;
	public readonly string Summary = summary;

	public override string ToString() {
		return $"{Title} {LastUpdated} {Summary}";
	}
}