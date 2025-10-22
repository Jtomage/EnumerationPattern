using EnumerationPattern.Swagger;

namespace EnumerationPattern.WebDemo.Models;

/// <summary>
/// Example using EnumrationParsable
/// </summary>
public class PaymentType(int id, string baseText)
	: EnumerationParsable<int, PaymentType>(id, baseText)
{
	public static PaymentType CreditCard = new PaymentType(1, "Credit Card");
	public static PaymentType DebitCard = new PaymentType(2, "Debit Card");
	public static PaymentType PaymentProcesser = new PaymentType(3, "Payment Processor");

}
