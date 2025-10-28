using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
	public class BasketValidationResultDto
	{
		public string Id { get; set; }
		public decimal TotalPrice { get; set; }
		public int Ttl { get; set; }
		public bool IsChildPurchase { get; set; }
		public bool IsGiftable { get; set; }
	}}
