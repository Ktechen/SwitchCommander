using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public class Switch : BaseEntity
{
    public string? Name { get; set; }
    public string? Manufacturer { get; set; }
    public string? SwitchChip { get; set; }
    public string? Management { get; set; }
    public string? AirFlow { get; set; }
    public string? SecurityAlgorithms { get; set; }
    public string? DhcpFunctions { get; set; }
    public string? ManagementProtocols { get; set; }
    public string? NetworkStandard{ get; set; }
    
    public int PortsCount { get; set; }
    public int ArpTabelleCount { get; set; }
    public int VlanCount { get; set; }
    public int JumboFrame { get; set; }
    public int RoutenCount { get; set; }
    public int Ram { get; set; }
    public int Flash { get; set; }
    public int ProcessorClockFrequency { get; set; }
    
    public bool PoE { get; set; }
    public bool Stacking { get; set; }
    public bool Pv6 { get; set; }
    public bool Layer3 { get; set; }
    public bool IgmpSnooping { get; set; }
    public bool Ssh { get; set; }
    public bool Multicast { get; set; }
    public bool AccessControlList { get; set; }
    public bool PortMirroring { get; set; }
    public bool SupportDataFlowControl { get; set; }
    public bool BroadcastStormControl { get; set; }
    public bool AutoMdimdix { get; set; }
    public bool TensionTreeProtocol { get; set; }
    public bool HeadOfLineHolBlockierung { get; set; }
    public bool VlanSupport { get; set; }
    public bool JumboFramesSupport { get; set; }
    public bool UpgradeableFirmware { get; set; }
    public bool QoS { get; set; }
    public bool CloudManaged { get; set; }
    
    public Tuple<int, string>? FlashMemory { get; set; }
    public Tuple<int, string>? PowerBudget { get; set; }
    public Tuple<int, string>? ForwardingRate { get; set; }
    public Tuple<int, string>? SwitchCapacity { get; set; }
    public Tuple<int, string>? Mtbf { get; set; }
    
    public IEnumerable<Port>? Ports { get; set; }
}