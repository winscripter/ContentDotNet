namespace ContentDotNet.Extensions.Image.Webp
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FourCCAttribute : Attribute
    {
        public string CharacterCode { get; set; }

        public FourCCAttribute(string cc)
        {
            CharacterCode = cc;
        }
    }
}
