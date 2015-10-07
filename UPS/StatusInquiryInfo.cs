namespace UPS
{
    using System;

    public class StatusInquiryInfo
    {
        public bool BatteryLow = false;
        public double BatteryVoltage = 0.0;
        public bool BeeperOn = false;
        public bool Bypass_Boost_or_Buck_Active = false;
        public double IFaultVoltage = 0.0;
        public double IFrequency = 0.0;
        public double IVoltage = 0.0;
        public int OMaximumCurrent = 0;
        public double OVoltage = 0.0;
        public bool ShutdownActive = false;
        public bool Success = false;
        public double Temperature = 0.0;
        public bool TestinProgress = false;
        public bool UPS_Failed = false;
        public string UPSStatus = "";
        public bool UPSTypeisStandby_0isOn_line = false;
        public bool UtilityFail_Immediate = false;
    }
}

