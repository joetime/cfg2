using cfgdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    /// <summary>
    /// The go-between class hzBroker -> Broker
    /// </summary>
    public class Broker
    {
        public Broker(hzBroker broker)
        {
            Id = broker.Id;
            Name = broker.Name;
            BrokerNumber = broker.Number;
            CoastalNumber = broker.CfgNumber;
            Street = broker.Street;
            City = broker.City;
            State = broker.State;
            Zip = broker.Zip;
            Ssn = broker.Ssn;
            ConstantCommission = broker.ConsCommission;
            Deleted = broker.Deleted;

            GroupsCount = broker.hzClients.Count();
            CommissionRatesCount = broker.hzCommissionRates.Count();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string BrokerNumber { get; private set; }
        public string CoastalNumber { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }
        public string Ssn { get; private set; }
        public decimal ConstantCommission { get; private set; }
        public bool Deleted { get; private set; }

        public int GroupsCount { get; private set; }
        public int CommissionRatesCount { get; private set; }
        //public virtual ICollection<object> Groups { get; set; }
        //public virtual ICollection<hzCommissionRate> CommissionRates { get; set; }
    }

    public partial class Repos
    {
        /// <summary>
        /// Find broker by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Broker Broker(int id)
        {
            hzBroker broker = DB.hzBrokers.Find(id);
            return new Broker(broker);
        }

        /// <summary>
        /// Find broker by broker number.
        /// </summary>
        /// <param name="number">Number to search for</param>
        /// <returns>A broker</returns>
        public Broker Broker(string number)
        {
            var brokers = DB.hzBrokers.Where(b => b.Number.ToLower() == number.ToLower());

            if (brokers.Count() > 1)
                throw new InvalidOperationException("More than one broker has this number.");

            hzBroker broker = brokers.FirstOrDefault();
            return new Broker(broker);
        }

        /// <summary>
        /// Find broker by broker name.
        /// </summary>
        /// <param name="name">Number to search for</param>
        /// <returns>A broker</returns>
        public Broker BrokerByName(string name)
        {
            hzBroker broker = DB.hzBrokers
                .FirstOrDefault(b => b.Name.ToLower() == name.ToLower());
            return new Broker(broker);
        }
    }
}
