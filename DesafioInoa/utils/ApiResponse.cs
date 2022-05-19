using System;

namespace DesafioInoa.utils
{
	public class ApiResponse
	{
		public bool valid_key { get; set; }
		public Dictionary<string, Result> results { get; set; }	
	}

	public class Result
    {
		public float price { get; set; }
		public string updated_at { get; set; }
    }
}

