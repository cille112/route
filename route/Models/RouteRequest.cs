using System.ComponentModel.DataAnnotations;

namespace Routes.Models
{

	public class Stop
	{
		[Required(ErrorMessage = "name is required.")]
		public string name { get; set; }
		public Point point { get; set; }
	}
		
	public class Point
	{
		[Required(ErrorMessage = "Latitude is required.")]
		[Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
		public double latitude { get; set; }
		[Required(ErrorMessage = "Longitude is required.")]
		[Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
		public double longitude { get; set; }
		
	}
}
