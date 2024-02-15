namespace RSS_Checker;

public readonly struct PostData(string title, DateTimeOffset lastUpdated, string summary) : IComparable<PostData> {
	public readonly string Title = title;
	public readonly DateTimeOffset LastUpdated = lastUpdated;
	public readonly string Summary = summary;

	public override string ToString() {
		return $"{Title} {LastUpdated} {Summary}";
	}

	public int CompareTo(PostData other) {
		return LastUpdated.CompareTo(other.LastUpdated);
	}
}