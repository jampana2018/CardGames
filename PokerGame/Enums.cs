using System.ComponentModel;
using System;

public enum Symbol
{
    [Description("Hearts")]
    H,
    [Description("Clubs")]
    C,
    [Description("Spades")]
    S,
    [Description("Diamonds")]
    D
}

public enum CardFace
{
	[Description("Ace")]
	A = 14,
	[Description("Jack")]
	J = 11,
	[Description("Queen")]
	Q = 12,
	[Description("Kind")]
	K = 13
}

public enum Stakes
{
    RoyalFlush = 100,
    StraightFlush = 90,
    FourOfKind = 80,
    FullHouse = 70,
    Flush = 60,
    Straight = 50,
    ThreeOfKind = 40,
    TwoPair = 30,
    OnePair = 20,
    BigCard = 10
}


public static class EnumExtensions
{
    public static string GetDescription<T>(this T enumerationValue)
      where T : struct
    {
        var type = enumerationValue.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
        }
        var memberInfo = type.GetMember(enumerationValue.ToString());
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }
        return enumerationValue.ToString();
    }
}
