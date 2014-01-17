using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public enum HorizonFileLineItemStatus
    {
        Ok,
        Error,
        Unknown
    }

    public class HorizonFileLineItem
    {   
        // char/strings used for parsing
        static string[] delimiters = new string[] { "\",\"" };
        static char quoteChar = '"';

        public string originalText { get; private set; }
        
        public HorizonFileLineItemStatus status { get; private set; }

        public HorizonFileLineItem(string text)
        {
            status = HorizonFileLineItemStatus.Unknown;
            originalText = text;
            status = doParse();
        }

        /// <summary>
        /// Parses text file for data
        /// </summary>
        /// <returns></returns>
        private HorizonFileLineItemStatus doParse() 
        {
            string[] parsed;
            
            parsed = originalText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (parsed.Count() != 14 ||
                parsed[0].First() != quoteChar ||
                parsed[13].Last() != quoteChar)
            {
                return HorizonFileLineItemStatus.Error;
            }
            else
            {
                parsed[0] = parsed.First().TrimStart(quoteChar);
                parsed[13] = parsed.Last().TrimEnd(quoteChar);

                BrokerageName = parsed[0];
                BrokerageNumber = parsed[1];
                GroupName = parsed[2];
                GroupNumber = parsed[3];
                Product = parsed[4];
                EffectiveDate = new MonthYear(parsed[5]);
                RenewalDate = new MonthYear(parsed[6]);
                InsuredPeriod = new MonthYear(parsed[7]);

                CommissionSchedule = parsed[10];
                PremiumReceived = Decimal.Parse(parsed[12]);
                CommissionReceived = Decimal.Parse(parsed[13]);

                return HorizonFileLineItemStatus.Ok;
            }
        }


        public string BrokerageName { get; private set; }
        public string BrokerageNumber { get; private set; }
        
        public string GroupName { get; private set; }
        public string GroupNumber { get; private set; }
        
        public string Product { get; private set; }

        public MonthYear EffectiveDate { get; private set; }
        public MonthYear RenewalDate { get; private set; }
        public MonthYear InsuredPeriod { get; private set; }

        public string CommissionSchedule { get; private set; }
        
        /// <summary>
        /// Premium paid by the Group to Horizon
        /// </summary>
        public decimal PremiumReceived { get; private set; }

        /// <summary>
        /// Commission paid to CFG for this account
        /// </summary>
        public decimal CommissionReceived { get; private set; }
    }
}

