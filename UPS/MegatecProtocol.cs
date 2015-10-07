namespace UPS
{
    using System;
    using System.Linq;

    public static class MegatecProtocol
    {
        public static void CancelShutdownCommand()
        {
            UPSModule.WriteToUPS("C");
        }

        public static void CancelTestCommand()
        {
            UPSModule.WriteToUPS("CT");
        }

        private static bool CharToBin(char Bit)
        {
            return (Bit == '1');
        }

        public static InformationCommandInfo InformationCommand()
        {
            string[] source = UPSModule.WriteAndReadFromUPS("I").Replace('(', ' ').Replace("\r", " ").Split(new char[] { ' ' });
            InformationCommandInfo info = new InformationCommandInfo();
            if (source.Count<string>() == 5)
            {
                info = new InformationCommandInfo {
                    Success = true,
                    Company_Name = source[1],
                    UPS_Model = source[2],
                    Version = source[3]
                };
            }
            return info;
        }

        public static void ShutdownAndRestoreCommand(int ShutMinutes, string StartMinutes)
        {
            UPSModule.WriteToUPS("S." + ShutMinutes.ToString() + "R" + StartMinutes);
        }

        public static void ShutdownCommand(int Minutes)
        {
            UPSModule.WriteToUPS("S." + Minutes.ToString());
        }

        public static StatusInquiryInfo StatusInquiry()
        {
            string[] source = UPSModule.WriteAndReadFromUPS("Q1").Replace('(', ' ').Replace("\r", " ").Replace(".", ",").Split(new char[] { ' ' });
            StatusInquiryInfo info = new StatusInquiryInfo();
            if (source.Count<string>() == 10)
            {
                char[] chArray = source[8].ToCharArray();
                info = new StatusInquiryInfo {
                    Success = true,
                    IVoltage = Convert.ToDouble(source[1]),
                    IFaultVoltage = Convert.ToDouble(source[2]),
                    OVoltage = Convert.ToDouble(source[3]),
                    OMaximumCurrent = Convert.ToInt32(source[4]),
                    IFrequency = Convert.ToDouble(source[5]),
                    BatteryVoltage = Convert.ToDouble(source[6]),
                    Temperature = Convert.ToDouble(source[7]),
                    UPSStatus = source[8],
                    UtilityFail_Immediate = CharToBin(chArray[0]),
                    BatteryLow = CharToBin(chArray[1]),
                    Bypass_Boost_or_Buck_Active = CharToBin(chArray[2]),
                    UPS_Failed = CharToBin(chArray[3]),
                    UPSTypeisStandby_0isOn_line = CharToBin(chArray[4]),
                    TestinProgress = CharToBin(chArray[5]),
                    ShutdownActive = CharToBin(chArray[6]),
                    BeeperOn = CharToBin(chArray[7])
                };
            }
            return info;
        }

        public static void TestFor10Seconds()
        {
            UPSModule.WriteToUPS("T");
        }

        public static void TestForSpecifiedTimePeriod(int Minutes)
        {
            if ((Minutes > 0x63) || (Minutes < 1))
            {
                throw new Exception("wrong value please: 1-99!");
            }
            string str = "";
            if (Minutes < 10)
            {
                str = "0" + Minutes.ToString();
            }
            else
            {
                str = Minutes.ToString();
            }
            UPSModule.WriteToUPS("T." + str);
        }

        public static void TestUntilBatteryLow()
        {
            UPSModule.WriteToUPS("TL");
        }

        public static void TurnOnOffBeep()
        {
            UPSModule.WriteToUPS("Q");
        }

        public static UPSRatingInformationInfo UPSRatingInformation()
        {
            string[] source = UPSModule.WriteAndReadFromUPS("F").Replace('#', ' ').Replace("\r", " ").Replace(".", ",").Split(new char[] { ' ' });
            UPSRatingInformationInfo info = new UPSRatingInformationInfo();
            if (source.Count<string>() == 6)
            {
                info = new UPSRatingInformationInfo {
                    Success = true,
                    RatingVoltage = Convert.ToDouble(source[1]),
                    RatingCurrent = Convert.ToDouble(source[2]),
                    BatteryVoltage = Convert.ToDouble(source[3]),
                    Frequency = Convert.ToDouble(source[4])
                };
            }
            return info;
        }
    }
}

