namespace RSS_Checker;

public readonly struct PostData(string title, DateTimeOffset lastUpdated, string summary, string link) : IComparable<PostData> {
	public readonly string Title = title;
	public readonly DateTimeOffset LastUpdated = lastUpdated;
	public readonly string Summary = summary;
	public readonly string Link = link;

	public override string ToString() {
		return $"{Title} {LastUpdated} {Summary} {Link}";
	}

	public int CompareTo(PostData other) {
		return LastUpdated.CompareTo(other.LastUpdated);
	}
}