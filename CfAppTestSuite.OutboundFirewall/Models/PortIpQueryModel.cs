using System.Text;

namespace CfAppTestSuite.OutboundFirewall.Models
{
    public class PortIpQueryModel
    {
        public PortSegment PortRange { get; set; }
        public IPSegment IPRange { get; set; }
    }

    public class PortSegment { }

    public class PortRange : PortSegment
    {
        public int Start { get; set; }
        public int End { get; set; }

        public override string ToString()
        {
            return $"{Start}-{End}";
        }
    }

    public class SinglePort : PortSegment
    {
        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class IPSegment { }

    public class IPRange : IPSegment
    {
        public SingleIP Min { get; set; }
        public SingleIP Max { get; set; }

        public override string ToString()
        {
            return $"{Min}-{Max}";
        }
    }
    public class CidrRange : IPSegment
    {
        public int Cidr { get; set; }
        public SingleIP IP { get; set; }

        public override string ToString()
        {
            return $"{IP}/{Cidr}";
        }
    }

    public class SingleIP : IPSegment
    {
        public byte[] Value { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < Value.Length; i++)
            {
                if (i > 0)
                    stringBuilder.Append(".");
                stringBuilder.Append(Value[i]);
            }
            return stringBuilder.ToString();
        }
    }
}
