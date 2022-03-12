namespace CavemanTools.Infrastructure
{
	public class YesNoResult
    {
        public static YesNoResult Yes=new YesNoResult(true);
        public static YesNoResult No=new YesNoResult(false);


        public bool Value { get; private set; }

        public YesNoResult(bool value)
        {
            Value = value;
        }
    }

}