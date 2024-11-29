namespace Routes.Models
{

	public class Stop
	{
		public string name { get; set; }
		public Point point { get; set; }
	}
		
	public class Point
	{
		public string latitude { get; set; }
		public string longitude { get; set; }
		
	}
}
